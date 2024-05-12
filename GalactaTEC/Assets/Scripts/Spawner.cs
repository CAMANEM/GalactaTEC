using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] GameObject playerNairanFighter;
    public Transform playerSpawn;


    // Start is called before the first frame update
    void Start()
    {
        spawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
        Generates the player on screen, fixing the z value to 0 to show player on game screen.
    */
    void spawnPlayer(){
        Vector3 newPos = playerSpawn.position;
        newPos.z = 0f;
        playerSpawn.position = newPos;
        Debug.Log(newPos);
        Instantiate(playerNairanFighter, playerSpawn.position, Quaternion.identity);
    }
}
