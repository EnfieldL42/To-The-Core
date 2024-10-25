using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject LaserHit;
    public GameObject LaserParticles;
    public Gun Gun;
    public bool IsControlled = false;
    
    public Camera cam;
    public LineRenderer lineRenderer;
    public Transform firepoint;
    private AudioSource Laser;
    public float HorizontalMove;
    public float VerticalMove;
    public bool ActionToggle;

    // Start is called before the first frame update
    void Start()
    {
        Laser = gameObject.GetComponent<AudioSource>();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        LaserParticles.SetActive(false);
        DisableLaser();
        
    }
    private void Update()
    {
        if (ActionToggle & Gun.IsLaser == false)
        {
            Gun.Anim.SetBool("IsShooting", true);
        }
        if (!ActionToggle & Gun.IsLaser == false)
        {
            Gun.Anim.SetBool("IsShooting", false);
        }
        if (ActionToggle)
        {
            if (IsControlled == true & Gun.IsLaser == true)
            {
                EnableLaser();
            }
            if (IsControlled == true)
            {
                Gun.Shoot();
                Gun.Mine();
                UpdateLaser();
            }
        }
        if (IsControlled == true & Gun.IsLaser == true)
        {
            if (ActionToggle)
            {

                UpdateLaser();
            }
            
            if (!ActionToggle)
            {
                DisableLaser();
            }
        }
        else
        {
            DisableLaser();
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsControlled == true)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            if (HorizontalMove < 0)
            {
                Gun.RotateLeft(HorizontalMove);
            }
            if (HorizontalMove > 0)
            {
                Gun.RotateRight(HorizontalMove);
            }
            
            
        }
    }
    public void Action()
    {
        if (IsControlled == true & Gun.IsLaser == true)
        {
            EnableLaser();
        }
        if (IsControlled == true)
        {
            if (Gun.IsLaser == false)
            {
                
                    
                
                
            }
            Gun.Shoot();
            Gun.Mine();
            UpdateLaser();
        }

           
    }
    void EnableLaser()
    {
        Laser.volume = 0.1f;
        
        LaserHit.SetActive(true);
        LaserParticles.SetActive(true);
        lineRenderer.enabled = true;
    }
    void UpdateLaser()
    {
        LaserHit.transform.position = Gun.HitpointUpdate;
        lineRenderer.SetPosition(0, firepoint.position);
        lineRenderer.SetPosition(0, firepoint.position);
        lineRenderer.SetPosition(1, Gun.HitpointUpdate);
    }
    void DisableLaser()
    {
        Laser.volume = 0;
        LaserHit.SetActive(false);
        LaserParticles.SetActive(false);
        lineRenderer.enabled = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!IsControlled & collision.gameObject.CompareTag("Player")) 
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = enabled;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
