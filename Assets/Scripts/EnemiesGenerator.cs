using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour
{
    public GameObject enemyType1;

    private float spawnXPosition1 = -1.5f;
    private float spawnXPosition2 = 0f;
    private float spawnXPosition3 = 1f;
    float[] spawnPositions = new float[3];

    private float enemySpawnTime = 2.2f;
    private float elapsedTime = 0;

    Transform myTransform;

    private void Start()
    {
        myTransform = GetComponent<Transform>();

        spawnPositions[0] = spawnXPosition1;
        spawnPositions[1] = spawnXPosition2;
        spawnPositions[2] = spawnXPosition3;
    }

    private int randomSelection = 0;
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= enemySpawnTime)
        {
            randomSelection = Random.Range(0, 2);
            Instantiate(enemyType1, new Vector3(spawnPositions[randomSelection], myTransform.position.y, myTransform.position.z), Quaternion.identity);
            elapsedTime = 0;
        }
    }
}
