// bewegingsensor

#include <Arduino.h>


void setup() {
  Serial.begin(9600);// setup Serial Monitor to display information
  pinMode(2, INPUT);// Input from sensor
  pinMode(8, OUTPUT);// OUTPUT to alarm or LED
}

void loop() {
  int motion =digitalRead(2);
  if(motion){
    Serial.println("Motion detected");
    digitalWrite(8,HIGH);
  }else{
     Serial.println("===nothing moves");
     digitalWrite(8,LOW);
  }
  delay(200);

}

// temperatuur sensor

#include <DHT.h>

#define DHTPIN 2     // Digital pin connected to the DHT sensor

#define DHTsensor DHT11   // DHT 11

DHT dht(DHTPIN, DHTsensor);

void setup() {
  Serial.begin(9600);
  Serial.println(F("DHTxx test!"));

  dht.begin();
}

void loop() {
  delay(2000);

  float h = dht.readHumidity();
  float t = dht.readTemperature();

  if (isnan(h) || isnan(t)) {
    Serial.println(F("Failed to read from DHT sensor!"));
    return;
  }

  Serial.print(F("Humidity: "));
  Serial.print(h);
  Serial.print(F("%  Temperature: "));
  Serial.print(t);
  Serial.println(F("°C "));
}




#include <ESP8266WiFi.h>        // Include the Wi-Fi library

#define SERVER "MYSERVER"
#define SERVERPORT 5000

const char* ssid     = "linksys07258";         // The SSID (name) of the Wi-Fi network you want to connect to
const char* password = "zjkmlr2p41";     // The password of the Wi-Fi network

void setup() {
  Serial.begin(115200);         // Start the Serial communication to send messages to the computer
  delay(10);
  Serial.println('\n');
  
  WiFi.begin(ssid, password);             // Connect to the network
  Serial.print("Connecting to ");
  Serial.print(ssid); Serial.println(" ...");

  int i = 0;
  while (WiFi.status() != WL_CONNECTED) { // Wait for the Wi-Fi to connect
    delay(1000);
    Serial.print(++i); Serial.print(' ');
  }

  Serial.println('\n');
  Serial.println("Connection established!");  
  Serial.print("IP address:\t");
  Serial.println(WiFi.localIP());         // Send the IP address of the ESP8266 to the computer
}


WiFiClient client;

void sendData(unsigned long timestamp, double speed) 
{   
    // Host and port
    if(client.connect(SERVER, SERVERPORT))
    {
        char body[64];

        // Clear the array to zeroes.
        memset(body, 0, 64);
        
        // Arduino sprintf does not support floats or doubles.
        sprintf(body, "milliseconds=%lu&speed=", timestamp); 
        // Use the dtostrf to append the speed.
        dtostrf(speed, 2, 3, &body[strlen(body)]);

        int bodyLength = strlen(body);

        // Specify the endpoint
        client.println("POST /data/providereading HTTP/1.1");
        
        // Write Host: SERVER:SERVERPORT
        client.print("Host: "); 
        client.print(SERVER);
        client.print(":");
        client.println(SERVERPORT);

        // Close the connection after the request
        client.println("Connection: close");

        // Write the amount of body data
        client.print("Content-Length: ");
        client.println(bodyLength);

        client.println("Content-Type: application/x-www-form-urlencoded");
        client.println();

        client.print(body);

        // Wait for the response
        delay(100);

        // Read the response (but we don't care what is in it)
        while(client.read() != -1);
    }
}
void loop() { }