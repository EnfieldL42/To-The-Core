using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Ship : MonoBehaviour
{
    private PowerManager power;
    public ParticleSystem[] Thruster;
    public GameObject[] Light;
    public GameObject[] LightInt;
    public float Movespeed;
    public Rigidbody2D Rb;   
    public int Depth;
    


    // Start is called before the first frame update
    void Start()
    {
        power = GetComponent<PowerManager>();
        FindFirstObjectByType<AudioManager>().Play("Thruster");
        FindFirstObjectByType<AudioManager>().StopVolume("Thruster");
     
    }
    public void MoveShip(float ControlHorizontal, float ControlVertical)
    {
        if(ControlHorizontal > 0.2 | ControlHorizontal < -0.2 | ControlVertical > 0.2 | ControlVertical < -0.2)
        {
            power.DrainPower();
        }
      if (power.Power > 0)
        {
            Rb.AddForce(new Vector2(ControlHorizontal * Movespeed, ControlVertical * Movespeed));
            if (ControlHorizontal > 0.2 | ControlHorizontal < -0.2 | ControlVertical > 0.2 | ControlVertical < -0.2)
            {
                FindFirstObjectByType<AudioManager>().PlayVolume("Thruster", (ControlHorizontal* ControlHorizontal + ControlVertical* ControlVertical) /20);
            }
            else
            {
                FindFirstObjectByType<AudioManager>().StopVolume("Thruster");
            }
            if (ControlHorizontal > 0.1)
            {

                var emission1 = Thruster[0].emission;
                emission1.rateOverTime = ControlHorizontal * 100f;
                var emission2 = Thruster[4].emission;
                emission2.rateOverTime = ControlHorizontal * 100f;
                Light[0].SetActive(true);
                Light[1].SetActive(true);
                LightInt[0].SetActive(true);
                LightInt[1].SetActive(true);
            }
            else
            {
                var emission1 = Thruster[0].emission;
                emission1.rateOverTime = 0f;
                var emission2 = Thruster[4].emission;
                emission2.rateOverTime = 0f;
                Light[0].SetActive(false);
                Light[1].SetActive(false);
                LightInt[0].SetActive(false);
                LightInt[1].SetActive(false);
            }
            if (ControlHorizontal < -0.1)
            {
                var emission1 = Thruster[1].emission;
                emission1.rateOverTime = ControlHorizontal * -100f;
                var emission2 = Thruster[5].emission;
                emission2.rateOverTime = ControlHorizontal * -100f;
                Light[2].SetActive(true);
                Light[3].SetActive(true);
                LightInt[2].SetActive(true);
                LightInt[3].SetActive(true);
            }
            else
            {
                var emission1 = Thruster[1].emission;
                emission1.rateOverTime = 0f;
                var emission2 = Thruster[5].emission;
                emission2.rateOverTime = 0f;
                Light[2].SetActive(false);
                Light[3].SetActive(false);
                LightInt[2].SetActive(false);
                LightInt[3].SetActive(false);
            }
            if (ControlVertical < -0.1)
            {
                var emission1 = Thruster[2].emission;
                emission1.rateOverTime = ControlVertical * -200f;
                Light[4].SetActive(true);
                LightInt[4].SetActive(true);
            }
            else
            {
                var emission1 = Thruster[2].emission;
                emission1.rateOverTime = 0f;
                Light[4].SetActive(false);
                LightInt[4].SetActive(false);
            }
            if (ControlVertical > 0.1)
            {
                var emission1 = Thruster[3].emission;
                emission1.rateOverTime = ControlVertical * 100f;
                Light[5].SetActive(true);
                LightInt[5].SetActive(true);
            }
            else
            {
                var emission1 = Thruster[3].emission;
                emission1.rateOverTime = 0f;
                Light[5].SetActive(false);
                LightInt[5].SetActive(false);
            }

        }
        // Update is called once per frame

    }

    private void Update()
    {
        if (Rb.linearVelocity == new Vector2(0,0))
        {
            FindFirstObjectByType<AudioManager>().StopVolume("Thruster");
            var emission6 = Thruster[0].emission;
            emission6.rateOverTime = 0f;
            var emission5 = Thruster[4].emission;
            emission5.rateOverTime = 0f;
            Light[0].SetActive(false);
            Light[1].SetActive(false);
            LightInt[0].SetActive(false);
            LightInt[1].SetActive(false);
            var emission4 = Thruster[1].emission;
            emission4.rateOverTime = 0f;
            var emission3 = Thruster[5].emission;
            emission3.rateOverTime = 0f;
            Light[2].SetActive(false);
            Light[3].SetActive(false);
            LightInt[2].SetActive(false);
            LightInt[3].SetActive(false);
            var emission2 = Thruster[2].emission;
            emission2.rateOverTime = 0f;
            Light[4].SetActive(false);
            LightInt[4].SetActive(false);
            var emission1 = Thruster[3].emission;
            emission1.rateOverTime = 0f;
            Light[5].SetActive(false);
            LightInt[5].SetActive(false);
        }
        Depth = (int)transform.position.y*-1;
        
    }
}
