using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragforce : MonoBehaviour
{
    float currentPosition;
    bool direction;
    // Start is called before the first frame update
    void Start()
    {
      
    }
    // Update is called once per frame
    void Update()
    {   currentPosition = gameObject.transform.position.y;
        if (currentPosition < -5){
            direction = true; 
        }
        else if (currentPosition > 5){
            direction = false;
        } 

        if(direction == true){
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, currentPosition + Random.Range(-0.01f, 0.2f), gameObject.transform.position.z);  
        }
        else if(direction == false){
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, currentPosition - Random.Range(-0.01f, 0.2f), gameObject.transform.position.z);  
        }
    }
}
