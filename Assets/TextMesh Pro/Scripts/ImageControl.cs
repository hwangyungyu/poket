using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ImageControl : MonoBehaviour
{
    public int count;
    
    void Start()
    {
        count = 1;
        transform.Find("Image 1").gameObject.SetActive(true);
    }

    // Update is called once per frame
    public void Action_Tuto_Images()
    {
        switch (count)
        {
            case 0:
                break;
            case 1:
                transform.Find("Image 1").gameObject.SetActive(false);
                transform.Find("Image 2").gameObject.SetActive(true);
                break;
            case 2:
                transform.Find("Image 2").gameObject.SetActive(false);
                transform.Find("Image 3").gameObject.SetActive(true);
                break;
            case 3:
                transform.Find("Image 3").gameObject.SetActive(false);
                transform.Find("Image 4").gameObject.SetActive(true);
                break;
            case 4:
                transform.Find("Image 4").gameObject.SetActive(false);
                transform.Find("Image 5").gameObject.SetActive(true);
                break;
            case 5:
                transform.Find("Image 5").gameObject.SetActive(false);
                break;
        }

        count++;
    }
}
