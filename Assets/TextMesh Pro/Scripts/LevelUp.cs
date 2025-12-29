using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;
    bool tutorialcheck = true;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.EffectBgm(false);
        if (tutorialcheck)
        {
            TextManager.instance.SelectPoket(tutorialcheck);
        }
        tutorialcheck = false;
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    void Next()
    {
        //1. Disable all items
        foreach(Item item in items)
        {
            item.gameObject.SetActive(false);
        }
        //2. Select 3 items randomly
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }

        for (int index = 0; index < ran.Length; index++)
        {
            Item ranItem = items[ran[index]];

            if (ranItem.level == ranItem.data.damages.Length)
            {        
                //3. If level = max -> consumable item
                items[4].gameObject.SetActive(true);
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }
        }

    }

    public void PlayerStart()
    {
        items[0].gameObject.SetActive(false);
        items[1].gameObject.SetActive(false);
        items[2].gameObject.SetActive(false);
        items[3].gameObject.SetActive(false);
        items[4].gameObject.SetActive(true);
        items[5].gameObject.SetActive(true);

        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);
    }

}
