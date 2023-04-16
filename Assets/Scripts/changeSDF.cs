using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class changeSDF : MonoBehaviour
{
    public Texture3D PhoneSdf;
    public Texture3D RoseSdf;

    public GameObject parentVFX;
    public GameObject mainVFX;
    public GameObject emitterVFX;

    

    private bool transform = false;
    private int timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (transform == true && timer < 200)
        {   
            parentVFX.transform.rotation = Quaternion.Lerp(parentVFX.transform.rotation, Quaternion.Euler(new Vector3(48.8f, 75.21f, 92.7f)), 0.01f);
            parentVFX.transform.position = Vector3.Lerp(parentVFX.transform.position, new Vector3(0f, 0f, 3.42f), 0.01f);
            timer++;

        }
        
    }

    public void switchToPhone(){

        mainVFX.GetComponent<VisualEffect>().SetTexture("SDF", PhoneSdf);

        emitterVFX.GetComponent<VisualEffect>().SetTexture("SDF", PhoneSdf);
        emitterVFX.GetComponent<VisualEffect>().SetFloat("YForce", 0);
        emitterVFX.GetComponent<VisualEffect>().SetFloat("SpawnMultiplicator", 150000);


        

        transform = true;

    
    }

    public void switchToRose(){
        mainVFX.GetComponent<VisualEffect>().SetTexture("SDF", RoseSdf);

        emitterVFX.GetComponent<VisualEffect>().SetTexture("SDF", RoseSdf);

        emitterVFX.GetComponent<VisualEffect>().SetFloat("YForce", 5);
        emitterVFX.GetComponent<VisualEffect>().SetFloat("SpawnMultiplicator", 75000);

        parentVFX.transform.position = new Vector3 (0f, -0.27f, 3.42f);
        parentVFX.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        mainVFX.GetComponent<VisualEffect>().SetInt("maxLifeTime", 2);
        mainVFX.GetComponent<VisualEffect>().SetInt("minLifeTime", 1);
        emitterVFX.GetComponent<VisualEffect>().SetInt("maxLifeTime", 2);
        emitterVFX.GetComponent<VisualEffect>().SetInt("minLifeTime", 1);

        timer = 0;

        transform = false; 



    }

    public void fade()
    {
        mainVFX.GetComponent<VisualEffect>().SetInt("maxLifeTime", 0);
        mainVFX.GetComponent<VisualEffect>().SetInt("minLifeTime", 0);
        emitterVFX.GetComponent<VisualEffect>().SetInt("maxLifeTime", 0);
        emitterVFX.GetComponent<VisualEffect>().SetInt("minLifeTime", 0);
    }
}
