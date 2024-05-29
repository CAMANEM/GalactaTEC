using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArmyController : MonoBehaviour
{
    public GameObject[] enemies = new GameObject[20];
    public GameObject klaedBattlecruiser;
    public Transform[] enemiesPosition;
    public int enemyType = 0;
    
    

    // Start is called before the first frame update
    void Start()
    {
        var enemy = klaedBattlecruiser;
        switch (enemyType)
        {
            case 0:
                enemy = klaedBattlecruiser;
                break;
        }

        for (int i = 0; i < 20; i++)
        {
            Vector3 newPos = enemiesPosition[i].position;
            newPos.z = 0f;
            GameObject newEnemy = Instantiate(enemy, newPos, Quaternion.identity);
            newEnemy.transform.SetParent(transform);
            enemies[i] = newEnemy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
