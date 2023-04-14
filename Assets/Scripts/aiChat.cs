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

    public string message; 

    private Color textColor = new Color(0f, 255f, 0f, 255f);

    public GameObject master;
    
    public void pushMessage()
    {
            message = master.GetComponent<WebsocketReceiver>().rMessage.message.message_text;
            
            GameObject newText = Instantiate(prefab, parent.transform);

            newText.GetComponent<TMP_Text>().text = message;
            //newText.GetComponent<TMP_Text>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);

            newText.GetComponent<TMP_Text>().color = Random.ColorHSV(0.33f, 0.3f, 0.9f, 1f, 1f, 1f);
            texts.Add(newText);
            textLengths.Add(Mathf.Ceil(newText.GetComponent<TMP_Text>().text.Length / 62) + 1);

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
    
                texts[i].transform.position += Vector3.up*(0.1f*factor);

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

}
