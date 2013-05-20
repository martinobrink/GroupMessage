Introduction
============

GroupMessage is a solution for the scenario where you want to be able to 
create ad-hoc groups between people and be able to broadcast messages to 
all users across several different channels (Text messages, push 
notifications, email etc...).

The vision of the project is to "make it Doodle-easy to create a group
and enable users to join (without registration/login)".

The solution is .NET 4.0 based and makes use of the Nancy web framework
for the server, and AngularJS for the website.

So far a smartphone client app for Android has also been created. This app
is developed using Xamarin Studio (the free version).

The server should run on almost any .NET/Mono enabled device. In fact, 
it is currently running on a Raspberry Pi.

Prerequisites
============
- .NET 4.0 / Mono 2.10
- A running MongoDB server
- For sending text messages using Twilio, account details must be placed in 
the missing 'Twilio.txt' file within the GroupMessage.Server directory (see 
below for details on the format of the contents in this file).
- For sending Android push notifications using Google Cloud Messaging (GCM),
account details must be placed in the missing 'GooglePushNotifications.txt' 
file within the GroupMessage.Server directory (see below for details on the 
format of the contents in this file).

Running
============
Run the solution and point a browser at

http://localhost:8282/

to verify that everything is working

Account details
===============
The Twilio account details must be placed in the missing Twilio.txt file.
This file should be placed directly within the GroupMessage.Server directory.
The format of the contents within this file should be the following 3 lines:

AccountSID=VH1n387j36d52248e7f62b052a475482a3
AuthToken=34as3f0425b2413fd9d08ca4cd142x25
SenderNumber=+1987-654-3210

Obviously these specific values do not work and should be replaced with your 
own values obtained from Twilio.

For sending push notifications to Android devices using Google's GCM service,
the Google account details must be placed in the missing 
GooglePushNotifications.txt file. This file should also be placed directly 
within the GroupMessage.Server directory.
The format of the contents within this file should be the following 3 lines:

APIKey=FJehJbbGMqTThsCKtEF8BXhkUmwuIKLcYZpkvu9mB
SenderId=123456789123
PackageName=com.your.package.name

Again, these specific values do not work and should be replaced with your 
own values obtained from Google Apis Console.

Remember to also set these values in the source code within the Android 
client app so the server and the client are connected by using the same keys.