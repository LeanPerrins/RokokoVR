using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


public class ControllingCube : MonoBehaviour
{
    public PhotonView pv;
    private float value1;
    private float value2;
    public float value3;

    private void Start()
    {
            EasyController.esconsend.Send_data(1, 0);
            EasyController.esconsend.Send_data(2, 0);
            EasyController.esconsend.Send_data(3, 0);
    }
    void Update ()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            value1 = EasyController.escon.get_state(1, 1f, 3f);
        
            pv.RPC("SyncValue", RpcTarget.All, value1);
        }
        
        
        
        
        
        
        this.transform.Rotate(value1, value2, value3);
        
        
        

    }
    [PunRPC]
    void SyncValue(float net_threshold_Fade1)
    {
        value1 = net_threshold_Fade1;
        Debug.Log(value1);
    }
}
