using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class debug : MonoBehaviour
{

    public GameObject DebugMenu;

    public TMP_InputField IDInput;

    private bool isDebugMenu;

    void Start(){
        isDebugMenu = false;
        //canvasComponent =  DebugMenu.GetComponent<Canvas>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            print ("debug");
            isDebugMenu = !isDebugMenu;

            //canvasComponent.enabled;

            if(isDebugMenu == true){
                //IDInput.transform.localScale = new Vector3(1f, 1f, 1f);
                DebugMenu.SetActive(true);
                IDInput.ActivateInputField();
            }
            else 
            {
                //IDInput.transform.localScale = new Vector3(0f, 0f, 0f);
                DebugMenu.SetActive(false);
                IDInput.DeactivateInputField();
            }
            

        }
    }
}
