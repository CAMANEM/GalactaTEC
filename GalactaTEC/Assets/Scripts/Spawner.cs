using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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

    [SerializeField] GameObject enemyKlaedBattlecruiser;
    [SerializeField] GameObject enemyKlaedDreadnought;
    [SerializeField] GameObject enemyNairanBattlecruiser;
    [SerializeField] GameObject enemyNairanDreadnought;
    [SerializeField] GameObject enemyNautolanBattlecruiser;
    [SerializeField] GameObject enemyNautolanDreadnought;


    public Transform EnemySpawn;
    private string[] enemies = new string[21];
    private int[] enemyTypes = new int[3];
    private int enemyShooting = 0;

    // Start is called before the first frame update
    void Start()
    {
        user = gameManager.getInstance().getCurrentPlayer();
        ship = user.ship;
        randomizeLevelEnemies();
        spawnPlayer();
        spawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Generates the player on screen, fixing the z value to 0 to show player on game screen.
    public void spawnPlayer()
    {
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

    public void spawnEnemies()
    {

        Vector3 enemyPos = EnemySpawn.position;
        enemyPos.z = 0f;
        // Gets the current level
        int enemyType = GameObject.Find("Canvas").GetComponent<GameSceneScript>().getLevel();
        // Gets the enemyType assigned to this level
        enemyType = enemyTypes[enemyType];
        Debug.Log(enemyType);
        var enemyShip = enemyKlaedBattlecruiser;

        switch (enemyType)
        {
            case 0:
                enemyShip = enemyKlaedBattlecruiser;
                break;
            case 1:
                enemyShip = enemyKlaedDreadnought;
                break;
            case 2:
                enemyShip = enemyNairanBattlecruiser;
                break;
            case 3:
                enemyShip = enemyNairanDreadnought;
                break;
            case 4:
                enemyShip = enemyNautolanBattlecruiser;
                break;
            case 5:
                enemyShip = enemyNautolanDreadnought;
                break;
        }
        int enemyIndex = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                GameObject newEnemy = Instantiate(enemyShip, enemyPos, Quaternion.identity);
                newEnemy.name = "enemy" + enemyIndex.ToString();
                enemies[enemyIndex] = newEnemy.name;
                enemyIndex++;
                enemyPos.x += 0.2f;
            }
            enemyPos.x -= 1.4f;
            enemyPos.y -= 0.25f;
        }

        InvokeRepeating(nameof(enemyShoot), 3, 2.5f);
    }

    // Method to destroy all enemies
    public void destroyAllEnemies()
    {
        CancelInvoke();
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                GameObject enemy = GameObject.Find(enemies[i]);
                if (enemy != null)
                {
                    Destroy(enemy);
                }
            }
        }

        enemies = new string[21];
        destroyShots();
    }

    // Calls a validation for the enemy with the shooting turn and select if the enemy shoul shoot a normal or charged shot
    private void enemyShoot()
    {

        if (validateEnemy())
        {
            Enemy enemyScript = GameObject.Find(enemies[enemyShooting]).GetComponent<Enemy>();
            enemyScript.shoot();
            enemyShooting++;
        }
    }

    /* Validates that the enemy with the shooting turn exists
        if not, reboots the shooting 
        if there is no more enemies, ends the shooting cycle
    */
    private bool validateEnemy()
    {

        if (enemies.Length == 0)
        {
            CancelInvoke();
            return false;
        }
        else if (enemies.Length <= enemyShooting)
        {
            enemyShooting = 0;
            return true;
        }
        else
        {
            return true;
        }
    }

    public void enemyDestroyed(string enemyName)
    {

        enemies = enemies.Where(val => val != enemyName).ToArray();
        if (enemies.Length == 0)
        {
            levelCompleted();
        }
    }

    public void levelCompleted()
    {
        GameObject.Find("Canvas").GetComponent<GameSceneScript>().levelCompleted();
    }

    public void updatePlayerShip()
    {
        if (user.username != gameManager.getInstance().getCurrentPlayer().username)
        {
            user = gameManager.getInstance().getCurrentPlayer();
            ship = user.ship;
            spawnPlayer();
        }
    }

    public void pauseSpawner()
    {
        Time.timeScale = 0f;
    }

    public void unPauseSpawner()
    {
        Time.timeScale = 1f;
    }

    private void randomizeLevelEnemies()
    {
        int min = 0;
        int max = 2;
        for (int i = 0; i < 3; i++)
        {
            int randEnemy = Random.Range(min, max);
            enemyTypes[i] = randEnemy;
            min += 2;
            max += 2;
        }
    }

    // destroy all types of shots on the game
    private void destroyShots(){
        var shots = GameObject.FindGameObjectsWithTag ("EnemyShot");
        foreach (var shot in shots){
            Destroy(shot);
        }
        shots = GameObject.FindGameObjectsWithTag ("PlayerShot");
        foreach (var shot in shots){
            Destroy(shot);
        }
        shots = GameObject.FindGameObjectsWithTag ("ChargedShot");
        foreach (var shot in shots){
            Destroy(shot);
        }
    }

}
