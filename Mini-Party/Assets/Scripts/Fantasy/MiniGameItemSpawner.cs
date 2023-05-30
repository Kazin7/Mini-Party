using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameItemSpawner : MonoBehaviour
{
    public Transform[] pos;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        if (PhotonNetwork.IsMasterClient)
        {
            while (true)
            {
                
                var item = PhotonNetwork.Instantiate("MiniGameItem_0" + Random.RandomRange(0, 4), pos[Random.RandomRange(0,pos.Length)].localPosition,Quaternion.identity);
                yield return new WaitForSeconds(2.45f);
            }
        }
    }
}
