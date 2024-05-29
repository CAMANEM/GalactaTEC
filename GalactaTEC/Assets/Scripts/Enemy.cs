using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float moveSpeed = 0.2f;
    public float movementDistanceX = -0.5f;
    public float movementDistanceY = 0.5f;

    public byte movePattern = 0;
    private Vector3 destinyPosition;

    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private GameObject enemyShot;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject chargedShot;



    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Rotate(180f, 0, 0);
        destinyPosition = transform.position - new Vector3(0, movementDistanceY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        move();
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(enemyShot, attackPoint.position, Quaternion.identity);
        }
    }

    /*
        Make the enemy move according with its pattern
    */
    void move(){
        switch(movePattern){
            case 0:
                moveZigzag();
                break;
            case 1:
                kamikaze(); 
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        Debug.Log("Colision");
        if (collision.gameObject.tag == "HorizontalBoundary" || collision.gameObject.tag == "VerticalBoundary")
        {
            switch(movePattern){
                case 0:
                    zigzagLimit(collision);
                    break;
            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            destroy();
        }
        else if (collision.gameObject.tag == "PlayerShot")
        {
            destroy();
        }
    }


    /*
        Movement pattern #0
        Moves the enemy with a zigzag pattern between screen borders
    */
    void moveZigzag(){
        if (transform.position == destinyPosition)
        {
            destinyPosition = transform.position - new Vector3( movementDistanceX, 0, 0);
        }
        transform.position = Vector3.MoveTowards(transform.position, destinyPosition, moveSpeed * Time.deltaTime);
    }

    /*
        Movement pattern #0
        Controls the movement when the enemy collides with a screen border
    */
    void zigzagLimit(Collision2D collision){

        if (collision.gameObject.tag == "VerticalBoundary")
        {
            Vector3 newPos = transform.position;
            newPos.x -= 3.8f;
            transform.position = newPos;
            destinyPosition = transform.position - new Vector3(0, movementDistanceY, 0);
        }
        else if (collision.gameObject.tag == "HorizontalBoundary")
        {
            Vector3 newPos = transform.position;
            newPos.x += 0.5f;
            newPos.y += 2f;
            transform.position = newPos;
            destinyPosition = transform.position - new Vector3(0, movementDistanceY, 0);
        }
        
    }

    void kamikaze()
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

    public void normalShoot()
    {
        Instantiate(enemyShot, attackPoint.position, Quaternion.identity);
    }

    public void chargedShoot()
    {
        Instantiate(chargedShot, attackPoint.position, Quaternion.identity);
    }
}
