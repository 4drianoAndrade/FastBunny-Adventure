using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpin : MonoBehaviour
{
    private GameController _GameController;
    private SpriteRenderer sRGroundComponent;
    private bool isSpinneThePlate = false;

    private void Awake()
    {
        _GameController = FindObjectOfType<GameController>();
        sRGroundComponent = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Bunny":
                sRGroundComponent.flipX = true;

                if (isSpinneThePlate == false)
                {
                    _GameController.SpinPlateFX();
                    isSpinneThePlate = true;
                }

                break;
        }
    }
}
