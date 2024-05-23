using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float moveSpeed = 0.2f;
    public float movementDistanceX = 0.5f;
    public float movementDistanceY = 0.5f;

    public byte movePattern = 0;
    private Vector3 destinyPosition;

    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private GameObject enemyShot;
    [SerializeField]
    private GameObject explosion;


    // Start is called before the first frame update
    void Start()
    {
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
            movementDistanceX *= -1;
            destinyPosition = transform.position - new Vector3(0, movementDistanceY, 0);
        }
        else if (collision.gameObject.tag == "HorizontalBoundary")
        {
            movementDistanceY *= -1;
            destinyPosition = transform.position - new Vector3(movementDistanceX, 0, 0);
        }
        
    }

    private void destroy()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
