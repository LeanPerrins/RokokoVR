using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using  Wave.Essence.Hand.StaticGesture;
using Wave.Essence.Hand.Model;

public class CmaeraRigController : MonoBehaviourPun
{
    public PhotonView pw;
    public Camera mainCam;
    public GameObject transforms;
    public HandMeshRenderer h1;
    public HandMeshRenderer h2;
    public Transform cube;
    public Transform hand1;
    public Transform hand2;

    public Transform hbone1;
    public Transform hbone2;
    private void Awake()
    {
        if (!pw.IsMine)
        {
            h1.enabled = false;
            h2.enabled = false;
            //transforms.SetActive(false);
            mainCam.gameObject.SetActive(false);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pw.IsMine)
        {
            //cube.position = mainCam.transform.position;
            hand1.position = hbone1.position;
            hand2.position = hbone2.position;
            Transform t = transform;
            this.photonView.RPC("SyncPositions", RpcTarget.All, Vector3.one);
            
            
        }
    }

    [PunRPC]
     void SyncPositions(Vector3 yolo)
    {
        cube.position = cube.position + new Vector3(0,0.001f,0);
        Debug.Log(yolo);
    }
}
