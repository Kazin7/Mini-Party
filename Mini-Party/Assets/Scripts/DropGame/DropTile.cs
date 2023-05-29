using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTile : MonoBehaviour
{
    private float cnt = 0.0f;
    private bool state = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if(state == true) 
        {
            cnt += Time.deltaTime;
        }

        if(cnt > 2)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision col) 
    {
        state = true;
    }
}
