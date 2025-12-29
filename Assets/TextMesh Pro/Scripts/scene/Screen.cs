using System.Collections;
using UnityEngine;

public class Screen : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private AudioSource audio1;
    public AudioClip lobbyMusic; 

    private void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        audio1 = gameObject.GetComponent<AudioSource>();
    }

    public IEnumerator FadeIn()
    {
        if (audio1 != null && lobbyMusic != null)
        {
            audio1.clip = lobbyMusic;
            audio1.Play();
        }

        gameObject.SetActive(true);

        canvasGroup.alpha = 0;

        while(true)
        {
            if (canvasGroup.alpha >= 1f)
                break;
            else 
            {
                canvasGroup.alpha += 0.4f * Time.deltaTime;

                yield return null;
            }

        }
    }

    public IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            if (canvasGroup.alpha <= 0f)
                break;
            else
            {
                canvasGroup.alpha -= 0.4f * Time.deltaTime;
                
                yield return null;
            }
        }

        if (audio1 != null && lobbyMusic != null)
        {
            audio1.Stop();
        }

        gameObject.SetActive(false);
    }

}
