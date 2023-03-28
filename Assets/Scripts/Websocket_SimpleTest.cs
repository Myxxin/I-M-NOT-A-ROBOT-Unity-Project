using WebSocketSharp;
using UnityEngine;

public class Websocket_SimpleTest : MonoBehaviour
{
    WebSocket ws;
    // Start is called before the first frame update
    void Start()
    {
        //URL: https://socketsbay.com/test-websockets
        ws = new WebSocket("wss://socketsbay.com/wss/v2/1/demo/");
        Debug.Log("Websocket created");

        //listening for a message
        ws.OnMessage += (sender, data) =>
        {
            Debug.Log("Icoming message from: " + ((WebSocket)sender).Url + ", Data: " + data.Data);
        };

        ws.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        if (ws == null)
        {
            return;
        }

        //send message
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ws.Send("Hello from Unity");
        }
    }
}
