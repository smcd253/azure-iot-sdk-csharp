#include <ESP8266WiFi.h>
#include <WiFiClient.h>

// Parametros de conexion a red WIFI.
const char* ssid = "bcaj";
const char* password = "aaaaaaaaa1";

// Start a TCP Server on port 13000
WiFiServer server(13000);

void setup() {
  Serial.begin(115200);
  WiFi.begin(ssid,password);
  Serial.println("");
  //Wait for connection
  while(WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
 Serial.println("My IP: ");
  Serial.println(WiFi.localIP()); // IP

  // Start the TCP server
  server.begin();
}

void loop() {
  TCPServer();
}

void TCPServer () {
   WiFiClient client = server.available();
    if (client) {
      Serial.println("We have a new client");
      Serial.println("Hello, client!");
        if (client.available() > 0) {
            char thisChar = client.read();
            Serial.println("We got data");
            Serial.println(thisChar);
            delay(200);
        }
    }
}
