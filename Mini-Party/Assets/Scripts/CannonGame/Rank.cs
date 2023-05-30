using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
public class Rank : MonoBehaviour
{

    public static Rank Instance;
    public bool isGameClear;
    public int rank;
    public TextMeshProUGUI rankTx;
    public PhotonView pv;

    [PunRPC]
    public void Goal()
    {

    }
    private void Awake()
    {
        Instance = this;
        pv = GetComponent<PhotonView>();
    }
    public void GoalCharacter()
    {
        isGameClear = true;
        rankTx.gameObject.SetActive(true);
        rankTx.text = rank + "등 입니다.";
        rank -= 1;
    }

}
