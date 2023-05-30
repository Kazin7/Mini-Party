using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name == "CannonGame")
            if (Input.GetMouseButtonDown(0))
                SceneManager.LoadScene("DropGame",LoadSceneMode.Single);
    }
}
