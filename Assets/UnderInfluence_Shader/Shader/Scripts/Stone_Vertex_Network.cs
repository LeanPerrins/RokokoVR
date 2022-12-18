using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Stone_Vertex_Network : MonoBehaviour
{
    public PhotonView pv;
    public float local_threshold;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {

        local_threshold = GetComponent<Renderer>().material.GetFloat("_Stone_Threshold");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            local_threshold = local_threshold + 0.01f;
            pv.RPC("SyncValue", RpcTarget.All, local_threshold);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            local_threshold = local_threshold - 0.01f;
            pv.RPC("SyncValue", RpcTarget.All, local_threshold);
        }
        gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Stone_Threshold", local_threshold);
    }
    
    [PunRPC]
    void SyncValue(float net_threshold)
    {
        local_threshold = net_threshold;
    }
}
