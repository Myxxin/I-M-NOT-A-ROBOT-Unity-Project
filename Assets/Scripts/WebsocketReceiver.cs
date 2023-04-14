using WebSocketSharp;
using UnityEngine;
using TMPro;

public class WebsocketReceiver : MonoBehaviour
{
    WebSocket ws;
    public GameObject IDtext;

    public GameObject incomingChat;
    public GameObject outgoingChat;


    [System.Serializable]
    public class Message
    {
        public string message_type;
        public string message_text;
        public int speaker;
    }

    [System.Serializable]
    public class ReceivedMessage
    {
        public string source;
        public string destination;
        public int booth_id;
        public int speaker;
        public Message message;

    }

    public class RegistrationMessageFromUnity
    {
        public int booth_id;
        public string source;
    }

    public class StateMessageFromUnity{
        public int booth_id;
        public string source;
        public string state; 
    }

    public ReceivedMessage rMessage = new ReceivedMessage();

    public RegistrationMessageFromUnity unityMessage = new RegistrationMessageFromUnity();
    public StateMessageFromUnity unityStateMessage = new StateMessageFromUnity();



    // Start is called before the first frame update
    void Start()
    {
        print(incomingChat.GetComponent<aiChat>().message);
        // unityStateMessage.booth_id = 1;
        // unityStateMessage.source = "unity";
        // unityStateMessage.state = "intro_finished";

        //unityMessage.booth_id = 1;
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


            


        };

        ws.Connect();
    }

    // Update is called once per frame
    void Update()
    {

        switch (rMessage.message.message_type)
            {

                case "raw_text":
                    Debug.Log("It is raw text");
                    HandleRawText(rMessage);
                    break;

                case "final_text":
                    Debug.Log("It is final text");
                    //if speaker 1 or 2
                    if(rMessage.message.speaker==1){
                        HandleFinalTextIncoming(rMessage);
                    }else if(rMessage.message.speaker==2){
                        HandleFinalTextOutgoing(rMessage);
                    }
                    
                    rMessage.message.message_type="";
                    break;

                case "phase_change":
                    Debug.Log("It is a phase change");
                    HandlePhaseChange(rMessage);
                    break;

                default:
                    Debug.Log("Nothing");
                    break;
            }
        


        if (ws == null)
        {
            return;
        }

        //send message
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string IDString = IDtext.GetComponent<TMP_Text>().text;
            unityMessage.booth_id = 1;
            ws.Send(JsonUtility.ToJson(unityMessage));
            Debug.Log("Message send!");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            
            ws.Send(JsonUtility.ToJson(unityStateMessage));
            Debug.Log("Message 2 send!");
        }
    }

    public void HandleRawText(ReceivedMessage rMessage)
    {
        return;
    }

    public void HandleFinalTextIncoming(ReceivedMessage rMessage)
    {           
        incomingChat.GetComponent<aiChat>().pushMessage();
        return;
    }

      public void HandleFinalTextOutgoing(ReceivedMessage rMessage)
    {
        outgoingChat.GetComponent<aiChat>().pushMessage();
        return;
    }

    public void HandlePhaseChange(ReceivedMessage rMessage)
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