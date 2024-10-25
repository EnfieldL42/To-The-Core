using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Gun : MonoBehaviour
{
    // Existing gun properties
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

    void Start()
    {
        if (!IsLaser)
        {
            Anim = gameObject.GetComponent<Animator>();
        }
    }

    public void Mine()
    {
        if (!IsLaser) return;

        charge += Time.deltaTime;

        // Adjust the raycast origin slightly forward to avoid self-collision
        Vector2 rayOrigin = (Vector2)transform.position + (Vector2)transform.up * 0.1f;

        // Use ContactFilter2D for more precise collision detection
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(~playerr);
        filter.useLayerMask = true;

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, transform.up, 500f, ~playerr);

        Debug.DrawRay(rayOrigin, transform.up * 500f, Color.green);

        if (hit.collider != null)
        {
            HitpointUpdate = hit.point;

            if (charge > chargeTime)
            {
                charge = 0f;
                BreakTiles(hit.point);
            }
        }
        else
        {
            charge = 0f;
        }
    }

    private void BreakTiles(Vector2 hitPoint)
    {
        // Spawn break particles
        GameObject breakParticles = Instantiate(breakParticlesPrefab, hitPoint, Quaternion.identity);
        Destroy(breakParticles, 1f);

        // Play break sound
        FindFirstObjectByType<AudioManager>()?.PlayP("Break");

        // Store hit point for reference
        Hitpoint = hitPoint;

        // Convert world position to cell position
        Vector3Int cellPosition = tilemap.WorldToCell(hitPoint);

        // Break tiles in radius (keeping your triple loop structure)
        for (int y = -breakRadius; y < breakRadius + 1; y++)
        {
            for (int i = -breakRadius; i <= breakRadius; i++)
            {
                for (int x = -breakRadius; x <= breakRadius; x++)
                {
                    Vector3Int tilePos = cellPosition + new Vector3Int(x, y, 0);

                    // Only break if there's actually a tile there
                    if (tilemap.HasTile(tilePos))
                    {
                        tilemap.SetTile(tilePos, null);
                    }
                }
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
            if (timebtwshots <= 0)
            {
                FindFirstObjectByType<AudioManager>().PlayP("Gun");
                Instantiate(projectile, Firepoint.position, transform.rotation);
                timebtwshots = startTimeBtwShots;
            }
        }
    }

    void Update()
    {
        Rotation = transform.localEulerAngles.z;
        if (timebtwshots > 0)
        {
            timebtwshots -= Time.deltaTime;
        }
    }
}