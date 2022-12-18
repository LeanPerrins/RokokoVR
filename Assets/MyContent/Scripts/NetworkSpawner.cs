using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkSpawner : MonoBehaviourPun
{

    public Transform t;
    // Start is called before the first frame update
    void Start()
    {

#if UNITY_EDITOR
        return;
#endif

#if UNITY_ANDROID
        PhotonNetwork.Instantiate("CameraRig", new Vector3(0,0,-4), Quaternion.identity);
#endif

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PhotonNetwork.Destroy(FindObjectOfType<CmaeraRigController>().gameObject.GetComponent<PhotonView>());
        }

        this.photonView.RPC("Test", RpcTarget.All);
    }

    [PunRPC]
    private void Test()
    {
        t.position += new Vector3(0, 0.0001f, 0);
    }
}
