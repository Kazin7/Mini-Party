using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class ButtonEvent : MonoBehaviour
{
    public static string nickName = null;
    public TMP_InputField playerName;

    public void OnClick(){
        nickName = playerName.GetComponent<TMP_InputField>().text;
        SceneManager.LoadScene("SampleScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
