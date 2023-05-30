using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class DropRank : MonoBehaviour
{

    public static DropRank Instance;
    public int rank;
    public TextMeshProUGUI rankTx;
    public PhotonView pv;

    [PunRPC]
    public void Goal()
    {
        rank -= 1;
    }

    private void Awake()
    {
        Instance = this;
        pv = GetComponent<PhotonView>();
    }

    public void GoalCharacter()
    {
        rankTx.gameObject.SetActive(true);
        rankTx.text = rank + "등 입니다.";
    }

}
