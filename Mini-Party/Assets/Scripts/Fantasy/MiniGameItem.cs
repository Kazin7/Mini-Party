using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{ 
    NONE,
    Booster,
    Punch,
    WaterBall,
    Slow
}
[Serializable]
public class MiniGameItem : MonoBehaviour
{
    public ItemType type;
    private PhotonView _pv;
    public PhotonView pv => _pv;

    private void Start()
    {
        _pv = GetComponent<PhotonView>();
    }
    [PunRPC]
    public void AA()
    {
        //if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Destroy(gameObject);
    }
}
