using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadJSON : MonoBehaviour
{
    public TextAsset textJSON;
    public TextMeshProUGUI textElement;
    
    [System.Serializable]

    public class ReceivedMessage
    {
        public string messageType;
        public string source;
        public int chronoID;
        public string message;
    }

    //[System.Serializable]

    public ReceivedMessage rMessage = new ReceivedMessage();
    // Start is called before the first frame update
    void Start()
    {
        rMessage = JsonUtility.FromJson<ReceivedMessage>(textJSON.text);
        textElement.text = rMessage.message;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
