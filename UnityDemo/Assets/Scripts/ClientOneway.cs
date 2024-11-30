using UnityEngine;
using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Threading;
using System.Collections.Concurrent;
using System.Diagnostics;

public class Receiver
{
    private Thread recieveThread;
    private bool isRunning = false;

    public Receiver()
    {
        recieveThread = new Thread((object callbackObj) =>
        {
            var callback = (Action<string>)callbackObj;
            using (var socket = new PullSocket())
            {
                socket.Connect("tcp://localhost:5555");

                while (isRunning)
                {
                    string message = socket.ReceiveFrameString();
                    callback(message); // callback is a function that will be passed in Start method.
                }
            }
        });
    }

    public void Start(Action<string> callback)
    {
        isRunning = true;
        recieveThread.Start(callback);
    }

    public void Stop()
    {
        isRunning = false;
        recieveThread.Join();
    }
}

public class ClientOneway : MonoBehaviour
{
    private ConcurrentQueue<Action> runOnMainThread = new ConcurrentQueue<Action>();
    private Receiver reciever;

    public void Start()
    {
        AsyncIO.ForceDotNet.Force(); // Ensure NetMQ is properly initialized

        reciever = new Receiver();
        reciever.Start((string message) =>
        {
            runOnMainThread.Enqueue(() =>
            {
                UnityEngine.Debug.Log(message); // Queue up the message to be logged to the console.
            });
        });
    }

    public void Update()
    {
        if (!runOnMainThread.IsEmpty)
        {
            while (runOnMainThread.TryDequeue(out Action action))
            {
                action.Invoke();
            }
        }
    }

    public void OnDestroy()
    {
        reciever.Stop();
    }
}
