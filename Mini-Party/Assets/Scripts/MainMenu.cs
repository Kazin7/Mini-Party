using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible=true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickNewGame(){//시작버튼 클릭시 씬 로드
        SceneManager.LoadScene("NickName");
    }
    public void OnclikQuitGame(){//종료버튼 클릭시 종료
        Application.Quit();       
    }
    public void OnclikExplainGame(){//설명 버튼 클릭시 씬 이동
        
    }
}
