using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{

    bool isLive;
    
    
    void Awake()
    {
        var button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked()
    {
        if (!isLive)
            return;
        Time.timeScale = 0;
    }

}
