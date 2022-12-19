using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkSpawner : MonoBehaviour
{

    [SerializeField] private Transform spawnTransform;
    // Start is called before the first frame update
    void Start()
    {
        SpawnNetworkPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
    void SpawnNetworkPlayer()
    {

#if UNITY_EDITOR
        return;
#endif

#if UNITY_ANDROID
        PhotonNetwork.Instantiate("PlayerCameraRig", spawnTransform.position, spawnTransform.rotation);
#endif
    }
}
