using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Health Health;
    public Transform Bar;
    private void Update()
    {
        Bar.transform.localScale = new Vector3(Health.healthPercent, 1);
    }
}
