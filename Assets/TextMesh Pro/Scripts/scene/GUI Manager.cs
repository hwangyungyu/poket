using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public Screen lobbyScene;

    public string gameSceneName = "SampleScene";

    public Button gameStartButton;

    private void Start()
    {
        StartCoroutine(lobbyScene.FadeIn());
        gameStartButton.onClick.AddListener(OnButtonClick);
    }

    private IEnumerator ChangeScene(string sceneName)
    {
        yield return StartCoroutine(lobbyScene.FadeOut());
        SceneManager.LoadScene(sceneName); 
    }

    void OnButtonClick()
    {
        StartCoroutine(ChangeScene(gameSceneName));
    }
}
