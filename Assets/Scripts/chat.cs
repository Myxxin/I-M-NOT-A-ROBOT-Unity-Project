using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CloneObject : MonoBehaviour
{
    public GameObject prefab;
    public GameObject parent;
    public float colorFadeFactor;

    public List <GameObject> texts;
    public List <float> textLengths;

    public string testString; 


    void Start()
    {
        for (var i = 1; i < 10; i++)
        {
            //Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity, parent, false);

            //GameObject obj = Instantiate(prefab, parent.transform);
            //obj.transform.position += Vector3.up*i/10;
            //obj.GetComponent<TMP_Text>().text = "hipp lipp" + " " + i;
            //obj.GetComponent<TMP_Text>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);;
        
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            //print("space key was pressed");
            //print (testString.Length);

            //GameObject[] texts = GameObject.FindGameObjectsWithTag("text");

            //print(texts.Length);

            //float factor = 0;

            GameObject obj = Instantiate(prefab, parent.transform);
            //obj.transform.position += Vector3.up*0.1f;
            obj.tag = "text";
            obj.GetComponent<TMP_Text>().text = testString;
            obj.GetComponent<TMP_Text>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            texts.Add(obj);
            textLengths.Add(Mathf.Floor(obj.GetComponent<TMP_Text>().text.Length / 62) + 1);

            //print(texts.Count);

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
