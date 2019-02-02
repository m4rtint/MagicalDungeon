using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ICharacter {
    
    public Sprite upSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite downSprite;

    private Rigidbody2D playerRigidbody;
    private SpriteRenderer playerSpriteRenderer;

    // Created a separate player speed for Haste
    public float speedModifier = 1;


    // Use this for initialization
    void Start () {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        decrementHealth(80f);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 direction = InputManager.MainInput(); //Get input
        Move(direction);
        SpriteChange(direction);
    }

    #region CharacterMovement

    //Behaviour while colliding with another solid object e.g enemy meele


    private void Move (Vector3 direction)
    {
        playerRigidbody.velocity = (Vector3.Normalize(direction) * base.moveSpeed * speedModifier);
    }

    private void SpriteChange(Vector3 direction)
    {
        if (Input.GetMouseButton(0)) //If mousebutton is down, look at mouse
        {
            //Convert the player to Screen coordinates
            Vector3 position = Utilities.worldToScreenObjectPosition(gameObject);
            position = Input.mousePosition - position;
            float angle = Utilities.getAngleDegBetween(position.y, position.x);
            if ((angle <= 45 && angle >= 0) || (angle >= -45 && angle <= 0))
            {
                playerSpriteRenderer.sprite = rightSprite;
            }
            else if (angle >= 135 || angle <= -135)
            {
                playerSpriteRenderer.sprite = leftSprite;
            }
            else if (angle > 0)
            {
                playerSpriteRenderer.sprite = upSprite;
            }
            else if (angle < 0)
            {
                playerSpriteRenderer.sprite = downSprite;
            }
        }
        else //Else look depending on direction
        {
            float x = direction.x;
            float y = direction.y;


            if (direction == Vector3.zero)
            {
                return;
            }
            if (Mathf.Abs(x) >= Mathf.Abs(y)) //Look horizontally as priority
            {
                if (x > 0)
                {
                    playerSpriteRenderer.sprite = rightSprite;
                }
                else
                {
                    playerSpriteRenderer.sprite = leftSprite;
                }
            }
            else
            {
                if (y > 0)
                {
                    playerSpriteRenderer.sprite = upSprite;
                }
                else
                {
                    playerSpriteRenderer.sprite = downSprite;
                }
            }
        }
    }

    #endregion

    #region override
    public override void incrementHealth(float heal)
    {
        base.incrementHealth(heal);
        GetComponent<PlayerSpellController>().onStateChange(STATE.HEAL);
    }
    #endregion
}
