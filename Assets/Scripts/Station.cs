using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public bool IsControlledStation = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsControlledStation)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!IsControlledStation & collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = enabled;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
