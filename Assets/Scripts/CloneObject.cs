using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class chat : MonoBehaviour
{
    public GameObject prefab;
    public GameObject parent;
    public float colorFadeFactor;

    public List <GameObject> texts;
    public List <float> textLengths;

    public string message; 

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {

            GameObject newText = Instantiate(prefab, parent.transform);
 
            newText.GetComponent<TMP_Text>().text = message;

            newText.GetComponent<TMP_Text>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);

            texts.Add(newText);

            textLengths.Add(Mathf.Floor(newText.GetComponent<TMP_Text>().text.Length / 62) + 1);

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
           }

        }
    }

}
