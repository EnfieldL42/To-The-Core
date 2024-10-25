using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject Ship;
    private PowerManager power;
    public float speed;
    public GameObject breakParticlesPrefab;
    private void Start()
    {
        Ship = GameObject.Find("ShipOutside");
        power = Ship.GetComponent<PowerManager>();
        Invoke("DestroyProjectile", 10f);
        
    }
    private void Update()
    {
        transform.Translate(new Vector3(0,1,0) * speed * Time.deltaTime);

    }
    void DestroyProjectile()
    {
        GameObject breakParticles = Instantiate(breakParticlesPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("Boulder"))
        {
            power.AddPower(33);
            
            collision.gameObject.GetComponent<Boulder>().Destroy();
            DestroyProjectile();
        }
        
        
    }
   
}
