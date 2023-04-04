using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadJSON : MonoBehaviour
{
    public TextAsset textJSON;
    public TextMeshProUGUI textElement;
    public float textAnimSpeed = 0.04f;
    
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
        //textElement.text = rMessage.message;
        StartCoroutine(TypeWrite(rMessage.message));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //new 
    private IEnumerator TypeWrite(string displayText)
    {
        textElement.text = "";

        foreach(char letter in displayText.ToCharArray())
        {
            textElement.text += letter;
            yield return new WaitForSeconds(textAnimSpeed);
        }
    }
}
