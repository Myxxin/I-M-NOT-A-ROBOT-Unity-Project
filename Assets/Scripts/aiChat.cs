using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class aiChat : MonoBehaviour
{
    public GameObject prefab;
    public GameObject parent;
    public float colorFadeFactor;

    public List <GameObject> texts;
    public List <float> textLengths;
    private float textLength;

    public string message; 

    private Color textColor = new Color(0f, 255f, 0f, 255f);

    public GameObject master;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            //pushMessage();
            //print("key");
        }
    }
    
    public void pushMessage()
    {
            message = master.GetComponent<WebsocketReceiver>().rMessage.message.message_text;
            
            GameObject newText = Instantiate(prefab, parent.transform);

            newText.GetComponent<TMP_Text>().text = message;
            //newText.GetComponent<TMP_Text>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);

            newText.GetComponent<TMP_Text>().color = Random.ColorHSV(0.33f, 0.3f, 0.9f, 1f, 1f, 1f);
            texts.Add(newText);
            textLength = Mathf.Round(newText.GetComponent<TMP_Text>().text.Length / 23); //original: ...+1

            if (newText.GetComponent<TMP_Text>().text.Length - textLength >= 0)
            {
                textLength += 2;
            }

            textLengths.Add(textLength + 1);

            for ( int i = 0; i < texts.Count -1; i++)
            {

                float factor;
                if (i == texts.Count -1)
                {
                    factor = 0;
                }
                else
                {
                    factor = textLengths[texts.Count -1];    
                }
    
                texts[i].transform.position += new Vector3 (0f, 0.04f, 0f)*factor - new Vector3 (0f, 0.005f, 0f)*(factor);

                for (float u = 0; u < factor; u++){texts[i].GetComponent<TMP_Text>().color *= colorFadeFactor;}

                if (texts.Count >  14)
                {   
                    Destroy(texts[0]);
                    texts.RemoveAt(0);
                    textLengths.RemoveAt(0);

                }

           }

        
    }

    public void test(){
        Debug.Log("WHAT THE SHIT?!");
        print("Master Text: " + master.GetComponent<WebsocketReceiver>().rMessage.message.message_text);
        message = master.GetComponent<WebsocketReceiver>().rMessage.message.message_text;

        pushMessage();
    }

    public void cleanUpChat()
    {
       
        int numberOfTexts = texts.Count;
         print(">> Text Count: " + numberOfTexts);
        
        for(int i = 0; i < numberOfTexts; i++)
        {
            Destroy(texts[i]);
            texts.RemoveAt(i);
            textLengths.RemoveAt(i);
        }
        print("CHAT CLEANED");
        print(">> Text Count: " + texts.Count);

    }

}
