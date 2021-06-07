using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RespawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject spwnObj;
    int xPos;
    int zPos;
    [SerializeField]
    int objCount;
    public float spawnTime = 15f;
    float leftmost = 20f;
    float rightmost = 80f;
    float upmost = 95f;
    float down = 35f;

    void Start()
    {
        StartCoroutine(EnemyDrop());
    }


    IEnumerator EnemyDrop()
    {
        while (objCount < float.MaxValue)
        {
            xPos = Random.Range(0, 120);
            zPos = Random.Range(15, 135);
            if (xPos >= leftmost && xPos <= rightmost && zPos >= down && zPos <= upmost)
            {
                continue;
            }
            Instantiate(spwnObj, new Vector3(xPos, 12, zPos), Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);
            objCount += 1;
        }
    }
}
