using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class dissolveSDF : MonoBehaviour
{
    public GameObject LogoVFX;

    public GameObject sound;

    public void dissolveLogo()
    {
        LogoVFX.GetComponent<VisualEffect>().SetInt("maxLifeTime", 0);
        LogoVFX.GetComponent<VisualEffect>().SetFloat("zVelocity", 5);
        LogoVFX.GetComponent<VisualEffect>().SetFloat("intensity", 5);

        sound.GetComponent<UnityEngine.Video.VideoPlayer>().Play();


    }

    public void resetLogo(){

        LogoVFX.GetComponent<VisualEffect>().SetInt("maxLifeTime", 3);
        LogoVFX.GetComponent<VisualEffect>().SetFloat("zVelocity", 0);
        LogoVFX.GetComponent<VisualEffect>().SetFloat("intensity", 0.1f);


    }
}
