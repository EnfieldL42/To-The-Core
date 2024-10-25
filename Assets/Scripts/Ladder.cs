using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
	float horizontalMove = 0f;
	private bool IsClimbing;
	public float speed = 6;
	// Start is called before the first frame update
	void Start()
	{
		IsClimbing = false;
	}

	// Update is called once per frame
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player" && Input.GetKey(KeyCode.UpArrow)  &&  other.GetComponent<Movement>().CanMove == true)
		{
			other.GetComponent<Rigidbody2D>().gravityScale = 0;
			IsClimbing = true;
		}

			if (IsClimbing == true)
			{

        
		if (other.tag == "Player" && Input.GetKey(KeyCode.UpArrow))
		{
			
			other.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, speed);
			
		}
		else if (other.tag == "Player" && Input.GetKey(KeyCode.DownArrow))
		{
			other.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -speed);

		}
		else
		{
			
			other.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(horizontalMove, 0);

		}
		}

			

	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			collision.GetComponent<Rigidbody2D>().gravityScale = 1;
			IsClimbing = false;
		}
	}
  
}
