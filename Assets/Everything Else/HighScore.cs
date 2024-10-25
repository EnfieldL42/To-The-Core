using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public Ship Ship;
    public TextMeshProUGUI Meter;
    public TextMeshProUGUI MeterHS;
    public int DepthHS;
  
    // Start is called before the first frame update
    void Start()
    {
        DepthHS = PlayerPrefs.GetInt("DepthHS");
    }

    // Update is called once per frame
    void Update()
    {
        if (DepthHS < Ship.Depth)
        {
           
            DepthHS = Ship.Depth;
            PlayerPrefs.SetInt("DepthHS", DepthHS);
        }
        Meter.text = (Ship.Depth.ToString() + ("m"));
        MeterHS.text = (DepthHS.ToString() + ("m"));

    }
}
