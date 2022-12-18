using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wave.Essence.Hand.StaticGesture;

public class ParticleTest : MonoBehaviour
{
    public ParticleSystem pa;
    public bool isleft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(WXRGestureHand.GetSingleHandGesture(isleft) == "IndexUp")
        {
            if (pa.isPaused)
            {
                pa.Play();
            }
        }
        else
        {
            if (pa.isPlaying)
            {
                pa.Pause();
            }
        }
    }
}
