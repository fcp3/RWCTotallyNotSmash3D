using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    private int maxHealth;
    private int currentHealth;

    [SerializeField]
    private Image healthImage;

    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }

        set
        {
            maxHealth = value;
        }
    }

    public float CurrentHealth
    {

        set
        {
            
            Debug.Log("Max Health: " + maxHealth);
            currentHealth = (int) value;
            Debug.Log("Current Health: " + currentHealth);
            Debug.Log((float)currentHealth / maxHealth);
            healthImage.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    // Use this for initialization
    void Start () {
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
