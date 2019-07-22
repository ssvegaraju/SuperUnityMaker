using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Health : MonoBehaviour {

    public int maxHealth = 50;
    private int currentHealth;
    public int CurrentHealth {
        get { return currentHealth; }
    }

    public event Action OnHurt;
    public event Action OnDeath;

	void Start () {
        currentHealth = maxHealth;
	}
	
	void Update () {
		if (currentHealth <= 0) {
            if (OnDeath != null)
                OnDeath();
        }
	}

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (OnHurt != null)
            OnHurt();
    }
}
