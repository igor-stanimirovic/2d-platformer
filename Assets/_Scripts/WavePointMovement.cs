using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePointMovement : MonoBehaviour
{
    [SerializeField] GameObject[] wavePoints;
    [SerializeField] float movementSpeed = 2.0f;
    int wavePointIndex = 0;

    void Update()
    {
        if (Vector2.Distance(wavePoints[wavePointIndex].transform.position, transform.position) < 0.1f)
        {
            wavePointIndex++;
            if (wavePointIndex >= wavePoints.Length)
            {
                wavePointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, wavePoints[wavePointIndex].transform.position, movementSpeed * Time.deltaTime);

    }
}
