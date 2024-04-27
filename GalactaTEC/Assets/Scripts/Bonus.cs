using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public AudioSource source;
    public AudioClip audioClip;
    public float volume=0.5f;

    void Start()
    {
        int randNum = Random.Range(0, 2);
        if (randNum == 1)
        {
            Vector3 pos = transform.position;
            int randX = Random.Range(300, 1600);
            pos.x = (float)randX;
            transform.position = pos;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update(){

        Vector3 pos = transform.position;
        if (pos.y < 100)
        {
            Destroy(gameObject);
        }
    }

    void OnCollision(){
        Debug.Log("Collision");
    }

    void OnTriggerEnter2D(){

        source.PlayOneShot(audioClip, volume);
        Debug.Log("Trigger");
        AddBonus();
        Destroy(gameObject);
    }

    void AddBonus(){

        int randBonus = Random.Range(0, 5);
        if (randBonus == 0)
        {
            PlayerController playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
            playerScript.ActivateChaser();
        }
        else if (randBonus == 1)
        {
            PlayerController playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
            playerScript.ActivateExpansive();
        }
        else if (randBonus == 2)
        {
            PlayerController playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
            playerScript.ActivateShield();
        }
        else if (randBonus == 3)
        {
            PlayerController playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
            playerScript.ActivateX2Pts();
        }
        else if (randBonus == 4)
        {
            Debug.Log("Give another Life");
        }
        
    }
}
