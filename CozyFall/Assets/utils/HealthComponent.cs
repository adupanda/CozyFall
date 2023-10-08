using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class HealthComponent : MonoBehaviour
{
    public event EventHandler OnHealthChanged;

    public int maxHealth = 10;

    public int currentHealth;

    public bool isImmune;

    public float immunityDuration;

    private Color flashColor = Color.red;

    private Color orignalColor;

    private float flashSpeed = 0.3f;

    private void Start()
    {
        currentHealth = maxHealth;
        
    }

    

    public void TakeDamage(int damage)
    {
        if(!isImmune)
        {
            currentHealth -= damage;


            OnDeath();

            flashStart();

            if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);

            Debug.Log("DamageTaken");
            StartCoroutine(ImmunityFrames());
        }
        
        
        
    }

    IEnumerator ImmunityFrames()
    { 
        isImmune = true;
        yield return new WaitForSeconds(immunityDuration);
        isImmune = false;
    }

    public void AddHealth(int health) 
    { 
        currentHealth += health;
        if(currentHealth> maxHealth)
        {
            currentHealth = maxHealth;  
        }
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    private void OnDeath()
    {
        if (currentHealth <= 0) 
        {
            
            currentHealth = maxHealth;
        }
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public float GetHealthPercent()
    {
        return currentHealth/ maxHealth;
    }

    


    private void flashStart()
    {
        
        Invoke("flashStop", flashSpeed);
    }
    private void flashStop()
    {
        
    }

}
