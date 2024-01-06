using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script for handeling player death
public class PlayerLive : MonoBehaviour
{
    [SerializeField] GameObject deathScene;
    Animator anim;
    bool dead = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (dead && Input.GetButtonDown("Jump"))
        {
            LevelReloader();
            deathScene.SetActive(false);
            dead = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            PlayerDeath();
        }
    }

    void PlayerDeath()
    {
        dead = true;
        anim.SetTrigger("death");
        deathScene.SetActive(true);
    }

    void LevelReloader()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
