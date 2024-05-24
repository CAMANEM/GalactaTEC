using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameManager;
using UserManager;

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

    private GameObject playerInstance;

    // Start is called before the first frame update
    void Start()
    {
        spawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        // Hide or show player's ship based on game pause state
        if (gameManager.getInstance().getGameIsPaused() && playerInstance != null)
        {
            playerInstance.SetActive(false); // Hide player ship
        }
        else if (!gameManager.getInstance().getGameIsPaused() && playerInstance != null)
        {
            playerInstance.SetActive(true); // Show player ship
        }
    }

    // Generates the player on screen, fixing the z value to 0 to show player on game screen.
    void spawnPlayer(){
        Vector3 newPos = playerSpawn.position;
        newPos.z = 0f;
        playerSpawn.position = newPos;

        User user = gameManager.getInstance().getCurrentPlayer();
        var playerShip = playerMarauder;
        switch (user.ship)
        {
            case 0:
                playerShip = playerMarauder;
                break;
            case 1:
                playerShip = playerKlaedFighter;
                break;
            case 2:
                playerShip = playerKlaedScout;
                break;
            case 3:
                playerShip = playerNairanFighter;
                break;
            case 4:
                playerShip = playerNairanScout;
                break;
            case 5:
                playerShip = playerNautolanBomber;
                break;
            case 6:
                playerShip = playerNautolanScout;
                break;
            default:
                playerShip = playerMarauder;
                break;
        }

        playerInstance = Instantiate(playerShip, playerSpawn.position, Quaternion.identity);
        playerInstance.name = "playerInstance";
    }
}
