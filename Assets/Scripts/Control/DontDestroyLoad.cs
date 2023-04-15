using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyLoad : MonoBehaviour
{
    private static DontDestroyLoad Instance;
    private SoundControl _SoundControl;

    // Control variables
    private string sceneName, nextScene;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        sceneName = SceneManager.GetActiveScene().name;
        _SoundControl = GetComponent<SoundControl>();

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        nextScene = SceneManager.GetActiveScene().name;

        if (sceneName != nextScene)
        {
            sceneName = nextScene;
            _SoundControl.CheckSceneName();
        }
    }
}
