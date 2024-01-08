using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowSecretText : MonoBehaviour
{
    [SerializeField] GameObject secretText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level 1" && transform.position.x == -5.56f)
        {
            secretText.SetActive(true);
        } else
        {
            secretText.SetActive(false);
        }
    }
}
