using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class settingName : MonoBehaviourPun
{
    public TMP_Text playerName;
    // Start is called before the first frame update
    void Start()
    {


    }
    
    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            playerName.text = PhotonNetwork.LocalPlayer.NickName;
        }
        else
        {
            playerName.text = photonView.Owner.NickName;
        }
    }
}
