using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public int xPos;
    public int zPos;
    public int enemyCount;
    public float spawnTime = 30f;
    float leftmost = 20f;
    float rightmost = 80f;
    float upmost = 95f;
    float down = 35f;

    void Start ()
    {
        //InvokeRepeating ("Spawn", spawnTime, spawnTime);
        StartCoroutine(EnemyDrop());
    }


    IEnumerator EnemyDrop()
    {
        while (enemyCount < float.MaxValue)
        {
            xPos = Random.Range(0, 120);
            zPos = Random.Range(15, 135);
            if (xPos >= leftmost && xPos <= rightmost && zPos >= down && zPos <= upmost)
			{
                continue;
            }   
            Instantiate(enemy, new Vector3(xPos, 13, zPos), Quaternion.identity);
            //Debug.Log("Enemy spawned at position x " + xPos + " z " + zPos);
            yield return new WaitForSeconds(spawnTime);
            enemyCount += 1;
        }
    }


}
