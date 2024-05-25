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
    private User user;
    private int ship;

    // Start is called before the first frame update
    void Start()
    {
        user = gameManager.getInstance().getCurrentPlayer();
        ship = user.ship;
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

        if (user.username != gameManager.getInstance().getCurrentPlayer().username)
        {
            user = gameManager.getInstance().getCurrentPlayer();
            ship = user.ship;
            spawnPlayer();
        }
    }

    // Generates the player on screen, fixing the z value to 0 to show player on game screen.
    void spawnPlayer(){
        // Delete the playerInstance if it already exists
        if (playerInstance != null)
        {
            Destroy(playerInstance);
        }

        Vector3 newPos = playerSpawn.position;
        newPos.z = 0f;
        playerSpawn.position = newPos;

        var playerShip = playerMarauder;
        switch (ship)
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
