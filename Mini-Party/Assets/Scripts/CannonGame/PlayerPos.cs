using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    public float Ypos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ypos = this.transform.position.y;
        if(Ypos < -5f){
            Rank.Instance.GoalCharacter();
        }
    }
}
