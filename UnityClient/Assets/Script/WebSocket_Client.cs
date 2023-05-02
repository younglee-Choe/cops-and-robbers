using System.Collections;
using System.Collections.Generic;
using WebSocketSharp;
using UnityEngine;

public class WebSocket_Client : MonoBehaviour
{
    WebSocket ws;
    void Start()
    {
        ws = new WebSocket("ws://localhost:8080");
        ws.Connect();
        ws.OnMessage += (sender, e) => {
            Debug.Log("Message received from" + ((WebSocket)sender).Url + ", Data: " + e.Data);
        };
        ws.Connect();
    }

    void Update()
    {
        if(ws == null) {
            return;
        }
        
        if(Input.GetKeyDown(KeyCode.Space)) {
            ws.Send("The title of this Game is <Cops and Robbers>");
        }
    }
}
