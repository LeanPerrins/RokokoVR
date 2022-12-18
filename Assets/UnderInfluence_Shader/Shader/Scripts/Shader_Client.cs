using System;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.Hub;

public class Shader_Client : MonoBehaviourPunCallbacks, IPunObservable
{
    private PhotonView pv;
    public float local_fade12;
    
    private float local_fade34;
    private float local_fade12_34;

    private float local_1_threshold;
    private float local_2_threshold;
    private float local_3_threshold;
    private float local_4_threshold;
    
    private float local_deform = 2f;
    private float local_deform_time = 0.5f;
    private float local_deform_scale = 4.5f;


    // Update is called once per frame
    void Update()
    {
        //Control & Sync main fader
        gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Fader_1_2", local_fade12);

        gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Fader_3_4", local_fade34);

        gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Fader_1_2_3_4", local_fade12_34);
       
        gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_TimeSpeed", local_deform_time);
        gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_NoiseScale", local_deform_scale);

       //Control & Sync state fader
       if (local_fade12_34 == 0)
       {
           if (local_fade12 == 0)
           {
               gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_1_Threshold", local_1_threshold);
           }

           if (local_fade12 == 1)
           {
               gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_2_Threshold", local_2_threshold);
           }
       }

       if (0f < local_fade12_34 && local_fade12_34 < 1f)
       {
           gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Threshold_Deform", local_deform); 
       }

       if (local_fade12_34 == 1)
       {
           if (local_fade34 == 0)
           {
               gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_3_Threshold", local_3_threshold);

               gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Threshold_Deform", local_3_threshold);
           }

           if (local_fade34 == 1)
           {
               gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_4_Threshold", local_4_threshold);

               gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Threshold_Deform", local_4_threshold);
           }

       }


    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(local_fade12);
            stream.SendNext(local_fade34);
            stream.SendNext(local_fade12_34);
            stream.SendNext(local_1_threshold);
            stream.SendNext(local_2_threshold);
            stream.SendNext(local_deform);
            stream.SendNext(local_3_threshold);
            stream.SendNext(local_4_threshold);
            stream.SendNext(local_deform_time);
            stream.SendNext(local_deform_scale);
        }
        else
        {
            this.local_fade12 = (float)stream.ReceiveNext();
            this.local_fade34 = (float)stream.ReceiveNext();
            this.local_fade12_34 = (float)stream.ReceiveNext();
            this.local_1_threshold = (float)stream.ReceiveNext();
            this.local_2_threshold = (float)stream.ReceiveNext();
            this.local_deform = (float)stream.ReceiveNext();
            this.local_3_threshold = (float)stream.ReceiveNext();
            this.local_4_threshold = (float)stream.ReceiveNext();
            this.local_deform_time = (float)stream.ReceiveNext();
            this.local_deform_scale = (float)stream.ReceiveNext();
        }
    }
 }
 
