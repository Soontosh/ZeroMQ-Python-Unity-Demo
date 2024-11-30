# ZeroMQ Demo in Unity
This is a simple demo of ZeroMQ in Unity with Python. It uses the ZeroMQ library for one-way communication between Unity and Python. The Python script sends a JSON message to Unity, and Unity Debug.Logs the message.

## Quick Start
1) Install ZeroMQ for Python
```
pip install pyzmq
```
2) Install ZeroMQ for Unity through NuGet. You can install NuGet through the [NuGetForUnity GitHub page](https://github.com/GlitchEnzo/NuGetForUnity/releases/). Run the `.unitypackage` file to install NuGet for Unity.
3) Install `System.ServiceModel.Syndication` through NuGet.
4) Open the Unity project and run the environment.
5) Run the Python script.

You should see messages being sent from Python to Unity. The Python script sends 100 JSON messages to Unity with a 1-second delay between each message. Unity recieves these messages and logs them to the console.

## Support
The Unity project was created in Unity version 6 (6000.0.29f1 LTS). The code was tested on both Windows and Linux (Ubunutu), and should work on MacOS as well.