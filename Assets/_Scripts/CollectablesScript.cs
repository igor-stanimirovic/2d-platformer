using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectablesScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointsText;
    int points = 0;

    // Method for handeling collectables
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            Destroy(collision.gameObject);
            points++;
            pointsText.text = "Points: " + points;
        }
    }
}
