using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    private Vector3 startingPosition;

    private void Start()
    {
        startingPosition= transform.position;
    }

    private Vector3 GetRomaingPosition()
    {
        return startingPosition + GetRandomDir() * Random.Range(10f, 70f);
    }

    private Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1, 1f), UnityEngine.Random.Range(-1, 1f)).normalized;
    }

}
