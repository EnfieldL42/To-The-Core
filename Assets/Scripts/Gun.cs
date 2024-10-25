using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Gun : MonoBehaviour
{
    public bool InvertControls;
    private float timebtwshots;
    public float startTimeBtwShots;
    public GameObject projectile;
    public bool IsLaser;
    public GameObject breakParticlesPrefab;
    public int breakRadius = 5;
    public float charge = 0f;
    public Tilemap tilemap;
    public float chargeTime;
    public LayerMask playerr;
    public Vector2 Hitpoint;
    public Vector2 HitpointUpdate;
    public Rigidbody2D rb;
    private float Rotation;
    public GameObject Guun;
    public Transform Firepoint;
    public Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
       if (!IsLaser)
        {
            Anim = gameObject.GetComponent<Animator>();
        }
    }
    public void Mine()
    {
        
       if (IsLaser)
        {
            charge += Time.deltaTime;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 500f, ~playerr);
            Debug.DrawRay(transform.position, transform.up, Color.green);

            // If it hits something...
            if (hit.collider != null)
            {
                HitpointUpdate = hit.point;
                if (charge > chargeTime)
                {

                    charge = 0f;
                    GameObject breakParticles = Instantiate(breakParticlesPrefab, hit.point, Quaternion.identity);
                    //print(hit.point.ToString());
                    Hitpoint = hit.point;
                    FindFirstObjectByType<AudioManager>().PlayP("Break");
                    Destroy(breakParticles, 1f);
                    for (int y = -breakRadius; y < breakRadius + 1; y++)
                    {
                        for (int i = -breakRadius; i <= breakRadius; i++)
                        {
                            for (int x = -breakRadius; x <= breakRadius; x++)
                            {
                                // Convert hit.point to tile position
                                Vector3Int tilePos = tilemap.WorldToCell(hit.point) + new Vector3Int(x, y, 0);

                                // Clear the tile at the calculated position
                                tilemap.SetTile(tilePos, null);
                            }
                        }

                    }


                }
            }

            else
            {
                charge = 0f;
            }
        }
       
    }
    public void RotateLeft(float control)
    {
        if (InvertControls)
        {
            if (Rotation < 270 & control < -0.2)
            {

                transform.Rotate(0, 0, 60 * Time.deltaTime);
            }
        }
        else
        {
            if (Rotation > 140 & control < -0.2)
            {

                transform.Rotate(0, 0, -60 * Time.deltaTime);
            }
        }
        
    }
    public void RotateRight(float control)
    {
        if (InvertControls)
        {
            if (Rotation > 140 & control > 0.2)
            {
                transform.Rotate(0, 0, -60 * Time.deltaTime);
            }
        }
        else
        {
            if (Rotation < 270 & control > 0.2)
            {
                transform.Rotate(0, 0, 60 * Time.deltaTime);
            }
        }
           
    }
    public void Shoot()
    {
        if (!IsLaser)
        {
           
            if(timebtwshots <= 0)
            {
                FindFirstObjectByType<AudioManager>().PlayP("Gun");
                Instantiate(projectile, Firepoint.position, transform.rotation);
                timebtwshots = startTimeBtwShots;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {      
        Rotation = transform.localEulerAngles.z;
        if (timebtwshots <= 0)
        {

        }
        else
        {
            timebtwshots -= Time.deltaTime;
        }


    }
    private void FixedUpdate()
    {
        
    }
}
