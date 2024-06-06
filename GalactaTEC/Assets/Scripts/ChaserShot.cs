using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using audio_manager;

public class ChaserShot : MonoBehaviour
{

    public float moveSpeed = 0.4f;
    private PointManager pointManager;
    private string targetName;
    public GameObject explosion;
    
    // Start is called before the first frame update
    void Start()
    {
        aimTarget();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        GameObject enemyTarget = GameObject.Find(targetName);
        if (enemyTarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, enemyTarget.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            aimTarget();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy"){
            destroy();
        }
        else if (collision.gameObject.tag == "EnemyShot")
        {
            destroy();
        }
        else if (collision.gameObject.tag == "HorizontalBoundary")
        {
            destroy();
        }
    }

    private void destroy()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void aimTarget(){
        var enemies = GameObject.FindGameObjectsWithTag ("Enemy");
        if (0 < enemies.Length)
        {
            targetName = enemies[0].name;
        }
        else
        {
            destroy();
        }
    }
}
