using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] GameObject playerNairanFighter;
    [SerializeField] GameObject playerNairanScout;
    [SerializeField] GameObject playerKlaedFighter;
    [SerializeField] GameObject playerKlaedScout;
    [SerializeField] GameObject playerNautolanBomber;
    [SerializeField] GameObject playerNautolanScout;
    [SerializeField] GameObject playerMarauder;
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

        Instantiate(playerMarauder, playerSpawn.position, Quaternion.identity);
    }
}
