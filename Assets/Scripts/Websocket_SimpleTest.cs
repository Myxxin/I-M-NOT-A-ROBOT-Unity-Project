using WebSocketSharp;
using UnityEngine;

public class Websocket_SimpleTest : MonoBehaviour
{
    WebSocket ws;

    [System.Serializable]
    public class Message
    {
        public string message_type;
        public string message_text;
    }

    [System.Serializable]
    public class ReceivedMessage
    {
        public string source;
        public string destination;
        public int booth_id;
        public Message message;

    }

    public class MessageFromUnity
    {
        public int booth_id;
        public string source;
    }

    public ReceivedMessage rMessage = new ReceivedMessage();

    public MessageFromUnity unityMessage = new MessageFromUnity();
    // Start is called before the first frame update
    void Start()
    {

        unityMessage.booth_id = 1;
        unityMessage.source = "unity";

        //For Testing
        //URL: https://socketsbay.com/test-websockets
         ws = new WebSocket("wss://socketsbay.com/wss/v2/1/demo/");

        //For connecting to other mac
        //ws = new WebSocket("ws://10.0.0.15:8765");


        Debug.Log("Websocket created");

        //listening for a message
        ws.OnMessage += (sender, data) =>
        {
            Debug.Log("Icoming message from: " + ((WebSocket)sender).Url + ", Data: " + data.Data);

            rMessage = JsonUtility.FromJson<ReceivedMessage>(data.Data);


            switch (rMessage.source)
            {

                case "raw_text":
                    Debug.Log("It is raw text");
                    HandleRawText(rMessage);
                    break;

                case "final_text":
                    Debug.Log("It is final text");
                    HandleFinalText(rMessage);
                    break;

                case "phase_change":
                    Debug.Log("It is a phase change");
                    HandlePhaseChange(rMessage);
                    break;

                default:
                    Debug.Log("Nothing");
                    break;
            }


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
            //ws.Send("{"booth_id": booth_id, "source": source}");
            ws.Send(JsonUtility.ToJson(unityMessage));
            Debug.Log("Message send!");
        }
    }

    private void HandleRawText(ReceivedMessage rMessage)
    {
        return;
    }

    private void HandleFinalText(ReceivedMessage rMessage)
    {
        return;
    }

    private void HandlePhaseChange(ReceivedMessage rMessage)
    {
        /* STATES:
        * start_intro, 
        * stop_intro, 
        * start_variable_intro, 
        * stop_variable_intro, 
        * start_conversation_mode, 
        * stop_conversation_mode, 
        * start_revelation_phase, 
        * stop_revelation_phase
        */

        /*switch("start_intro"){

            case "start_intro":
                Debug.Log("Start Intro");
                break;

            case "stop_intro":
                Debug.Log("stop_intro");
                break;

            case "start_variable_intro":
                Debug.Log("start_variable_intro");
                break;

            case "stop_variable_intro":
                Debug.Log("stop_variable_intro");
                break;

            case "start_conversation_mode":
                Debug.Log("start_conversation_mode");
                break;

            case "stop_conversation_mode":
                Debug.Log("stop_conversation_mode");
                break;

            case "start_revelation_phase":
                Debug.Log("start_revelation_phase");
                break;

            case "stop_revelation_phase":
                Debug.Log("stop_revelation_phase");
                break;

        }*/


        return;
    }

}
