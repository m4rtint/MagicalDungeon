using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    const string ATTACK_LEFT = "AttackLeft";
    const string ATTACK_RIGHT = "AttackRight";
    const string IDLE = "IdleRight";

    private Rigidbody2D playerRigidbody;
    private PlayerSpellController playerSpell;
    private Animator animate;

    // Use this for initialization
    void Start () {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerSpell = GetComponent<PlayerSpellController>();
        animate = GetComponent<Animator>();
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
        playerRigidbody.velocity = (Vector3.Normalize(direction) * playerSpell.MoveSpeed() * playerSpell.SpeedModifier());
    }

    public void castSpell()
    {
        //Convert the player to Screen coordinates
        Vector3 position = Utilities.worldToScreenObjectPosition(gameObject);
        position = Input.mousePosition - position;
        float angle = Utilities.getAngleDegBetween(position.y, position.x);

        string animationName = (angle > -90 && angle < 90) ? ATTACK_RIGHT : ATTACK_LEFT;
        animate.SetTrigger(animationName);
        
    }

    private void SpriteChange(Vector3 direction)
    {
        float x = direction.x;
        float y = direction.y;


        if (direction != Vector3.zero)
        {
            if (Mathf.Abs(x) >= Mathf.Abs(y)) //Look horizontally as priority
            {
                animate.SetBool(IDLE, (x > 0));
            }
            else
            {
                if (y > 0)
                {
                    //playerSpriteRenderer.sprite = upSprite;
                }
                else
                {
                    //playerSpriteRenderer.sprite = downSprite;
                }
            }
        }
        
    }

    #endregion
}
