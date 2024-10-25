using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class PowerManager : MonoBehaviour
    {
        public GameObject[] Indicator;
        public float Power;
        public int Drainage;
    public GameObject[] LightGreen;
    public GameObject[] LightRed;
    // Start is called before the first frame update
    void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Power > 1)
            {
                Indicator[0].SetActive(true);
            LightGreen[0].SetActive(true);
            LightRed[0].SetActive(false);
            if (Power > 33)
                {
                    Indicator[1].SetActive(true);
                LightGreen[1].SetActive(true);
                LightRed[1].SetActive(false);
                if (Power > 66)
                    {
                        Indicator[2].SetActive(true);
                    LightGreen[2].SetActive(true);
                    LightRed[2].SetActive(false);
                }
                }
            }
            if (Power < 66)
            {
                Indicator[2].SetActive(false);
            LightGreen[2].SetActive(false);
            LightRed[2].SetActive(true);
            if (Power < 33)
                {
                    Indicator[1].SetActive(false);
                LightGreen[1].SetActive(false);
                LightRed[1].SetActive(true);
                if (Power < 1)
                    {
                        Indicator[0].SetActive(false);
                    LightGreen[0].SetActive(false);
                    LightRed[0].SetActive(true);
                }
                }
            }
            if (Power > 100)
            {
                Power = 100f;
            }
        }
        public void AddPower(int amount)
        {
            if (Power < 100)
            {
                Power += amount;
            }

        }
        public void DrainPower()
        {
            if (Power > 0)
            {
                Power -= Time.deltaTime * Drainage;
            }
            else
            {
                Power = 0;
            }
        }
    }

