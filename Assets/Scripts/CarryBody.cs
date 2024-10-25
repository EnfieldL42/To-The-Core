using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryBody : MonoBehaviour
{
    public List<Rigidbody2D> bodies = new List<Rigidbody2D>();
    public Vector3 LastPos;
    Transform _transform;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        LastPos = _transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        if(bodies.Count > 0)
        {
            for (int i = 0; i < bodies.Count; i++)
            {
                Rigidbody2D rb = bodies[i];
                Vector3 velocity = (_transform.position - LastPos);
                rb.transform.Translate(velocity, _transform);
            }
            LastPos = _transform.position;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        

    }
}
