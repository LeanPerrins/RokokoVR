using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Wave.Essence.Hand.StaticGesture;
using Wave.Essence.Hand.Model;
using Wave.Essence.Hand;

[RequireComponent(typeof(PhotonView))]
public class NetworkPlayer : MonoBehaviourPun
{
    [Header("GameObjects")]
    [SerializeField] private GameObject HandMeshL;
    [SerializeField] private GameObject HandMeshR;
    [SerializeField] private GameObject HandTransforms;
    [SerializeField] private GameObject Camera;
    [SerializeField] private GameObject PlayerModel;
    [SerializeField] private GameObject PlayerModelHead;

    [Header("Drawing")]
    [SerializeField] private ParticleSystem particleL;
    [SerializeField] private ParticleSystem particleR;
    [SerializeField] private bool canDraw = true;
    [SerializeField] private Transform HandLDrawTranform;
    [SerializeField] private Transform HandRDrawTranform;

    private PhotonView thisphotonView;
    private string handStateL;
    private string handStateR;

    // Start is called before the first frame update
    void Awake()
    {
        GetVariables();
        SetupForClient(thisphotonView.IsMine);
    }

    void GetVariables()
    {
        thisphotonView = gameObject.GetComponent<PhotonView>();
    }

    void SetupForClient(bool isOwner)
    {
        if (!isOwner)
        {
            HandMeshL.SetActive(false);
            HandMeshR.SetActive(false);
            particleL.gameObject.transform.SetParent(transform);
            particleR.gameObject.transform.SetParent(transform);
            HandTransforms.SetActive(false);
            Camera.SetActive(false);
        }
        else
        {
            MeshRenderer[] playerMeshRenderers = PlayerModel.transform.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mr in playerMeshRenderers)
            {
                mr.enabled = false;
            }

            SkinnedMeshRenderer[] playerSkinnedMeshRenderers = PlayerModel.transform.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer smr in playerSkinnedMeshRenderers)
            {
                smr.enabled = false;
            }
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (thisphotonView.IsMine)
        {
            UpdateHandGestures();
            SetModelPosition();
        }
    }

    void SetModelPosition()
    {
        PlayerModel.transform.position = Camera.transform.position;
        PlayerModel.transform.rotation = Quaternion.Euler(0, Camera.transform.rotation.eulerAngles.y, 0);
        //PlayerModelHead.transform.rotation = Quaternion.Euler(Camera.transform.rotation.eulerAngles);
    }

    void UpdateHandGestures()
    {
        particleL.transform.position = HandLDrawTranform.position;
        particleR.transform.position = HandRDrawTranform.position;


        handStateL = WXRGestureHand.GetSingleHandGesture(true);
        handStateR = WXRGestureHand.GetSingleHandGesture(false);

        CheckForDraw();
    }

    void CheckForDraw()
    {

    // Left hand
      if(!particleL.isPlaying)
        {
            if (canDraw && handStateL == "IndexUp")
            {
                thisphotonView.RPC("SetParticleSystemL", RpcTarget.All, true);
            }
        }

      else if (particleL.isPlaying)
        {
            if (!canDraw || handStateL != "IndexUp")
            {
                thisphotonView.RPC("SetParticleSystemL", RpcTarget.All, false);
            }
        }

        // Right hand
        if (!particleR.isPlaying)
        {
            if (canDraw && handStateR == "IndexUp")
            {
                thisphotonView.RPC("SetParticleSystemR", RpcTarget.All, true);
            }
        }

        else if (particleR.isPlaying)
        {
            if (!canDraw || handStateR != "IndexUp")
            {
                thisphotonView.RPC("SetParticleSystemR", RpcTarget.All, false);
            }
        }
    }   

    [PunRPC]
    private void SetParticleSystemL(bool isplaying)
    {
        Debug.Log("!!!!!!!!!!!!!!!!");
        if (isplaying)
        {
            particleL.Play();
        }
        else
        {
            particleL.Pause();
        }
    }

    [PunRPC]
    private void SetParticleSystemR(bool isplaying)
    {
        Debug.Log("???????????");
        if (isplaying)
        {
            particleR.Play();
        }
        else
        {
            particleR.Pause();
        }
    }
}
