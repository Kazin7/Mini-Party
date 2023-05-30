using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Rank : MonoBehaviour
{

    public static Rank Instance;
    public bool isGameClear;
    public int rank;
    public Text rankTx;
    public PhotonView pv;

    [PunRPC]
    public void Goal()
    {

    }
    private void Awake()
    {
        rank -= 1;
        Instance = this;
        pv = GetComponent<PhotonView>();
    }
    public void GoalCharacter()
    {
        isGameClear = true;
        rankTx.gameObject.SetActive(true);
        rankTx.text = rank + "등 입니다.";
    }

}
