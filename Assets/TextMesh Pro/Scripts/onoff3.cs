using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class onoff3 : MonoBehaviour
{
    public float cooltime;
    private float curtime;

    public void OnClickbtton()
    {
        transform.Find("slow").gameObject.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Skill3);
        Invoke("Off", 5f);

    }

    void Off()
    {
        transform.Find("slow").gameObject.SetActive(false);
    }

}