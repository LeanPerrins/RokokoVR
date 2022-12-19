using Photon.Pun;
using UnityEngine;

public class SceneControl : MonoBehaviourPunCallbacks, IPunObservable
{
    public PhotonView pv;
    private float Skybox_ex;

    public Light Direct1;
    public Light Direct2;

   
    public GameObject AudioVis;

    

    private float Direct_Intense2;
    private float Direct_Intense1;
    private float AudioVisFade;

    public int Skychange = 1;
    public Material SkyboxMaterial1;
    public Material SkyboxMaterial2;

    
    
    // Start is called before the first frame update
    void Start()
    {
       EasyController.esconsend.Send_data(3,0); 
       EasyController.esconsend.Send_data(4,0);
       EasyController.esconsend.Send_data(5,0);
       EasyController.esconsend.Send_data(6,0);
    }

    // Update is called once per frame
    void Update()
    {
        Skybox_ex = EasyController.escon.get_state(18, 0f, 0.35f);
        Direct_Intense1 = EasyController.escon.get_state(0, 0.05f, 0.9f);
        Direct_Intense2 = EasyController.escon.get_state(10, 0.01f, 0.7f);
        AudioVisFade = EasyController.escon.get_state(5, 0f, 1.1f);
        
        RenderSettings.skybox.SetFloat("_Exposure", Skybox_ex);
        Direct1.intensity = Direct_Intense1;
        Direct2.intensity = Direct_Intense2;
        AudioVis.transform.localScale = new Vector3(AudioVisFade, AudioVisFade, AudioVisFade);

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (Skychange == 1)
            {
                RenderSettings.skybox = SkyboxMaterial2;
                Skychange = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
                if (Skychange == 0)
                {
                    RenderSettings.skybox = SkyboxMaterial1;
                    Skychange = 1;
                }

        }
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Skybox_ex);
            stream.SendNext(Direct_Intense1);
            stream.SendNext(Direct_Intense2);
            stream.SendNext(AudioVisFade);
            stream.SendNext(Skychange);
        }
        else
        {
            this.Skybox_ex = (float)stream.ReceiveNext();
            this.Direct_Intense1 = (float)stream.ReceiveNext();
            this.Direct_Intense2 = (float)stream.ReceiveNext();
            this.AudioVisFade = (float)stream.ReceiveNext();
            this.Skychange = (int)stream.ReceiveNext();
        }
    }
}
