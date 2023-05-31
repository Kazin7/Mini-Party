using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name == "CannonGame" && PhotonNetwork.IsMasterClient)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SceneManager.LoadScene("CannonGame",LoadSceneMode.Single);
            if(Input.GetKeyDown(KeyCode.Alpha2))
                SceneManager.LoadScene("DropGame", LoadSceneMode.Single);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                SceneManager.LoadScene("Fantasy_01", LoadSceneMode.Single);
        }
            
    }
}
