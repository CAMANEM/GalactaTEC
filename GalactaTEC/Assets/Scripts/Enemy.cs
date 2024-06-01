using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float moveSpeed = 0.2f;
    public float movementDistanceX = -0.5f;
    public float movementDistanceY = 0.5f;
    private bool moveLaterally = true;

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
    private bool alreadyShotCharged = false;




    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Rotate(180f, 0, 0);
        destinyPosition = transform.position - new Vector3(0, 1f, 0);
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
                moveSideToSide();
                break;
            case 1:
                kamikaze(); 
                break;
            case 2:
                moveZigzag();
                break;
            case 3:
                moveShortSTS();
                break;
            case 4:
                moveUpDown();
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        Debug.Log("Colision");
        if (collision.gameObject.tag == "HorizontalBoundary" || collision.gameObject.tag == "VerticalBoundary")
        {
            switch(movePattern){
                case 0:
                    sideTiSideLimit(collision);
                    break;
                case 2:
                    zigzagLimit(collision);
                    break;
                case 3:
                    shortSTSLimit(collision);
                    break;
                case 4:
                    upDownLimit(collision);
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
    void moveSideToSide(){
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
    void sideTiSideLimit(Collision2D collision){

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

    /*
        Movement pattern #1
        Moves the enemy directly towards the player
    */
    void kamikaze()
    {
        GameObject playerInstance = GameObject.Find("playerInstance");
        transform.position = Vector3.MoveTowards(transform.position, playerInstance.transform.position, moveSpeed * Time.deltaTime);
    }

    /*
        Movement pattern #2
        Moves the enemy diagonally
    */
    void moveZigzag(){
        if (transform.position == destinyPosition)
        {
            movementDistanceY *= -1;
            destinyPosition = transform.position - new Vector3( movementDistanceX, movementDistanceY, 0);
        }
        transform.position = Vector3.MoveTowards(transform.position, destinyPosition, moveSpeed * Time.deltaTime);
    }

    void zigzagLimit(Collision2D collision){

        if (collision.gameObject.tag == "VerticalBoundary")
        {
            Vector3 newPos = transform.position;
            newPos.x -= 3.8f;
            newPos.y -= 0.8f;
            transform.position = newPos;
            destinyPosition = transform.position - new Vector3(movementDistanceX, movementDistanceY, 0);
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


    private void moveShortSTS(){

        if (transform.position == destinyPosition)
        {
            destinyPosition = transform.position - new Vector3( movementDistanceX, 0, 0);
        }
        transform.position = Vector3.MoveTowards(transform.position, destinyPosition, moveSpeed * Time.deltaTime);
    }

    private void shortSTSLimit(Collision2D collision){

        if (collision.gameObject.tag == "VerticalBoundary")
        {
            movementDistanceX *= -1;
            destinyPosition = transform.position - new Vector3(0, movementDistanceY, 0);
        }
        else if (collision.gameObject.tag == "HorizontalBoundary")
        {
            Vector3 newPos = transform.position;
            newPos.y += 2f;
            transform.position = newPos;
            destinyPosition = transform.position - new Vector3(0, movementDistanceY, 0);
        }
    }

    private void moveUpDown(){

        if (transform.position == destinyPosition)
        {
            if (moveLaterally)
            {
                destinyPosition = transform.position - new Vector3( movementDistanceX, 0, 0);
                moveLaterally = false;
            }
            else{
                movementDistanceY *= -1;
                destinyPosition = transform.position - new Vector3( 0, movementDistanceY, 0);
                moveLaterally = true;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, destinyPosition, moveSpeed * Time.deltaTime);
    }


    void upDownLimit(Collision2D collision){

        if (collision.gameObject.tag == "VerticalBoundary")
        {
            Vector3 newPos = transform.position;
            newPos.x -= 3.8f;
            newPos.y -= 0.8f;
            transform.position = newPos;
            destinyPosition = transform.position - new Vector3(movementDistanceX, 0, 0);
        }
        else if (collision.gameObject.tag == "HorizontalBoundary")
        {
            Vector3 newPos = transform.position;
            newPos.x += 0.5f;
            newPos.y += 2f;
            transform.position = newPos;
            if (movementDistanceY < 0)
            {
                movementDistanceY *= -1;
            }
            destinyPosition = transform.position - new Vector3(0, movementDistanceY, 0);
        }
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
