using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float Movespeed;
    public Sprite upSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite downSprite;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 direction = InputManager.MainInput(); //Get input
        Move(direction);
        SpriteChange(direction);


	}

    private void Move (Vector3 direction)
    {
        transform.Translate(Vector3.Normalize(direction) * Time.deltaTime * Movespeed);
    }

    private void SpriteChange(Vector3 direction)
    {
        if (Input.GetMouseButton(0)) //If mousebutton is down, look at mouse
        {
            //Convert the player to Screen coordinates
            Vector3 position = Camera.main.WorldToScreenPoint(transform.position);
            position = Input.mousePosition - position;
            float angle = Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg;
            if ((angle <= 45 && angle >= 0) || (angle >= -45 && angle <= 0))
            {
                GetComponent<SpriteRenderer>().sprite = rightSprite;
            }
            else if (angle >= 135 || angle <= -135)
            {
                GetComponent<SpriteRenderer>().sprite = leftSprite;
            }
            else if (angle > 0)
            {
                GetComponent<SpriteRenderer>().sprite = upSprite;
            }
            else if (angle < 0)
            {
                GetComponent<SpriteRenderer>().sprite = downSprite;
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
                    GetComponent<SpriteRenderer>().sprite = rightSprite;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = leftSprite;
                }
            }
            else
            {
                if (y > 0)
                {
                    GetComponent<SpriteRenderer>().sprite = upSprite;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = downSprite;
                }
            }

        }

            



    }


}
