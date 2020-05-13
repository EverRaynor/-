#include <M5StickC.h>
#include <BluetoothSerial.h>
#include<WiFi.h>
#if !defined(CONFIG_BT_ENABLED) || !defined(CONFIG_BLUEDROID_ENABLED)
#error Bluetooth is not enabled! Please run `make menuconfig` to and enable it
#endif

BluetoothSerial SerialBT;
const char* ssid="xiaohaidemeng";
const char* password="19721102";
const char* host = "192.168.1.8";
float accX = 0.0F;
float accY = 0.0F;
float accZ = 0.0F;

float gyroX = 0.0F;
float gyroY = 0.0F;
float gyroZ = 0.0F;

float pitch = 0.0F;
float roll  = 0.0F;
float yaw   = 0.0F;
int last_value_red = 0;
int cur_value_red = 0;
int last_value_blue = 0;
int cur_value_blue = 0;
unsigned long now, lastTime = 0;
float dt;
 WiFiClient client;
void setup() {
  // put your setup code here, to run once:
  M5.begin();
  M5.IMU.Init();

  
  pinMode(32, INPUT);
  pinMode(33, INPUT);
  Serial.begin(115200);
  SerialBT.begin("Esp32");
  Serial.println("pair");
  Serial.println();
  Serial.println();
  Serial.print("Connecting to ");
  Serial.println(ssid);
  WiFi.begin(ssid, password);
      while (WiFi.status() != WL_CONNECTED) {
        delay(500);
        Serial.print(".");
    }
 const int httpPort =80;
 if (!client.connect(host, httpPort)) {
        Serial.println("connection failed");
        return;
  }

    Serial.println("");
    Serial.println("WiFi connected");
    Serial.println("IP address: ");
    Serial.println(WiFi.localIP());
  M5.Lcd.setRotation(3);
  M5.Lcd.fillScreen(BLACK);
  M5.Lcd.setTextSize(1);
  M5.Lcd.setCursor(40, 0);
  M5.Lcd.println("IMU TEST");
  M5.Lcd.setCursor(0, 10);
  M5.Lcd.println("  X       Y       Z");
  M5.Lcd.setCursor(0, 50);
  M5.Lcd.println("  Pitch   Roll    Yaw");
}

int kills=0;
/*****************************************
M5.IMU.getGyroData(&gyroX,&gyroY,&gyroZ);
M5.IMU.getAccelData(&accX,&accY,&accZ);
M5.IMU.getAhrsData(&pitch,&roll,&yaw);
M5.IMU.getTempData(&temp);
*****************************************/
void loop() {
    unsigned long now = millis();             //当前时间(ms)
    dt = (now - lastTime) / 1000.0;           //微分时间(s)
    lastTime = now;   
  cur_value_red = digitalRead(32);
  cur_value_blue = digitalRead(33);
  // put your main code here, to run repeatedly:
  M5.IMU.getGyroData(&gyroX,&gyroY,&gyroZ);
  M5.IMU.getAccelData(&accX,&accY,&accZ);
  M5.IMU.getAhrsData(&pitch,&roll,&yaw);
  if (Serial.available()) {
    SerialBT.write(Serial.read());
  }
  if (SerialBT.available()) {
    int i =SerialBT.read();
    kills+=i;
    client.printf("%d",i);
    Serial.printf("%d\n",kills);
  }


   //SerialBT.printf("%6.2f  %6.2f  %6.2f      ", gyroX, gyroY, gyroZ);
   //SerialBT.printf(" %5.2f   %5.2f   %5.2f   ", accX, accY, accZ);
   if(gyroZ>=-8&&gyroZ<=8){
    gyroZ=0;
    }
   int X = -gyroZ+500;
   int Y = roll*5+500;
   if(roll>=-10&&roll<=10){
    Y=500;
   }
   if(X>=1000){
    X=999;  
    }
    if(X<=0){
      X=1;
      }
      if(cur_value_red==1&&cur_value_blue==1){
   SerialBT.printf("%d%s%s%d",X,"w","h",Y);}
   if(cur_value_red==0&&cur_value_blue==1){
    
    SerialBT.printf("%d%s%s%d",X,"W","h",Y);}
   if(cur_value_blue==0&&cur_value_red==1){
    
    SerialBT.printf("%d%s%s%d",X,"w","H",Y);}
     if(cur_value_blue==0&&cur_value_red==0){
    
    SerialBT.printf("%d%s%s%d",X,"W","H",Y);}
    
    

   

  /*if (SerialBT.available()) {
    Serial.write(SerialBT.read());
  }*/
  M5.Lcd.setCursor(0, 20);
  M5.Lcd.printf("%6.2f  %6.2f  %6.2f      ", gyroX, gyroY, gyroZ);
  M5.Lcd.setCursor(140, 20);
  M5.Lcd.print("o/s");
  M5.Lcd.setCursor(0, 30);
  M5.Lcd.printf(" %5.2f   %5.2f   %5.2f   ", accX, accY, accZ);
  M5.Lcd.setCursor(140, 30);
  M5.Lcd.print("G");
  M5.Lcd.setCursor(0, 60);
  M5.Lcd.printf(" %5.2f   %5.2f   %5.2f   ", pitch, roll, yaw);

  M5.Lcd.setCursor(0, 70);
  M5.Lcd.printf("Kill: %d",kills);

  delay(50);
}
