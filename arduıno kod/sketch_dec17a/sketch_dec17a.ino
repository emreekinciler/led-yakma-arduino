char veri=0;


void setup() {
 pinMode(4,OUTPUT),
 pinMode(3,OUTPUT),
 pinMode(2,OUTPUT),
 pinMode(10,OUTPUT),
 pinMode(9,OUTPUT),
 pinMode(8,OUTPUT),
 pinMode(7,OUTPUT),
 pinMode(6,OUTPUT),
 pinMode(5,OUTPUT),

Serial.begin(9600);
} 

void loop() {
  if(Serial.available())//seri haberleşme var ise;
  {
    veri=Serial.read();// veri değişkenine atama

    if(veri=='4')// veri 4 ise; 
    {
      digitalWrite(4,LOW);//ledi yak
    }
    else
    {
      digitalWrite(4,HIGH);// veri 4 değilse söndür.
    }
  
  if(veri=='3')
    {
      digitalWrite(3,LOW);
    }
    else
    {
      digitalWrite(3,HIGH);
    }if(veri=='2')
    {
      digitalWrite(2,LOW);
    }
    else
    {
      digitalWrite(2,HIGH);
    }if(veri=='b')
    {
      digitalWrite(10,LOW);
    }
    else
    {
      digitalWrite(10,HIGH);
    }if(veri=='9')
    {
      digitalWrite(9,LOW);
    }
    else
    {
      digitalWrite(9,HIGH);
    }if(veri=='8')
    {
      digitalWrite(8,LOW);
    }
    else
    {
      digitalWrite(8,HIGH);
    }if(veri=='7')
    {
      digitalWrite(7,LOW);
    }
    else
    {
      digitalWrite(7,HIGH);
    }if(veri=='6')
    {
      digitalWrite(6,LOW);
    }
    else
    {
      digitalWrite(6,HIGH);
    }if(veri=='5')
    {
      digitalWrite(5,LOW);
    }
    else
    {
      digitalWrite(5,HIGH);
    } 
  }
}
  
