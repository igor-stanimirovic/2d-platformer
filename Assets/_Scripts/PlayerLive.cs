using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

// Script for handeling player death
public class PlayerLive : MonoBehaviour
{
    [SerializeField] GameObject deathScene;
    [SerializeField] GameObject pointText;
    [SerializeField] AudioSource deathSound;
    PlayerMovement playerMovementScript;
    Animator anim;
    Rigidbody2D rb;
    float yBound = -5;
    bool isDead = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovementScript = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        // Space to restart the Level
        if (isDead && Input.GetButtonDown("Jump"))
        {
            LevelReloader();
            deathScene.SetActive(false);
            isDead = false;
        }

        // Eec to go to the Start screen
        if (isDead && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            PlayerDeath();
        }
    }

    // Player dieing method
    void PlayerDeath()
    {
        deathSound.Play();
        isDead = true;
        playerMovementScript.enabled = false;
        anim.SetTrigger("death");
        deathScene.SetActive(true);
        pointText.SetActive(false);
        rb.isKinematic = true; // dissables Physics on object
    }

    // Reloading level method
    void LevelReloader()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        playerMovementScript.enabled = true;
        pointText.SetActive(true);
        rb.isKinematic = false;
    }

}
