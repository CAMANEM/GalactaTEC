using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using audio_manager;

public class Enemy : MonoBehaviour
{

    public float moveSpeed = 0.2f;
    public bool kamikaze = false;
    public bool guardBonus = false;

    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private GameObject enemyShot;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject chargedShot;
    private bool alreadyShotCharged = false;
    private int level;




    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Rotate(180f, 0, 0);
        level = GameObject.Find("Canvas").GetComponent<GameSceneScript>().getLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (kamikaze)
        {
            moveKamikaze();
        }
        else if (guardBonus)
        {
            moveToBonus();
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

    public void kamikazeOrder(){
        gameObject.transform.SetParent(null);
        kamikaze = true;
    }

    private void moveKamikaze()
    {
        GameObject playerInstance = GameObject.Find("playerInstance");
        if (playerInstance != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerInstance.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    public void protectBonusOrder(){
        gameObject.transform.SetParent(null);
        guardBonus = true;
    }

    private void moveToBonus(){
        GameObject bonusInstance = GameObject.Find("Bonus");
        if (bonusInstance != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, bonusInstance.transform.position, 2f * Time.deltaTime);
        }
        else{
            guardBonus = false;
            kamikaze = true;
        }
    }



    private void destroy()
    {
        AudioManager.getInstance().playEnemyExplosionEffect();
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
        if (level == 1)
        {
            AudioManager.getInstance().playEnemyShot1Effect();
        }
        else if (level == 2)
        {
            AudioManager.getInstance().playEnemyShot2Effect();
        }
        else if (level == 3)
        {
            AudioManager.getInstance().playEnemyShot3Effect();
        }
        Instantiate(enemyShot, attackPoint.position, Quaternion.identity);
    }

    private void chargedShoot()
    {
        AudioManager.getInstance().playEnemyChargedShotEffect();
        Instantiate(chargedShot, attackPoint.position, Quaternion.identity);
    }

}
