using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{

    public Vector3 moveDirection;
    public float speed;
    public float velocity;
    bool move;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (move == false)
        {
            velocity -= Time.deltaTime;
            if (velocity <= 0)
            {
                velocity = 0;
            }
        }
        else
        {
            velocity += Time.deltaTime;
            if (velocity >= 1)
            {
                velocity = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            move = true;

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            move = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += moveDirection * Time.deltaTime * speed * velocity;
    }
}
