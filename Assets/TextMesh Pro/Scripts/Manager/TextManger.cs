using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public static TextManager instance;

    public Text talktext;
    public Text tutotext;
    public LevelUp uiLevelUp;
    public GameObject uiTextObject;
    public GameObject uiTutoObject;

    private int textcount;
    private int tutocount;
    string[] text;
    string[] tuto_text;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        textcount = 0;
        tutocount = 0;
        text = new string[9];
        tuto_text = new string[6];
        // 이미지 배열 크기에 맞게 초기화 필요 시 크기 지정

        uiTutoObject.SetActive(true);
        uiTextObject.SetActive(false); // 시작할 때는 메인 텍스트 UI를 비활성화합니다.

        // talktext 배열의 각 요소에 텍스트를 할당합니다.
        text[0] = "안녕! 만나서 반가워";
        text[1] = "들어가기에 앞서 필요한 것들을 알려줄께";
        text[2] = "가장 먼저 적들이 너에게 몰려올꺼야";
        text[3] = "다행히 너를 지켜줄 위즈가 있어";
        text[4] = "앞으로 쭉 함께할 위즈니깐 신중히 선택해줘!";
        //증강 선택 이후
        text[5] = "각 위즈들은 강력한 스킬을 가지고 있고";
        text[6] = "<color=#8B0000><size=150>tab</size></color>을 통해 스킬을 바꿀 수 있어";
        text[7] = "강력한 스킬들은 <color=#8B0000><size=150>Q</size></color>키를 통해 사용가능해!";
        text[8] = "그럼 모험을 떠나 볼까?";

        // tuto_text 배열의 각 요소에 텍스트를 할당합니다.
        tuto_text[0] = "평화로운 나날이 계속되던 어느날";
        tuto_text[1] = "큰 다툼이 있었고";
        tuto_text[2] = "그들은 서로를 신뢰하지 못하는 상황에 이른다";
        tuto_text[3] = "하지만 주인공과 위즈는 함께 오해를 풀기로 마음먹고";
        tuto_text[4] = "설득을 위해 모험에 떠난다.";
        tuto_text[5] = "bn";

        // tuto_images 배열에 각 요소에 이미지 할당 (에디터에서 설정할 수 있습니다)
        // 예시: tuto_images[0] = Resources.Load<Sprite>("image1");

        Action_Tuto();
    }

    public void Action()
    {
        GameManager.instance.Stop();
        talktext.text = text[textcount];
        textcount++;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        textStop(textcount);
    }

    public void Action_Tuto()
    {
        GameManager.instance.Stop();
        tutotext.text = tuto_text[tutocount];
        tutocount++;

        if (tutocount == tuto_text.Length)
        {
            uiTutoObject.SetActive(false);
            uiTextObject.SetActive(true);
            Action();
        }
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Tuto);
    }

    public void textStop(int textcount)
    {
        if (textcount == 5)
        {
            GameResumInTutorial();
            // 증강 제공
            SelectPoket(false);
        }

        if (textcount == text.Length)
        {
            GameResumInTutorial();
        }
    }

    public void SelectPoket(bool tutorialcheck)
    {
        if (!tutorialcheck)
        {
            uiLevelUp.PlayerStart();
        }
        else
        {
            uiTextObject.SetActive(true);
            Action();
        }
    }

    public void GameResumInTutorial()
    {
        GameManager.instance.Resume();
        uiTextObject.SetActive(false);
    }
}
