using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using Rokoko;
using Rokoko.Core;
using Rokoko.Inputs;

public class NewtonManager : MonoBehaviour
{
    public bool rotateActor;
    private void Start()
    {

        if (!PhotonNetwork.IsMasterClient)
        {
            GetComponent<Actor>().enabled = false;
            this.enabled = false;
        }

    }
    private Vector3 newrotation;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (rotateActor)
        {
            newrotation = transform.rotation.eulerAngles + new Vector3(0, 1f, 0);
            transform.rotation = Quaternion.Euler(newrotation);
        }
    }
}
