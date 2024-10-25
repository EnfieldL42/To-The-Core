using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Spawner : MonoBehaviour
    {
        public Vector2 Cooldown;
        private float LastSpawn;
        public GameObject SpawnedObject;
        public Vector2 ProjectileSpeed;
   private bool used = false;
    public Ship ship;

        // Start is called before the first frame update
        void Start()
        {
            
        Cooldown = new Vector2(50, 60);
        LastSpawn = Time.time + Random.Range(Cooldown.x, Cooldown.y);
    }

        // Update is called once per frame
        void Update()
        {
        if (Cooldown.x > 4)
        {
            Cooldown = new Vector2(50 - ((float)ship.Depth / 4), 60 - ((float)ship.Depth / 4));
        }
        else if (Cooldown.x < 4)
        {
            Cooldown = new Vector2(2f, 20);
        }
       
     
        if (Cooldown.x < 20f & !used)
        {
            LastSpawn = Time.time + Random.Range(Cooldown.x, Cooldown.y);
            used = true;
        }
            if (LastSpawn < Time.time)
            {
                LastSpawn = Time.time + Random.Range(Cooldown.x, Cooldown.y);
                GameObject clone = Instantiate(SpawnedObject, transform.position, transform.rotation);
                SpawnedObject.GetComponent<Boulder>().Speed = Random.Range(ProjectileSpeed.x, ProjectileSpeed.y);
                Destroy(clone, 20.0f);
            }
        }
    }
