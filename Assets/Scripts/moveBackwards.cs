using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBackwards : MonoBehaviour
{
    float currentPosition;
    public float objectTransformRate;
    public float distance;
    public float audioDivisionRate;

    public AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = gameObject.transform.position.z;

        if (currentPosition < distance){
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 
            gameObject.transform.position.y, 
            currentPosition + objectTransformRate);
            audioSource.volume *= audioDivisionRate;
        }
        
    }
}
