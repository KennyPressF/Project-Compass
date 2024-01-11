using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health;

    public void TakeDamage()
    {
        health--;

        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("THE PLAYER DIED");
        //Disable the PlayerController script + whatever else could still be an issue
    }
}
