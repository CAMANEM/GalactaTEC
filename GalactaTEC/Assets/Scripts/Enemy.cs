using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float moveSpeed = 0.2f;
    public bool kamikaze = false;

    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private GameObject enemyShot;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject chargedShot;
    private bool alreadyShotCharged = false;




    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Rotate(180f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (kamikaze)
        {
            moveKamikaze();
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        Debug.Log("Colision");
        if (collision.gameObject.tag == "Player")
        {
            destroy();
        }
        else if (collision.gameObject.tag == "PlayerShot")
        {
            destroy();
        }
        else
        {
            GameObject.Find("EnemyArmy").GetComponent<EnemyArmy>().OnCollisionEnter2D(collision);
        }
    }

    void moveKamikaze()
    {
        GameObject playerInstance = GameObject.Find("playerInstance");
        transform.position = Vector3.MoveTowards(transform.position, playerInstance.transform.position, moveSpeed * Time.deltaTime);
    }



    private void destroy()
    {
        Spawner spawnerScript = GameObject.Find("MainCamera").GetComponent<Spawner>();
        spawnerScript.enemyDestroyed(gameObject.name);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void shoot(){

        if (alreadyShotCharged)
        {
            normalShoot();
        }
        else
        {
            int randNum = Random.Range(0, 5);
            if (randNum == 1)
            {
                chargedShoot();
                alreadyShotCharged = true;
            }
            else
            {
                normalShoot();
            }    
        }
    }

    private void normalShoot()
    {
        Instantiate(enemyShot, attackPoint.position, Quaternion.identity);
    }

    private void chargedShoot()
    {
        Instantiate(chargedShot, attackPoint.position, Quaternion.identity);
    }

}
