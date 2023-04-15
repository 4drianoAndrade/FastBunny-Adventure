using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum actions
{
    MOVE_RIGHT,
    STOP_RIGHT,
    MOVE_LEFT,
    STOP_LEFT,
    JUMP
}

public class BunnyController : MonoBehaviour
{
    private GameController _GameController;
    private SoundControl _SoundControl;

    private Rigidbody2D rb2DBunnyComponent;
    private Animator bunnyAnimatorComponent;

    private SpriteRenderer sRenderBunnyComponent;
    public CircleCollider2D cColliderBunnyComponent;
    private MoveOffsetBg[] movesBG;

    // Control variables
    private bool jumpAfterObstacleCollision = false;
    private bool isGroundedBunny;
    private bool isPunctuatedBunny;
    private float horizontalMovementBunny;
    private bool isDeathBunny;
    private float jumpTimerBunny;
    private bool isBunnyMoving;

    private void Awake()
    {
        _GameController = FindObjectOfType<GameController>();
        _SoundControl = FindObjectOfType<SoundControl>();

        rb2DBunnyComponent = GetComponent<Rigidbody2D>();
        bunnyAnimatorComponent = GetComponent<Animator>();

        sRenderBunnyComponent = GetComponent<SpriteRenderer>();
        movesBG = _GameController.movementBGScenary;

        horizontalMovementBunny = 1f;
    }

    void Update()
    {
        BunnyAnimations();
    }

    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") != 0f)
        {
            horizontalMovementBunny = Input.GetAxisRaw("Horizontal");
            rb2DBunnyComponent.velocity = new Vector2(horizontalMovementBunny * _GameController.movementSpeedBunny, rb2DBunnyComponent.velocity.y);
        }
        else if (isBunnyMoving == false)
        {
            rb2DBunnyComponent.velocity = new Vector2(0f, rb2DBunnyComponent.velocity.y);
        }

        jumpTimerBunny -= Time.fixedDeltaTime;
    }

    private void LateUpdate()
    {
        if (isPunctuatedBunny == true)
        {
            _GameController.ToScore(1);
            isPunctuatedBunny = false;
        }
    }

    private void BunnyAnimations()
    {
        isGroundedBunny = Physics2D.OverlapCircle(_GameController.checkGroundTransform.position, _GameController.groundCheckRadiusSize, _GameController.groundJumpLayer);

        bunnyAnimatorComponent.SetBool("isGrounded", isGroundedBunny);

        if (Input.GetButtonDown("Jump") && isGroundedBunny == true && isDeathBunny == false && jumpTimerBunny <= 0f)
        {
            jumpTimerBunny = 0.75f;
            rb2DBunnyComponent.AddForce(new Vector2(0f, _GameController.jumpForceBunny));
            bunnyAnimatorComponent.SetTrigger("isJumpedTrigger");
            _GameController.JumpBunnyFX();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Collectable":
                isPunctuatedBunny = true;
                Destroy(collision.gameObject);
                break;

            case "Obstacle":
                isDeathBunny = true;
                _GameController.movementSpeedBunny = 0f;
                GameObject[] tempGround = GameObject.FindGameObjectsWithTag("Ground");

                foreach (GameObject tempG in tempGround)
                {
                    tempG.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                }

                foreach (MoveOffsetBg movesScenary in movesBG)
                {
                    movesScenary.enabled = false;
                }

                _SoundControl.sfxAudioSource.Stop();

                if (jumpAfterObstacleCollision == false)
                {
                    rb2DBunnyComponent.velocity = Vector2.zero;
                    cColliderBunnyComponent.isTrigger = true;
                    _SoundControl.sfxSoundEffects.PlayOneShot(_SoundControl.obstacleDamageSoundEffect);
                    rb2DBunnyComponent.AddForce(new Vector2(0f, _GameController.jumpForceBunny));
                    _SoundControl.sfxSoundEffects.PlayOneShot(_SoundControl.deathSoundEffect);
                    sRenderBunnyComponent.sortingOrder = 10;
                    jumpAfterObstacleCollision = true;
                }

                bunnyAnimatorComponent.SetBool("isDeath", true);
                Invoke("SceneGameOver", 2.5f);
                break;

            case "LineDeathTrigger":
                _GameController.jumpForceBunny = 350f;
                rb2DBunnyComponent.AddForce(new Vector2(0f, _GameController.jumpForceBunny));
                Wait(2f);
                collision.gameObject.tag = "Obstacle";
                OnTriggerEnter2D(collision);
                break;

            case "EndOfScene":
                _GameController.ChangeScene("Stages");
                break;
        }
    }

    private void SceneGameOver()
    {
        PlayerPrefs.SetString("SceneName", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        _GameController.ChangeScene("GameOver");
    }

    private void Wait(float time)
    {
        if (time > 0f)
        {
            time -= Time.fixedDeltaTime;
        }
    }

    private void MOVE_RIGHT()
    {
        isBunnyMoving = true;
        rb2DBunnyComponent.velocity = new Vector2(1 * _GameController.movementSpeedBunny, rb2DBunnyComponent.velocity.y);
    }

    private void STOP_RIGHT()
    {
        isBunnyMoving = false;
        rb2DBunnyComponent.velocity = new Vector2(0f, rb2DBunnyComponent.velocity.y);
    }

    private void MOVE_LEFT()
    {
        isBunnyMoving = true;
        rb2DBunnyComponent.velocity = new Vector2(-1 * _GameController.movementSpeedBunny, rb2DBunnyComponent.velocity.y);
    }

    private void STOP_LEFT()
    {
        isBunnyMoving = false;
        rb2DBunnyComponent.velocity = new Vector2(0f, rb2DBunnyComponent.velocity.y);
    }

    private void JUMP()
    {
        if (isGroundedBunny == true && isDeathBunny == false && jumpTimerBunny <= 0f)
        {
            jumpTimerBunny = 0.75f;
            bunnyAnimatorComponent.SetBool("isGrounded", isGroundedBunny);

            rb2DBunnyComponent.AddForce(new Vector2(0f, _GameController.jumpForceBunny));
            bunnyAnimatorComponent.SetTrigger("isJumpedTrigger");
            _GameController.JumpBunnyFX();
        }
    }
}