using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeTransition : MonoBehaviour
{
    private SoundControl _SoundControl;

    [Header("FADE TRANSITION SETTINGS")]
    public Animator fadeOutAnimatorComponent;

    private string sceneName;

    private void Awake()
    {
        _SoundControl = FindObjectOfType<SoundControl>();
    }

    public void StartFade(string destinationScene)
    {
        _SoundControl.StartCoroutine("MusicFade", true);
        fadeOutAnimatorComponent.SetTrigger("FadeOutTrigger");
        sceneName = destinationScene;
    }

    public void OnFadeComplete()
    {
        if (sceneName != "")
        {
            SceneManager.LoadScene(sceneName);
            _SoundControl.StartCoroutine("MusicFade", false);
        }
    }
}
