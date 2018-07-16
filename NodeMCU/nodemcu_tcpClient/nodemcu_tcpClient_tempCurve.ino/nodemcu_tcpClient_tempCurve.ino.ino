/*
 *  This sketch sends data via HTTP GET requests to data.sparkfun.com service.
 *
 *  You need to get streamId and privateKey at data.sparkfun.com and paste them
 *  below. Or just customize this script to talk to other HTTP servers.
 *
 */

#include <ESP8266WiFi.h>
#include <WiFiClient.h>


int temp();

const char* ssid     = "Foxtrot";
const char* password = "8888aaaa";

const char* host = "10.0.0.37";
const int port = 13000;

const char* streamId   = "....................";
const char* privateKey = "....................";

void setup() {
  Serial.begin(115200);
  delay(10);

  // We start by connecting to a WiFi network

  Serial.println();
  Serial.println();
  Serial.print("Connecting to ");
  Serial.println(ssid);
  
  /* Explicitly set the ESP8266 to be a WiFi-client, otherwise, it by default,
     would try to act as both a client and an access-point and could cause
     network-issues with your other WiFi-devices on your WiFi-network. */
  WiFi.mode(WIFI_STA);
  WiFi.begin(ssid, password);
  
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }

  Serial.println("");
  Serial.println("WiFi connected");  
  Serial.println("My IP address: ");
  Serial.println(WiFi.localIP());
}


double ticker = 0.00;
double delaySecs = 1.0;
void loop() {
  delay((int)delaySecs * 1000);

  double temp = temperatureGenerator();
  
  Serial.print("connecting to ");
  Serial.println(host);
  
  // Use WiFiClient class to create TCP connections
  WiFiClient client;
  if (!client.connect(host, port)) {
    Serial.println("connection failed");
    return;
  }

  String toPrint = "temp: " + String(temp);
  Serial.println(toPrint);
  client.print(toPrint);
  
  unsigned long timeout = millis();
  while (client.available() == 0) {
    if (millis() - timeout > 100) {
      Serial.println(">>> Client Timeout !");
      client.stop();
      return;
    }
  }
  
  //Read all the lines of the reply from server and print them to Serial
  while(client.available()){
    String line = client.readStringUntil('\r');
    Serial.print(line);
  }
  
  Serial.println();
  Serial.println("closing connection");
}

double temperatureGenerator(){
  if(ticker < 3.1459){ // 
    ticker += 0.01;
  }
  else{
    ticker = 0.00;
  }
  
  double minTemp = 32 + 30*sin(ticker);
  double maxTemp = 46 + 30*sin(ticker);
  
  double temp = random(minTemp, maxTemp);
  return temp;
}

