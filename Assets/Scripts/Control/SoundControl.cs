using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    [Header("AUDIO SOURCES")]
    public Toggle touchModeGame;
    public GameObject PanelAudio;
    public Canvas CanvasSoundController;
    public GameObject EventSystemPanelSound;
    public AudioSource sfxAudioSource;
    public AudioSource sfxSoundEffects;

    [Header("SOUND BUTTONS SETTINGS")]
    public Toggle musicSpeakerToggle;
    public Toggle sfxSpeakerToggle;

    [Header("SOUND SLIDERS SETTINGS")]
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("SFX SOUNDS")]
    public AudioClip titleAudio;
    public AudioClip stageAudio;
    public AudioClip levelAudio;
    public AudioClip gameOverAudio;

    [Header("SFX EFFECTS")]
    public AudioClip jumpSoundEffect;
    public AudioClip scoreSoundEffect;
    public AudioClip obstacleDamageSoundEffect;
    public AudioClip deathSoundEffect;
    public AudioClip spinPlateSoundEffect;

    public static SoundControl Instance;

    // Control variables
    private bool isMusicSpeaker, isSFXSpeaker;

    private void Awake()
    {
        EventSystemPanelSound.SetActive(false);
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("FirstGameplay") == 0)
        {
            PlayerPrefs.SetFloat("MusicVolume", 0.1f);
            PlayerPrefs.SetFloat("SFXVolume", 0.05f);
            PlayerPrefs.SetInt("FirstGameplay", 1);
            PlayerPrefs.Save();
        }

        sfxAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        sfxSoundEffects.volume = PlayerPrefs.GetFloat("SFXVolume");

        if (PlayerPrefs.GetInt("isMusicMuted") != 0)
        {
            if (PlayerPrefs.GetInt("isMusicMuted") != 1)
            {
                sfxAudioSource.mute = true;
            }
        }

        if (PlayerPrefs.GetInt("isSFXMuted") != 0)
        {
            if (PlayerPrefs.GetInt("isSFXMuted") != 1)
            {
                sfxSoundEffects.mute = true;
            }
        }

        if (PlayerPrefs.GetInt("isTouchMode") != 0)
        {
            if (PlayerPrefs.GetInt("isTouchMode") != 1)
            {
                touchModeGame.isOn = true;
            }
            else
            {
                touchModeGame.isOn = false;
            }
        }
    }

    public void CheckSceneName()
    {
        CanvasSoundController.worldCamera = Camera.main;
        sfxAudioSource.loop = true;
        sfxAudioSource.pitch = 1f;

        if (SceneManager.GetActiveScene().name == "Title")
        {
            sfxAudioSource.clip = titleAudio;
        }

        if (SceneManager.GetActiveScene().name == "Stages")
        {
            sfxAudioSource.clip = stageAudio;
        }

        if (SceneManager.GetActiveScene().name == "StageEasy")
        {
            sfxAudioSource.clip = levelAudio;
        }

        if (SceneManager.GetActiveScene().name == "StageHard")
        {
            sfxAudioSource.clip = levelAudio;
            sfxAudioSource.pitch = 1.2f;
        }

        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            sfxAudioSource.clip = gameOverAudio;
            sfxAudioSource.loop = false;
        }

        sfxAudioSource.Play();
    }

    public void AudioSoundsControl()
    {
        sfxAudioSource.volume = musicSlider.value;
        sfxSoundEffects.volume = sfxSlider.value;

        if (musicSlider.value <= 0f)
        {
            musicSpeakerToggle.isOn = true;
            MuteMusicSpeaker();
        }
        else
        {
            musicSpeakerToggle.isOn = false;
            MuteMusicSpeaker();
        }

        if (sfxSlider.value <= 0f)
        {
            sfxSpeakerToggle.isOn = true;
            MuteSFXSpeaker();
        }
        else
        {
            sfxSpeakerToggle.isOn = false;
            MuteSFXSpeaker();
        }
    }

    public void MusicControl()
    {
        sfxAudioSource.volume = musicSlider.value;

        if (musicSlider.value <= 0f)
        {
            musicSpeakerToggle.isOn = true;
            MuteMusicSpeaker();
        }
        else
        {
            musicSpeakerToggle.isOn = false;
            MuteMusicSpeaker();
        }
    }

    public void SFXControl()
    {
        sfxSoundEffects.volume = sfxSlider.value;

        if (sfxSlider.value <= 0f)
        {
            sfxSpeakerToggle.isOn = true;
            MuteSFXSpeaker();
        }
        else
        {
            sfxSpeakerToggle.isOn = false;
            MuteSFXSpeaker();
        }
    }

    public void MuteMusicSpeaker()
    {
        if (musicSpeakerToggle.isOn == true && isMusicSpeaker == false)
        {
            isMusicSpeaker = true;
            sfxAudioSource.mute = true;
        }
        else
        {
            isMusicSpeaker = false;
            sfxAudioSource.mute = false;
        }
    }

    public void MuteSFXSpeaker()
    {
        if (sfxSpeakerToggle.isOn == true && isSFXSpeaker == false)
        {
            isSFXSpeaker = true;
            sfxSoundEffects.mute = true;
        }
        else
        {
            isSFXSpeaker = false;
            sfxSoundEffects.mute = false;
        }
    }

    public void QuitPanelSound()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.SetInt("isMusicMuted", (musicSpeakerToggle.isOn ? 2 : 1));
        PlayerPrefs.SetInt("isSFXMuted", (sfxSpeakerToggle.isOn ? 2 : 1));
        PlayerPrefs.SetInt("isTouchMode", (touchModeGame.isOn ? 2 : 1));
        PlayerPrefs.Save();
    }

    public void PanelSoundSettings()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        AudioSoundsControl();

        if (PlayerPrefs.GetInt("isMusicMuted") != 0)
        {
            musicSpeakerToggle.isOn = (PlayerPrefs.GetInt("isMusicMuted") != 1);
        }

        if (PlayerPrefs.GetInt("isSFXMuted") != 0)
        {
            sfxSpeakerToggle.isOn = (PlayerPrefs.GetInt("isSFXMuted") != 1);
        }

        if (PlayerPrefs.GetInt("isTouchMode") != 0)
        {
            touchModeGame.isOn = (PlayerPrefs.GetInt("isTouchMode") != 1);
        }

        PanelAudio.SetActive(true);
    }

    public IEnumerator MusicFade(bool value)
    {
        float currentMusicVolume = PlayerPrefs.GetFloat("MusicVolume");

        if (value == true)
        {
            for (float volume = currentMusicVolume; volume > 0f; volume -= 0.0005f)
            {
                sfxAudioSource.volume = volume;
                yield return new WaitForEndOfFrame();
            }
        }

        if (value == false)
        {
            sfxAudioSource.Play();

            for (float volume = 0f; volume < currentMusicVolume; volume += 0.0005f)
            {
                sfxAudioSource.volume = volume;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
