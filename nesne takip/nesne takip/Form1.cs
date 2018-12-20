using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Imaging.Filters;
using AForge.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using System.IO.Ports;

namespace nesne_takip
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection kamera;//kamera isminde tanımladığımız değişken bilgisayardaki kamerayı tutan bir dizi. 
        private VideoCaptureDevice nesne; //nesne ise bizim kullanacağımız aygıt 
       // SerialPort sp = new SerialPort("COM3",9600);//haberleşme                                       

        public Form1()
        {
            InitializeComponent();
            //sp.Open();//port açtık.
        }
        int R; //Trackbarın değişkeneleri
        int G;
        int B;
        private void Form1_Load(object sender, EventArgs e)
        {
            kamera = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo VideoCaptureDevice in kamera)//kamera dizisine mevcut kamerayı gönderiyor.
            
            {

                comboBox1.Items.Add(VideoCaptureDevice.Name);//kamerayı combobox a gönderiyoruz.

            }

            comboBox1.SelectedIndex = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            nesne = new VideoCaptureDevice(kamera[comboBox1.SelectedIndex].MonikerString);
            nesne.NewFrame += new NewFrameEventHandler(Finalvideo_NewFrame);
            nesne.Start();//başlaya basıldığıdnda yukarıda tanımladığımız nesne değişkenine comboboxta seçilmş olan kamerayı atıyoruz ve kamera başlıyor.
        }

        void Finalvideo_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {

            Bitmap image = (Bitmap)eventArgs.Frame.Clone();// kameradan alınan görüntüyü görüntü1 e atıyoruz.
            Bitmap image1 = (Bitmap)eventArgs.Frame.Clone();
            görüntü1.Image = image;

            
            EuclideanColorFiltering filter = new EuclideanColorFiltering();//öklid renk filtresini uyguluyoruz.
            filter.CenterColor = new RGB(Color.FromArgb(R, G, B));
            filter.Radius = 100;//yarıçapı belirledim.
            filter.ApplyInPlace(image1);//filtreyi uyguladım.

            image1 = new Mean().Apply(image1);// görültüleri yok etmek için kullandım.

            nesnebul(image1);

        }

       

        public void nesnebul(Bitmap image)
        {
            BlobCounter blobCounter = new BlobCounter();//blobların boyutlarını filtreledim..
            blobCounter.MinWidth = 5;
            blobCounter.MinHeight = 5;
            blobCounter.FilterBlobs = true;
            blobCounter.ObjectsOrder = ObjectsOrder.Size;
                 

            blobCounter.ProcessImage(image);
            Rectangle[] rects = blobCounter.GetObjectsRectangles();//neseneyi dikdörtgen içine alıyor.
            Blob[] blobs = blobCounter.GetObjectsInformation();
            görüntü2.Image = image;//tanımlanan nesne görüntü2 ye atıyor.

            foreach (Rectangle recs in rects)
            {
              
                {
                    Rectangle objectRect = rects[0];
                    Graphics g = görüntü1.CreateGraphics();
                    using (Pen pen = new Pen(Color.FromArgb(0, 255, 0), 5))//renk ve kenar kalınlığını belirledim.
                    {
                        g.DrawRectangle(pen, objectRect);
                    }
                    //Cizdirilen Dikdörtgenin Koordinatlari aliniyor.
                    int objectX = objectRect.X + (objectRect.Width / 2);
                    int objectY = objectRect.Y + (objectRect.Height / 2);
                 
                    g.Dispose();


                    this.Invoke((MethodInvoker)delegate//uyarılmasını bekleyen döngü
                    {
                    if (objectX < 150 && objectY < 150)
                    {
                        //sp.Write("4");
                        textBox1.Text = "1.led yanıyor";
                        button4.BackColor = Color.Green;
                    }
                    else
                        button4.BackColor = Color.White;

                    if ((objectX < 150 && objectY > 150) && objectY < 250)
                            {
                                //sp.Write("3");
                                textBox1.Text = "2.led yanıyor";
                                button5.BackColor = Color.Green;
                            }
                            else
                                button5.BackColor = Color.White;

                            if ((objectX < 150 && objectY > 250) && objectY < 350)
                        {
                                //sp.Write("2");
                                textBox1.Text = "3.led yanıyor";
                                button6.BackColor = Color.Green;
                            }
                            else
                                button6.BackColor = Color.White;

                            if ((objectX > 150 && objectX < 250) && objectY < 150)
                        {
                                //sp.Write("b");
                                textBox1.Text = "4.led yanıyor";
                                button7.BackColor = Color.Green;
                           }
                            else
                                button7.BackColor = Color.White;

                        if ((objectX > 150 && objectX < 250) && (objectY > 150 && objectY < 250))
                        {
                                //sp.Write("9");
                                textBox1.Text = "5.led yanıyor";
                                button8.BackColor = Color.Green;
                            }
                            else
                                button8.BackColor = Color.White;

                        if ((objectX > 150 && objectX < 250) && (objectY > 250 && objectY < 400))
                        {
                                //sp.Write("8");
                                textBox1.Text = "6.led yanıyor";
                                button9.BackColor = Color.Green;
                            }
                            else
                                button9.BackColor = Color.White;

                        if (objectX > 250 && objectY < 150)
                            {
                                //sp.Write("7");
                                textBox1.Text = "7.led yanıyor";
                                button10.BackColor = Color.Green;
                            }
                            else
                                button10.BackColor = Color.White;

                        if ((objectX > 250) && (objectY > 150 && objectY < 250))
                        {
                                //sp.Write("6");
                                textBox1.Text = "8.led yanıyor";
                                button11.BackColor = Color.Green;
                            }
                            else
                                button11.BackColor = Color.White;

                        if (objectX > 250 && objectY > 250)
                        {
                                //sp.Write("5");
                                textBox1.Text = "9.led yanıyor";
                                button12.BackColor = Color.Green;
                            }
                            else
                                button12.BackColor = Color.White;

                            richTextBox1.Text = objectRect.Location.ToString() + "\n" + richTextBox1.Text + "\n"; ;
                        });
                    
                }
            }

        }
            private void button2_Click(object sender, EventArgs e)
        {
            if (nesne.IsRunning)
            {
                nesne.Stop();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (nesne.IsRunning)
            {
                nesne.Stop();

            }

            Application.Exit();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            R = trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            G = trackBar2.Value;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            B = trackBar3.Value;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
