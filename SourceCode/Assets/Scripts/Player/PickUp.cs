using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
        // Zmiana hunger i thirst w zaleznosci od podniesionego itema
        if (playerController != null)
        {
            if (gameObject.tag == "kebab")
            {
                audioManager.PlaySFX(audioManager.eat);
                playerController.currentHunger += 25;
                playerController.AddScore(25);
            }
            if (gameObject.tag == "potworek")
            {
                audioManager.PlaySFX(audioManager.can);
                playerController.currentThirst += 25;
                playerController.AddScore(25);
            }
            if (gameObject.tag == "potworek_gold")
            {
                audioManager.PlaySFX(audioManager.can);
                playerController.currentHealth += 5;
                playerController.currentThirst += 50;
                playerController.currentHunger += 50;
                playerController.AddScore(50);
            }

            // Sprawdzenie czy hunger, thirst i health nie przekraczaja wartosci max.
            if (playerController.currentHunger > playerController.maxHunger)
            {
                playerController.currentHunger = playerController.maxHunger;
            }
            if (playerController.currentThirst > playerController.maxThirst)
            {
                playerController.currentThirst = playerController.maxThirst;
            }
            if (playerController.currentHealth > playerController.health)
            {
                playerController.currentHealth = playerController.health;
            }

            // Ustawienie wartosci w UI
            playerController.hungerBar.SetHunger(playerController.currentHunger);
            playerController.thirstBar.SetThirst(playerController.currentThirst);
            playerController.healthBar.SetHealth(playerController.currentHealth);

            Destroy(gameObject);
            Debug.Log("Podnios³eœ " + gameObject.tag);
            
        }
    }
}
