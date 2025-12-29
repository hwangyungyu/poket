using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillContorl : MonoBehaviour
{
    public string[] weaponname = { "Weapon 0", "Weapon 1", "Weapon 2", "Weapon 3", "Weapon 4", "Weapon 5", "Weapon 6", "Weapon 7", "Weapon 8", "Weapon 9", "Weapon 10", };
    void Update()
    {
        for(int i = 0; i < weaponname.Length; i++)
        {
            if (transform.Find("Weapon 0"))
            {
                transform.Find("Bullet 0").gameObject.SetActive(true);
            }

            if (transform.Find("Weapon 1"))
            {
                transform.Find("Bullet 3").gameObject.SetActive(true);
            }

            if (transform.Find("Weapon 2"))
            {
                transform.Find("Bullet 4").gameObject.SetActive(true);
            }

            if (transform.Find("Weapon 3"))
            {
                transform.Find("Bullet 5").gameObject.SetActive(true);
            }

            if (transform.Find("Weapon 6"))
            {
                transform.Find("Bullet 8").gameObject.SetActive(true);
            }

            if (transform.Find("Weapon 7"))
            {
                transform.Find("Bullet 9").gameObject.SetActive(true);
            }

            if (transform.Find("Weapon 8"))
            {
                transform.Find("Bullet 10").gameObject.SetActive(true);
            }

            if (transform.Find("Weapon 9"))
            {
                transform.Find("Bullet 11").gameObject.SetActive(true);
            }
        }


    }
}
