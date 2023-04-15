using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonCommands : MonoBehaviour
{
    private SoundControl _SoundControl;
    private FadeTransition _FadeTransition;

    private void Awake()
    {
        _SoundControl = FindObjectOfType<SoundControl>();
        _FadeTransition = FindObjectOfType<FadeTransition>();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "PreTitle")
        {
            GoToScene("Title");
        }
    }

    public void GoToScene(string sceneName)
    {
        _FadeTransition.StartFade(sceneName);
    }

    public void TryAgain()
    {
        if (PlayerPrefs.GetString("SceneName") != "")
        {
            _FadeTransition.StartFade(PlayerPrefs.GetString("SceneName"));
        }
    }

    public void AccessPanelSound()
    {
        _SoundControl.PanelSoundSettings();
    }
}
