using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skillcontrol : MonoBehaviour
{
    public static Skillcontrol Instance;

    public GameObject[] hideSkillButtons;
    public GameObject[] textPros;
    public TextMeshProUGUI[] hideSkillTimeText;
    public Image[] hideSkillImages;
    private bool[] isHideSkills = { false, false, false, false, false, false, false, false, false, false };
    private float[] skillTimes = { 10, 15, 15, 5, 7, 6, 9, 12 ,3 , 25, 5 };
    private float[] getskillTimes = {0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0};

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < textPros.Length; i++)
        {
            hideSkillTimeText[i] = textPros[i].GetComponent<TextMeshProUGUI>();
            hideSkillButtons[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HideSkillChk();
    }

    public void HideSkillSetting(int skillNum)
    {
        hideSkillButtons[skillNum].SetActive(true);
        getskillTimes[skillNum] = skillTimes[skillNum];
        isHideSkills[skillNum] = true;
    }

    private void HideSkillChk()
    {
        if (isHideSkills[0])
        {
            StartCoroutine(SkillTimeChk(0));
        }

        if (isHideSkills[1])
        {
            StartCoroutine(SkillTimeChk(1));
        }

        if (isHideSkills[2])
        {
            StartCoroutine(SkillTimeChk(2));
        }

        if (isHideSkills[3])
        {
            StartCoroutine(SkillTimeChk(3));
        }

        if (isHideSkills[4])
        {
            StartCoroutine(SkillTimeChk(4));
        }

        if (isHideSkills[5])
        {
            StartCoroutine(SkillTimeChk(5));
        }

        if (isHideSkills[6])
        {
            StartCoroutine(SkillTimeChk(6));
        }

        if (isHideSkills[7])
        {
            StartCoroutine(SkillTimeChk(7));
        }

        if (isHideSkills[8])
        {
            StartCoroutine(SkillTimeChk(8));
        }

        if (isHideSkills[9])
        {
            StartCoroutine(SkillTimeChk(9));
        }
    }

    IEnumerator SkillTimeChk(int skillNum)
    {
        yield return null;

        if (getskillTimes[skillNum] > 0)
        {
            getskillTimes[skillNum] -= Time.deltaTime;

            if (getskillTimes[skillNum] < 0)
            {
                getskillTimes[skillNum] = 0;
                isHideSkills[skillNum] = false;
                hideSkillButtons[skillNum].SetActive(false);
            }

            hideSkillTimeText[skillNum].text = getskillTimes[skillNum].ToString("00");

            float time = getskillTimes[skillNum] / skillTimes[skillNum];
            hideSkillImages[skillNum].fillAmount = time;

        }
    }

}
