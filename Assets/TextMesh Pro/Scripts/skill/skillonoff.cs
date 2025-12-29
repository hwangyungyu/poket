using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillonoff : MonoBehaviour
{
    public int count;
    public GameObject[] skill;
    public int[] id;
    public int number;


    public float cooltime;
    private float curtime;

    private void Start()
    {
        skill[0].gameObject.SetActive(true);
        id = new int[11];
        count = 0;
        number = 0;
    }

    void OnEnable()
    {
        EventManager.OnItemIdInit += HandleItemIdInit;
    }

    void OnDisable()
    {
        EventManager.OnItemIdInit -= HandleItemIdInit;
    }

    void HandleItemIdInit(int itemId)
    {
        if(itemId != 4 && itemId != 5)
        {
            id[count] = itemId;
            count++;
        }
    }

    void Update()
    {
        if (curtime <= 0)
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                for (int i = 0; i < count; i++)
                {
                    if (i == number)
                    {
                        skill[id[i]].gameObject.SetActive(true);
                    }
                    else
                    {
                        skill[id[i]].gameObject.SetActive(false);
                    }
                }
                number++;
                if (number == count)
                {
                    number = 0;
                }

                if(number > 11)
                {
                    number = 0;
                }
            }
            curtime = cooltime;
        }
        curtime -= Time.deltaTime;
    }
}
