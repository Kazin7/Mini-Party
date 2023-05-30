using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerPos : MonoBehaviour
{
    public float Ypos;
    public PhotonView pv;
    private bool drop = false;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        Ypos = this.transform.position.y;
        if(Ypos < -5f){
            if(pv.IsMine && !drop){
                drop = true;
                Rank.Instance.pv.RPC("Goal",RpcTarget.AllBufferedViaServer);
                Rank.Instance.GoalCharacter();
                transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
}
