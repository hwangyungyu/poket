using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    public static EndingManager instance;

    public Text endingtext;
    public GameObject uiendingObject;

    private int textcount;
    string[] text;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        textcount = 0;

        text = new string[9];

        // 이미지 배열 크기에 맞게 초기화 필요 시 크기 지정

        // 시작할 때는 메인 텍스트 UI를 비활성화합니다.

        // talktext 배열의 각 요소에 텍스트를 할당합니다.
        text[0] = "험난한 모험이었지만";
        text[1] = "자신의 친구들이 모두 웃음을 찾을때까지";
        text[2] = "그날까지 모험은 계속됩니다!";
        text[3] = "continue...?";

        uiendingObject.SetActive(true);

        Action_Tuto();
    }
    public void Action_Tuto()
    {
        GameManager.instance.Stop();
        endingtext.text = text[textcount];
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Tuto);
        textcount++;
        
        textStop(textcount);
        
    }

    public void textStop(int textcount)
    {
        if (textcount == text.Length)
        {
            GameResumInEnding();
        }
    }

    public void GameResumInEnding()
    {
        GameManager.instance.Resume();
        GameManager.instance.GameVic();
        uiendingObject.SetActive(false);
    }
}
