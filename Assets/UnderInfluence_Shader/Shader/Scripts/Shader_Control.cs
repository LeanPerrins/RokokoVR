using System;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.Hub;

public class Shader_Control : MonoBehaviourPunCallbacks, IPunObservable
{
    private PhotonView pv;
    public float local_fade12;
    
    private float local_fade34;
    private float local_fade12_34;
    
    private float local_1_threshold;
    private float local_2_threshold;
    private float local_3_threshold;
    private float local_4_threshold;
    
    private float local_deform;
    private float local_deform_time;
    private float local_deform_scale;

    // Start is called before the first frame update
    void Start()
    {

        EasyController.esconsend.Send_data(1,65);
        EasyController.esconsend.Send_data(2,65);
        EasyController.esconsend.Send_data(3,0);
        EasyController.esconsend.Send_data(4,0);
        EasyController.esconsend.Send_data(5,0);
        EasyController.esconsend.Send_data(6,0);
        EasyController.esconsend.Send_data(7,0);
        EasyController.esconsend.Send_data(8,0);
        EasyController.esconsend.Send_data(9,0);
        EasyController.esconsend.Send_data(10,0);
        EasyController.esconsend.Send_data(11,0);
        EasyController.esconsend.Send_data(12,0);
        EasyController.esconsend.Send_data(13,0);
        EasyController.esconsend.Send_data(14,0);
        EasyController.esconsend.Send_data(15,0);
        EasyController.esconsend.Send_data(16,0);
        EasyController.esconsend.Send_data(17,0);
        EasyController.esconsend.Send_data(18,0);


    }

    // Update is called once per frame
    void Update()
    {
        //Control & Sync main fader
        local_fade12 = EasyController.escon.get_state(15, 0f, 1f); 
       gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Fader_1_2", local_fade12);

       local_fade34 = EasyController.escon.get_state(17, 0f, 1f);
       gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Fader_3_4", local_fade34);

       local_fade12_34 = EasyController.escon.get_state(16, 0f, 1f);
       gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Fader_1_2_3_4", local_fade12_34);
       
       local_deform_time = EasyController.escon.get_state(1, 0f, 1f);
       local_deform_scale = EasyController.escon.get_state(2, 0f, 1f);
       gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_TimeSpeed", local_deform_time);
       gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_NoiseScale", local_deform_scale);

       //Control & Sync state fader
       if (local_fade12_34 == 0)
       {
           if (local_fade12 == 0)
           {
               local_1_threshold = EasyController.escon.get_state(7, 0f, 2f);
               gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_1_Threshold", local_1_threshold);
           }

           if (local_fade12 == 1)
           {
               local_2_threshold = EasyController.escon.get_state(8, 0f, 0.8f);
               gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_2_Threshold", local_2_threshold);
           }
       }

       if (0f < local_fade12_34 && local_fade12_34 < 1f)
       {
           local_deform = EasyController.escon.get_state(16, -2f, -1.16f)*-1;
           gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Threshold_Deform", local_deform); 
       }

       if (local_fade12_34 == 1)
       {
           if (local_fade34 == 0)
           {
               local_3_threshold = EasyController.escon.get_state(11, -1.16f, 0.5f)*-1;
               gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_3_Threshold", local_3_threshold);

               if (local_3_threshold > 0)
               {
                   local_deform = EasyController.escon.get_state(11, -1.16f, 0f)*-1;
                   gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Threshold_Deform", local_deform);  
             
                   local_deform = EasyController.escon.get_state(3, -0.01f, 2f);
                   gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Threshold_Deform", local_deform); 
               }
           }

           if (local_fade34 == 1)
           {
               local_4_threshold = EasyController.escon.get_state(12, -2f, -0.45f)*-1;
               gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_4_Threshold", local_4_threshold);
               
               local_deform = EasyController.escon.get_state(12, -2f, -0.45f)*-1;
               gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Threshold_Deform", local_deform);
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