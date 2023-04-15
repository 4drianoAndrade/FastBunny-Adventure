using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private SoundControl _SoundControl;
    private FadeTransition _FadeTransition;

    [Header("TOUCH MODE SETTINGS")]
    public GameObject arrowRightButton;
    public GameObject arrowLeftButton;
    public GameObject jumpButton;

    [Header("PLAYER BUNNY SETTINGS")]
    public float movementSpeedBunny;
    public float jumpForceBunny;
    public Transform checkGroundTransform;
    public float groundCheckRadiusSize;
    public LayerMask groundJumpLayer;

    [Header("GROUND SETTINGS")]
    public float groundSpeedMovement;
    public float distanceToDestroyGround;
    public float groundSizeForInstantiation;
    public GameObject[] groundPrefab;
    public int counterGroundScenario;
    public MoveOffsetBg[] movementBGScenary;

    [Header("GAME SCORE")]
    public int gameScore;
    public Text scoreTxt;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        _SoundControl = FindObjectOfType<SoundControl>();
        _FadeTransition = FindObjectOfType<FadeTransition>();
    }

    private void Start()
    {
        if (_SoundControl.touchModeGame.isOn == true)
        {
            arrowRightButton.SetActive(true);
            arrowLeftButton.SetActive(true);
            jumpButton.SetActive(true);
        }
        else
        {
            arrowRightButton.SetActive(false);
            arrowLeftButton.SetActive(false);
            jumpButton.SetActive(false);
        }
    }

    public void JumpBunnyFX()
    {
        _SoundControl.sfxSoundEffects.PlayOneShot(_SoundControl.jumpSoundEffect);
    }

    public void ToScore(int amountOfPoints)
    {
        gameScore += amountOfPoints;
        scoreTxt.text = "Score: " + gameScore.ToString();
        _SoundControl.sfxSoundEffects.PlayOneShot(_SoundControl.scoreSoundEffect);
    }

    public void ObstacleDamageFX()
    {
        _SoundControl.sfxSoundEffects.PlayOneShot(_SoundControl.obstacleDamageSoundEffect);
    }

    public void DeathFX()
    {
        _SoundControl.sfxAudioSource.PlayOneShot(_SoundControl.deathSoundEffect);
    }

    public void SpinPlateFX()
    {
        _SoundControl.sfxSoundEffects.PlayOneShot(_SoundControl.spinPlateSoundEffect);
    }

    public void ChangeScene(string destinationScene)
    {
        _FadeTransition.StartFade(destinationScene);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkGroundTransform.position, groundCheckRadiusSize);
    }
}
