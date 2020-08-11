#include "Adafruit_GFX.h"   // Core graphics library
#include "MCUFRIEND_kbv.h"   // Hardware-specific library
MCUFRIEND_kbv tft;

#include "FreeDefaultFonts.h"

//#include "head.h"
//#include "Ard_Logo.h"
#include "volthead.h"
#include "voltfoot.h"
#include "voltextra.h"

#define WHITE   0xFFFF
#define BLACK   0x0000
#define BLUE    0x36C
#define RED     0xF000
#define ORANGE  0xF900

int analogInput = 8;
float Vout = 0.00;
float Vin = 0.00;
float R1 = 100000.00; // resistance of R1 (100K)
float R2 = 10000.00; // resistance of R2 (10K)
int val = 0;
float prevVin;
unsigned long randNumber;
//float voltNumber;
float voltNumber = random(3.22, 3.68);

//void showmsgXY(int x, int y, int sz, const GFXfont *f, const char *msg)
//{
//  int16_t x1, y1;
//  uint16_t wid, ht;
//
//  tft.setFont(f);
//  tft.setCursor(x, y);
//  tft.setTextSize(sz);
//  tft.println(msg);
//}

uint8_t r = 255, g = 255, b = 255;
uint16_t color;

void setup()
{
  // Voltmeter setup
  pinMode(analogInput, INPUT); //assigning the input port
  Serial.begin(9600); //BaudRate
  //int16_t x1, y1;
  //uint16_t wid, ht;

  Serial.begin(9600);
  // Screen setup
  uint16_t ID = tft.readID();
  tft.begin(ID);
  tft.invertDisplay(false);
  tft.setRotation(1);
  tft.fillScreen(WHITE);
}

void loop(void)
{

  int x = 1;
  int fade = 10;
  float rise = .03;
  //randNumber = random(400);

  //tft.drawRGBBitmap(100, 50, test, 350, 200);
  //
  //tft.drawBitmap(350, 70, volthead, 162, 80);
  tft.drawBitmap(0, 250, header, 160, 92, RED);
  tft.drawBitmap(320, 0, footer, 160, 80, BLUE);
  tft.drawBitmap(20, 20, extra, 63, 61, ORANGE);
  tft.drawBitmap(150, 300, extra, 63, 61, BLUE);
  tft.drawBitmap(460, 210, extra, 63, 61, RED);
  
  for (int i = 0; i > -1; i = i + x) {

    tft.setTextSize(14);
    tft.setCursor(40, 110);
    //draw a filled black 10 by 10 rectangle at (100,20) to erase the old text
    color = tft.color565(r -= fade, g -= fade, b -= fade);    //tft.setTextColor(color);
    tft.setTextColor(color, WHITE);
    //val = analogRead(analogInput);//reads the analog input
    //Vout = (val * 5.00) / 1024.00; // formula for calculating voltage out i.e. V+, here 5.00
    //Vin = Vout / (R2/(R1+R2)); // formula for calculating voltage in i.e. GND
    //if (Vin<0.09)//condition
    //{
    // Vin=0.00;//statement to quash undesired reading !
    //}
    randNumber = random(4000000000000, 4000000006000);

    Vin = voltNumber += rise;
    tft.print(Vin);
    tft.print("V");

    tft.setCursor(40, 225);

    tft.setTextSize(2);
    tft.setTextColor(color, WHITE);

    tft.print("BACTERIAL POPULATION:");
    tft.print(randNumber);

    if (i == 20 && x == 1) {
      x = -1;
      fade = -10;
      rise = -.03;// switch direction at peak
    } else if (i == 0 && x == -1) {
      x = 1;
      fade = 10;
      rise = .03;
      voltNumber = random(3.22, 3.68);
    }


    Serial.println(randNumber);
    delay(10);
  }
  while (1);

}
