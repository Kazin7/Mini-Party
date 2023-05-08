using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulltetManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "Player"){
            Debug.Log("맞춤");
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
