using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using audio_manager;

public class Bonus : MonoBehaviour
{
    void Start()
    {

    }

    void Update(){

        move();
    }

    private void move(){
        Vector3 newPos = transform.position;
        newPos.y -= 0.1f * Time.deltaTime;
        transform.position = newPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "HorizontalBoundary")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            AudioManager.getInstance().playBonusEffect();
            AddBonus();
            Destroy(gameObject);
        }
    }

    void AddBonus(){

        int randBonus = Random.Range(0, 5);
        if (randBonus == 0)
        {
            PlayerController playerScript = GameObject.Find("playerInstance").GetComponent<PlayerController>();
            playerScript.ActivateChaser();
        }
        else if (randBonus == 1)
        {
            PlayerController playerScript = GameObject.Find("playerInstance").GetComponent<PlayerController>();
            playerScript.ActivateExpansive();
        }
        else if (randBonus == 2)
        {
            PlayerController playerScript = GameObject.Find("playerInstance").GetComponent<PlayerController>();
            playerScript.ActivateShield();
        }
        else if (randBonus == 3)
        {
            PlayerController playerScript = GameObject.Find("playerInstance").GetComponent<PlayerController>();
            playerScript.ActivateX2Pts();
        }
        else if (randBonus == 4)
        {
            PlayerController playerScript = GameObject.Find("playerInstance").GetComponent<PlayerController>();
            playerScript.increaseLifes();
        }
        
    }
}
