using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GroundControl : MonoBehaviour
{
    private GameController _GameController;
    private Rigidbody2D rb2DGroundComponent;

    // Control variables
    private bool isInstantiated;


    private void Awake()
    {
        _GameController = FindObjectOfType<GameController>();
        rb2DGroundComponent = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb2DGroundComponent.velocity = new Vector2(_GameController.groundSpeedMovement, 0f);
    }

    void Update()
    {
        if (isInstantiated == false)
        {
            if (transform.position.x <= 0f)
            {
                isInstantiated = true;

                int randomNumberRespawnGround = Random.Range(0, 96);
                int indexGroundPrefab;

                if (randomNumberRespawnGround < 12)
                {
                    indexGroundPrefab = 0;
                }
                else if (randomNumberRespawnGround < 24)
                {
                    indexGroundPrefab = 1;
                }
                else if (randomNumberRespawnGround < 36)
                {
                    indexGroundPrefab = 2;
                }
                else if (randomNumberRespawnGround < 48)
                {
                    indexGroundPrefab = 3;
                }
                else if (randomNumberRespawnGround < 60)
                {
                    indexGroundPrefab = 4;
                }
                else if (randomNumberRespawnGround < 72)
                {
                    indexGroundPrefab = 5;
                }
                else if (randomNumberRespawnGround < 84)
                {
                    indexGroundPrefab = 6;
                }
                else
                {
                    indexGroundPrefab = 7;
                }

                _GameController.counterGroundScenario++;

                if (SceneManager.GetActiveScene().name == "StageEasy" && _GameController.counterGroundScenario == 7)
                {
                    indexGroundPrefab = 8;
                }

                if (SceneManager.GetActiveScene().name == "StageHard" && _GameController.counterGroundScenario == 10)
                {
                    indexGroundPrefab = 8;
                }

                GameObject groundTemp = Instantiate(_GameController.groundPrefab[indexGroundPrefab]);

                float instantiatePositionX = transform.position.x + _GameController.groundSizeForInstantiation;
                groundTemp.transform.position = new Vector2(instantiatePositionX, transform.position.y);
            }
        }

        if (transform.position.x <= _GameController.distanceToDestroyGround)
        {
            Destroy(this.gameObject);
        }
    }
}
