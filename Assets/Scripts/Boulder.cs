using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Boulder : MonoBehaviour
    {
    private Health Health;
        public float Speed;
        private Rigidbody2D Rb;
        public GameObject breakParticlesPrefab;
        public CameraShake Camera;
        // Start is called before the first frame update
        void Start()
        {
            Rb = GetComponent<Rigidbody2D>();
            Rb.linearVelocity = new Vector2(0, -Speed);
        }

        // Update is called once per frame
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ship"))
            {
            collision.GetComponent<Health>().Damage(10);            
                
         
            Destroy();

            }
        }
    public void Destroy()
    {
        FindFirstObjectByType<AudioManager>().PlayP("ShipHit");
        GameObject breakParticles = Instantiate(breakParticlesPrefab, transform.position, Quaternion.identity);
        Destroy(breakParticles, 1f);
        CameraShake.Instance.Shake(3, 0.2f);
        Destroy(gameObject);
    }
    }
