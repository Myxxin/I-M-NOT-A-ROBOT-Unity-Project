using WebSocketSharp;
using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class WebsocketReceiver : MonoBehaviour
{
    WebSocket ws;
    public GameObject IDtext;
    public GameObject cam;
    public GameObject ErrorVideo;
    

    public GameObject incomingChat;
    public GameObject outgoingChat;

    public GameObject IdlePrefab;
    public GameObject HipSocialPrefab;
    public GameObject convoPrefab;

    private bool _isRegistered;


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

    [System.Serializable]
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

    public RegistrationMessageFromUnity unityRegistrationMessage = new RegistrationMessageFromUnity();
    public StateMessageFromUnity unityStateMessage = new StateMessageFromUnity();



    // Start is called before the first frame update
    void Start()
    {
        StartIdle();
        
        _isRegistered = false;
        //======= SETUP UNITY MESSAGES =======\\
        //Manual Setup in UI
        /*unityRegistrationMessage.booth_id = 1;*/
        unityRegistrationMessage.source = "unity";
        
        unityStateMessage.booth_id = unityRegistrationMessage.booth_id;
        unityStateMessage.source = unityRegistrationMessage.source;
        unityStateMessage.state = "intro_finished";

        //======= CONNECT WITH TEST WEBSOCKET =======\\
        //URL: https://socketsbay.com/test-websockets
        //ws = new WebSocket("wss://socketsbay.com/wss/v2/1/demo/");

        //=======CONNECT WITH OTHER MAC=======\\
        ws = new WebSocket("ws://10.0.0.15:8765");
        Debug.Log("Websocket created");

        //======= LISTEN TO MESSAGES =======\\
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

        if (ws == null)
        {
            return;
        }

        SendRegistration();

        //======= HANDLE MESSAGES =======\\
        switch (rMessage.message.message_type)
            {

                case "raw_text":
                    Debug.Log("It is raw text");
                    HandleRawText(rMessage);
                    CleanMessage(rMessage);
                    break;

                case "final_text":
                    Debug.Log("It is final text");
                    
                    //Speaker 1: Incoming, Speaker 2: Outgoing -- has to be different for each booth!
                    if(rMessage.message.speaker==1){
                        HandleFinalTextIncoming(rMessage);
                    }else if(rMessage.message.speaker==2){
                        HandleFinalTextOutgoing(rMessage);
                    }
                    
                    CleanMessage(rMessage);
                    break;

                case "phase_change":
                    Debug.Log("It is a phase change");
                    HandlePhaseChange(rMessage);
                    CleanMessage(rMessage);
                    break;

                default:
                    //Debug.Log("Nothing");
                    CleanMessage(rMessage);
                    break;
            }

        //=== DEBUG ONLY: Manually send message ===\\
       /* if (Input.GetKeyDown(KeyCode.Space))
        {
            //Send registration
            ws.Send(JsonUtility.ToJson(unityRegistrationMessage));
            Debug.Log("Message send!");
        }*/

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Send State of finished Intro
            //ws.Send(JsonUtility.ToJson(unityStateMessage));
            rMessage.message.message_type = "phase_change";
            rMessage.message.message_text = "play_intro";
            Debug.Log("Message 2 send!");
        }
    

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Send State of finished Intro
            //ws.Send(JsonUtility.ToJson(unityStateMessage));
            rMessage.message.message_type = "phase_change";
            rMessage.message.message_text = "start_var_intro";
        }

        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Send State of finished Intro
            //ws.Send(JsonUtility.ToJson(unityStateMessage));
            rMessage.message.message_type = "phase_change";
            rMessage.message.message_text = "start_conversation_mode";
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Send State of finished Intro
            //ws.Send(JsonUtility.ToJson(unityStateMessage));
            rMessage.message.message_type = "phase_change";
            rMessage.message.message_text = "other_booth_hungup";//"end_experience_hard_reset";
        }


    }

    public void SendRegistration(){
        if(!_isRegistered){
            ws.Send(JsonUtility.ToJson(unityRegistrationMessage));
            Debug.Log("REGISTRATION SENT");
            _isRegistered = true;
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

    public void StartIdle(){

        print("Idle started!");
        incomingChat.GetComponent<aiChat>().cleanUpChat();
        outgoingChat.GetComponent<aiChat>().cleanUpChat();
        IdlePrefab.SetActive(true);
        IdlePrefab.GetComponent<dissolveSDF>().resetLogo();
        cam.GetComponent<UnityEngine.Video.VideoPlayer>().enabled = false;
        ErrorVideo.GetComponent<UnityEngine.Video.VideoPlayer>().enabled = false;

        HipSocialPrefab.GetComponent<changeSDF>().switchToRose();
        HipSocialPrefab.SetActive(false);
        convoPrefab.SetActive(false);
        
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

        switch(rMessage.message.message_text){

            case "play_intro":
                Debug.Log("Start Intro");

                cam.GetComponent<UnityEngine.Video.VideoPlayer>().enabled=true;
                IdlePrefab.GetComponent<dissolveSDF>().dissolveLogo();
                StartCoroutine(PlayIntroVideo());
                StartCoroutine(StartHipLip());
                StartCoroutine(SendStateUpdate());


               
                break;

            case "start_var_intro":

                

                IdlePrefab.SetActive(false);
                Debug.Log("start_var_intro");
                HipSocialPrefab.GetComponent<changeSDF>().switchToPhone();
                break;

                

               

            case "start_conversation_mode":

                incomingChat.GetComponent<aiChat>().cleanUpChat();
                outgoingChat.GetComponent<aiChat>().cleanUpChat();

                Debug.Log("start_conversation_mode");
                HipSocialPrefab.GetComponent<changeSDF>().fade();
                convoPrefab.SetActive(true);

                break;
                 //Social Bot faded raus mit Audio, oder Hip Lip
                /*Entweder Hip Lip löst sich auf, oder der Social Bot.
                AI Blob kommt*/

            case "other_booth_hungup":
                Debug.Log("other_booth_hungup");
                ErrorVideo.GetComponent<UnityEngine.Video.VideoPlayer>().enabled = true;
                ErrorVideo.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
                break;


            case "end_experience_hard_reset":
                StartIdle();
                Debug.Log("end_experience_hard_reset");
                break;

        }


        return;
    }

    public void CleanMessage(ReceivedMessage rMessage){
        rMessage.source = "";
        rMessage.destination = "";
        rMessage.booth_id = 0;
        rMessage.message.message_type = "";
        rMessage.message.message_text = "";
        rMessage.message.speaker = 0;

        return;
    }

    IEnumerator PlayIntroVideo(){
        yield return new WaitForSeconds(5);
        cam.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
    }

    IEnumerator StartHipLip(){
        yield return new WaitForSeconds(6); //51
        print("HIP LIp IS COMING!");
        HipSocialPrefab.SetActive(true);
        HipSocialPrefab.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
    }

    IEnumerator SendStateUpdate(){
        yield return new WaitForSeconds(7); //79
         ws.Send(JsonUtility.ToJson(unityStateMessage));
        Debug.Log("INTRO DONE - STATE UPDATE SENT"); 

    }

}