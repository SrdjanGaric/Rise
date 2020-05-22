using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour
{
    public float health = 100, stamina = 100, hunger = 100, energy = 100;
    public float maxHealth = 100, maxStamina = 100, maxHunger = 100, maxEnergy = 100;
    public float minHealth = 0, minStamina = 0, minHunger = 0, minEnergy = 0;

    PlayerMovement playerMovement;
    bool sprint, moving;

    private void Start()
    {
        playerMovement = GameObject.Find("First-Person Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerMovement.movementSpeed == 10)
        {
            sprint = true;
        }
        else
        {
            sprint = false;
        }

        if (playerMovement.input.x != 0 || playerMovement.input.z != 0)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        if (health <= minHealth)
        {
            Die();
        }

        if (health < maxHealth)
        {
            health += 1 * Time.deltaTime;

            if(health > maxHealth)
            {
                health = maxHealth;
            }
        }

        if (stamina > minStamina && ((sprint && moving) || !playerMovement.isGrounded))
        {
            stamina -= 10 * Time.deltaTime;

            if (stamina < minStamina)
            {
                stamina = minStamina;
            }
        }

        if (stamina < maxStamina && !sprint && playerMovement.isGrounded)
        {
            if (moving)
            {
                if(playerMovement.movementSpeed == 5)
                {
                    stamina += 0.5f * Time.deltaTime;
                }
                else if(playerMovement.movementSpeed == 2.5f)
                {
                    stamina += 1 * Time.deltaTime;
                }
            }
            else
            {
                stamina += 2 * Time.deltaTime;
            }

            if(stamina > maxStamina)
            {
                stamina = maxStamina;
            }
        }

        if (hunger > minHunger)
        {
            hunger -= 1 * Time.deltaTime;

            if (hunger < minHunger)
            {
                hunger = minHunger;
            }
        }

        if (hunger == minHunger)
        {
            health -= 2 * Time.deltaTime;
        }

        if (energy > minEnergy)
        {
            energy -= 1 * Time.deltaTime;

            if(energy < minEnergy)
            {
                energy = minEnergy;
            }
        }

        if (energy == minEnergy)
        {
            stamina -= 2 * Time.deltaTime;
        }

        GameObject.Find("Health Bar").GetComponent<Slider>().value = health;
        GameObject.Find("Stamina Bar").GetComponent<Slider>().value = stamina;
        GameObject.Find("Hunger Bar").GetComponent<Slider>().value = hunger;
        GameObject.Find("Energy Bar").GetComponent<Slider>().value = energy;
    }

    void Die()
    {
        print("YOU DIED");
    }
}
