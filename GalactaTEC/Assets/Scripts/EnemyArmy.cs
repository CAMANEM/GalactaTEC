using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArmy : MonoBehaviour
{
    
    public float moveSpeed = 0.2f;
    public float movementDistanceX = -0.5f;
    public float movementDistanceY = 0.5f;
    private bool moveLaterally = true;

    public int movePattern = 1;
    private Vector3 destinyPosition;

    // Start is called before the first frame update
    void Start()
    {
        restart();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move(){
        switch(movePattern){
            case 1:
                moveSideToSide();
                break;
            case 2:
                moveShortSTS(); 
                break;
            case 3:
                moveZigzag();
                break;
            case 4:
                moveUpDown();
                break;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision){
        Debug.Log("Colision");
        Debug.Log(movePattern);
        if (collision.gameObject.tag == "HorizontalBoundary" || collision.gameObject.tag == "VerticalBoundary")
        {
            switch(movePattern){
                case 1:
                    sideTiSideLimit(collision);
                    break;
                case 2:
                    shortSTSLimit(collision);
                    break;
                case 3:
                    zigzagLimit(collision);
                    break;
                case 4:
                    upDownLimit(collision);
                    break;
            }
        }
    }

    
    /*
        Movement pattern #1
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
        Movement pattern #1
        Controls the movement when the enemy collides with a screen border
    */
    void sideTiSideLimit(Collision2D collision){

        if (collision.gameObject.tag == "VerticalBoundary")
        {
            Vector3 newPos = transform.position;
            newPos.x -= 1.7f;
            newPos.y -= 0.1f;
            transform.position = newPos;
            destinyPosition = transform.position - new Vector3(movementDistanceX, 0, 0);
        }
        else if (collision.gameObject.tag == "HorizontalBoundary")
        {
            gameObject.transform.position = Vector3.zero;
            destinyPosition = transform.position - new Vector3(0, 1f, 0);
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
            Vector3 newPos = transform.position;
            newPos.y -= 0.1f;
            transform.position = newPos;
            destinyPosition = transform.position - new Vector3(movementDistanceX, 0, 0);
        }
        else if (collision.gameObject.tag == "HorizontalBoundary")
        {
            gameObject.transform.position = Vector3.zero;
            destinyPosition = transform.position - new Vector3(0, 1f, 0);
        }
    }

    /*
        Movement pattern #3
        Moves the enemy diagonally
    */
    void moveZigzag(){
        if (transform.position == destinyPosition)
        {
            destinyPosition = transform.position - new Vector3( movementDistanceX, movementDistanceY, 0);
            movementDistanceY *= -1;
        }
        transform.position = Vector3.MoveTowards(transform.position, destinyPosition, moveSpeed * Time.deltaTime);
    }

    void zigzagLimit(Collision2D collision){

        if (collision.gameObject.tag == "VerticalBoundary")
        {
            Vector3 newPos = transform.position;
            newPos.x -= 1.7f;
            newPos.y -= 0.1f;
            transform.position = newPos;
            if (movementDistanceY > 0 )
            {
                movementDistanceY *= -1;
            }
            destinyPosition = transform.position - new Vector3(movementDistanceX, movementDistanceY, 0);
        }
        else if (collision.gameObject.tag == "HorizontalBoundary")
        {
            gameObject.transform.position = Vector3.zero;
            destinyPosition = transform.position - new Vector3(0, 1f, 0);
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
                destinyPosition = transform.position - new Vector3( 0, movementDistanceY, 0);
                movementDistanceY *= -1;
                moveLaterally = true;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, destinyPosition, moveSpeed * Time.deltaTime);
    }


    public void upDownLimit(Collision2D collision){
        if (collision.gameObject.tag == "VerticalBoundary")
        {
            Vector3 newPos = transform.position;
            newPos.x -= 1.7f;
            newPos.y -= 0.1f;
            transform.position = newPos;
            if (movementDistanceY < 0)
            {
                movementDistanceY *= -1;
            }
            destinyPosition = transform.position - new Vector3(movementDistanceX, 0, 0);
        }
        else if (collision.gameObject.tag == "HorizontalBoundary")
        {
            gameObject.transform.position = Vector3.zero;
            destinyPosition = transform.position - new Vector3(0, 1f, 0);
            if (movementDistanceY < 0)
            {
                movementDistanceY *= -1;
            }
        }
    }

    private void configMovePattern(){
        movePattern = GameObject.Find("Canvas").GetComponent<GameSceneScript>().getMovementPattern();
        //movePattern = 4;
    }

    // resets all its position and initial movement  values
    public void restart(){
        gameObject.transform.position = Vector3.zero;
        moveSpeed = 0.2f;
        movementDistanceX = -0.5f;
        movementDistanceY = 0.5f;
        moveLaterally = true;
        configMovePattern();
        destinyPosition = transform.position - new Vector3(0, 1f, 0);
        //destinyPosition = transform.position - new Vector3(-1f, 0, 0);

    }
}
