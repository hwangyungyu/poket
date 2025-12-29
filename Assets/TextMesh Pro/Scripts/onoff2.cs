using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class onoff2 : MonoBehaviour
{
    public float cooltime;
    private float curtime;

    public void OnClickbtton()
    {
        transform.Find("lazer").gameObject.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Skill5); 
        Invoke("Off", 3f);

    }

    void Off()
    {
        transform.Find("lazer").gameObject.SetActive(false);
    }

}
