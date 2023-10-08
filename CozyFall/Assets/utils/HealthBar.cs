using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private HealthComponent healthComponent;

    public void Setup(HealthComponent healthComponent)
    {
        this.healthComponent = healthComponent;

        healthComponent.OnHealthChanged += HealthComponent_OnHealthChanged;
    }

    private void HealthComponent_OnHealthChanged(object sender, System.EventArgs e)
    {
        transform.Find("Bar").localScale = new Vector3(healthComponent.GetHealthPercent(), 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
