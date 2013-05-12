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

Running
============
Run the solution and point a browser at

http://localhost:8282/

to verify that everything is working