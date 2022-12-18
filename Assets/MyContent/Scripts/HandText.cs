using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wave.Essence.Hand.StaticGesture;

public class HandText : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Father";
    }

    // Update is called once per frame
    void Update()
    {
        text.text = WXRGestureHand.GetSingleHandGesture(true);
    }
}
