using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class onoff : MonoBehaviour
{
    public float cooltime;
    private float curtime;

    public void OnClickbtton()
    {
        transform.Find("fw").gameObject.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Skill4);
        Invoke("Off", 5f);

    }

    void Off()
    {
        transform.Find("fw").gameObject.SetActive(false);
    }

}
