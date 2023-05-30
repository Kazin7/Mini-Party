using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    private PhotonView pv;
    IEnumerator coroutine;
    IEnumerator On()
    {
        pv = GetComponent<PhotonView>();
        if (pv == null) gameObject.AddComponent<PhotonView>();
        yield return new WaitForSeconds(.5f);
        //gameObject.SetActive(false);
        pv.RPC("Off", RpcTarget.AllBufferedViaServer);
    }
    private void OnEnable()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = On();
        StartCoroutine(coroutine);
    }

    [PunRPC]
    void Off()
    {
        gameObject.SetActive(false);
    }
}
