using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

using GameManager;
using UserManager;
using audio_manager;

public class GameSceneScript : MonoBehaviour
{
    [SerializeField] GameObject pnlPauseDialogue;
    [SerializeField] Button btnOption1;
    [SerializeField] Button btnOption2;
    [SerializeField] Button btnOption3;
    [SerializeField] Button btnClosePauseDialogue;

    [SerializeField] GameObject playerPos1;
    [SerializeField] GameObject playerPos2;

    [SerializeField] UnityEngine.UI.Image imgUser1;
    [SerializeField] UnityEngine.UI.Image imgUser2;

    [SerializeField] UnityEngine.UI.Image imgControl1;
    [SerializeField] UnityEngine.UI.Image imgControl2;

    [SerializeField] TMPro.TextMeshProUGUI txtLevel;

    [SerializeField] TMPro.TextMeshProUGUI txtUsername1;
    [SerializeField] TMPro.TextMeshProUGUI txtUsername2;

    [SerializeField] TMPro.TextMeshProUGUI txtScore1;
    [SerializeField] TMPro.TextMeshProUGUI txtScore2;

    [SerializeField] GameObject bonus;
    [SerializeField] int score = 0;
    [SerializeField] int level = 1;
    [SerializeField] float lifes = 3f;

    public float x2PtsDuration = 15f;
    public bool x2PtsIsActive = false;

    private User user1;
    private User user2;
    private PlayerContext playerContext1;
    private PlayerContext playerContext2;

    // Start is called before the first frame update
    void Start()
    {
        // Remove this later
        //setAttackPatternForLevel(int level, int attackPattern)
        gameManager.getInstance().setAttackPatternForLevel(1, 1);
        gameManager.getInstance().setAttackPatternForLevel(2, 2);
        gameManager.getInstance().setAttackPatternForLevel(3, 3);

        Debug.Log("Level1: " + gameManager.getInstance().getAttackPatternForLevel(1));
        Debug.Log("Level2: " + gameManager.getInstance().getAttackPatternForLevel(2));
        Debug.Log("Level3: " + gameManager.getInstance().getAttackPatternForLevel(3));
        //

        AudioManager.getInstance().playLevel1Soundtrack();
        AudioManager.getInstance().pauseMusicSource.Play();
        AudioManager.getInstance().pauseMusicSource.Pause();

        if (gameManager.getInstance().cuantityOfPlayers == 1)
        {
            User user = userManager.getInstance().getUserByUsername(gameManager.getInstance().player1Username);
            gameManager.getInstance().setCurrentPlayer(user);
            imgControl1.gameObject.SetActive(true);
            txtUsername1.text = user.username;

            // Load player image from specified path
            if (!string.IsNullOrEmpty(user.userImage) && File.Exists(Application.dataPath + user.userImage))
            {
                byte[] imageData = File.ReadAllBytes(Application.dataPath + user.userImage);
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(imageData);
                imgUser1.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }
        else if (gameManager.getInstance().cuantityOfPlayers == 2 && !gameManager.getInstance().getOneInsteadOfTwo())
        {
            user1 = userManager.getInstance().getUserByUsername(gameManager.getInstance().player1Username);
            user2 = userManager.getInstance().getUserByUsername(gameManager.getInstance().player2Username);

            playerContext1 = new PlayerContext(PlayerState.Playing, user1.username, score, level, user1.ship, lifes);
            playerContext2 = new PlayerContext(PlayerState.Waiting, user2.username, score, level, user2.ship, lifes);
            playerContext1.saveInitPlayerState();
            playerContext2.saveInitPlayerState();

            if (user1.username == gameManager.getInstance().playerToPlay)
            {
                gameManager.getInstance().setCurrentPlayer(user1);
                imgControl1.gameObject.SetActive(true);
            }
            else
            {
                gameManager.getInstance().setCurrentPlayer(user2);
                imgControl2.gameObject.SetActive(true);
            }

            txtUsername1.text = user1.username;
            txtUsername2.text = user2.username;

            // Load player image from specified path
            if (!string.IsNullOrEmpty(user1.userImage) && File.Exists(Application.dataPath + user1.userImage))
            {
                byte[] imageData1 = File.ReadAllBytes(Application.dataPath + user1.userImage);
                Texture2D texture1 = new Texture2D(2, 2);
                texture1.LoadImage(imageData1);
                imgUser1.sprite = Sprite.Create(texture1, new Rect(0, 0, texture1.width, texture1.height), new Vector2(0.5f, 0.5f));
            }

            if (!string.IsNullOrEmpty(user2.userImage) && File.Exists(Application.dataPath + user2.userImage))
            {
                byte[] imageData2 = File.ReadAllBytes(Application.dataPath + user2.userImage);
                Texture2D texture2 = new Texture2D(2, 2);
                texture2.LoadImage(imageData2);
                imgUser2.sprite = Sprite.Create(texture2, new Rect(0, 0, texture2.width, texture2.height), new Vector2(0.5f, 0.5f));
            }

            ShowPlayerPos2();
        }
        else
        {
            User user = userManager.getInstance().getUserByUsername(gameManager.getInstance().playerToPlay);
            gameManager.getInstance().setCurrentPlayer(user);
            imgControl1.gameObject.SetActive(true);
            txtUsername1.text = user.username;

            // Load player image from specified path
            if (!string.IsNullOrEmpty(user.userImage) && File.Exists(Application.dataPath + user.userImage))
            {
                byte[] imageData = File.ReadAllBytes(Application.dataPath + user.userImage);
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(imageData);
                imgUser1.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }

        //generateBonus();
    }

    // Update is called once per frame
    void Update()
    {
        pauseMenu();
    }

    private void pauseMenu()
    {
        // Detect if the "P" key is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!pnlPauseDialogue.activeSelf)
            {
                GameObject.Find("MainCamera").GetComponent<Spawner>().pauseSpawner();

                // Activate the pause panel
                pnlPauseDialogue.SetActive(true);

                // Pause game time and music
                AudioManager.getInstance().pauseSoundtrack();
                AudioManager.getInstance().pauseMusicSource.volume = AudioManager.getInstance().audioVolumeBeforePause;
                AudioManager.getInstance().pauseMusicSource.UnPause();
                if (AudioManager.getInstance().isAudioPaused == true)
                {
                    Time.timeScale = 0f;
                }
            }
        }
    }

    public void onCollision(float playerLifes)
    {
        lifes = playerLifes;
        if (gameManager.getInstance().cuantityOfPlayers == 2 && !gameManager.getInstance().getOneInsteadOfTwo())
        {
            if (!pnlPauseDialogue.activeSelf)
            {
                // Start the coroutine to wait and activate the panel
                StartCoroutine(pauseOnCollision(1.0f));
            }
        }
        else
        {
            // Start the coroutine to wait and restart player and enemy spawn
            StartCoroutine(restartOnCollision(1.5f));
        }
    }

    private IEnumerator pauseOnCollision(float time)
    {
        yield return new WaitForSeconds(time);

        GameObject.Find("MainCamera").GetComponent<Spawner>().pauseSpawner();

        // Activate the pause panel
        pnlPauseDialogue.SetActive(true);

        // Pause game time and music
        AudioManager.getInstance().pauseSoundtrack();
        AudioManager.getInstance().pauseMusicSource.volume = AudioManager.getInstance().audioVolumeBeforePause;
        AudioManager.getInstance().pauseMusicSource.UnPause();
        if (AudioManager.getInstance().isAudioPaused == true)
        {
            Time.timeScale = 0f;
        }

        if (gameManager.getInstance().getCurrentPlayer().username == playerContext1.getPlayer())
        {
            playerContext1.savePlayerState(score, level, lifes);
            playerContext2.restorePlayerState();

            gameManager.getInstance().playerToPlay = user2.username;
            gameManager.getInstance().setCurrentPlayer(user2);

            imgControl1.gameObject.SetActive(false);
            imgControl2.gameObject.SetActive(true);

            score = playerContext2.getScore();
            level = playerContext2.getLevel();
            lifes = playerContext2.getLifes();
        }
        else
        {
            playerContext2.savePlayerState(score, level, lifes);
            playerContext1.restorePlayerState();

            gameManager.getInstance().playerToPlay = user1.username;
            gameManager.getInstance().setCurrentPlayer(user1);

            imgControl1.gameObject.SetActive(true);
            imgControl2.gameObject.SetActive(false);

            score = playerContext1.getScore();
            level = playerContext1.getLevel();
            lifes = playerContext1.getLifes();
        }

        GameObject.Find("MainCamera").GetComponent<Spawner>().updatePlayerShip();
        GameObject.Find("MainCamera").GetComponent<Spawner>().destroyAllEnemies();
        GameObject.Find("MainCamera").GetComponent<Spawner>().spawnEnemies();
        GameObject.Find("playerInstance").GetComponent<PlayerController>().setLifes((int)lifes);
    }

    private IEnumerator restartOnCollision(float time)
    {
        yield return new WaitForSeconds(time);

        GameObject.Find("MainCamera").GetComponent<Spawner>().destroyAllEnemies();
        GameObject.Find("MainCamera").GetComponent<Spawner>().spawnPlayer();
        GameObject.Find("MainCamera").GetComponent<Spawner>().spawnEnemies();
        GameObject.Find("playerInstance").GetComponent<PlayerController>().setLifes((int)lifes);
    }

    public void option1ButtonOnClick()
    {

    }

    public void option3ButtonOnClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void closePauseDialogueButtonOnClick()
    {
        if (pnlPauseDialogue.activeSelf)
        {
            GameObject.Find("MainCamera").GetComponent<Spawner>().unPauseSpawner();

            // Deactivate the pause panel
            pnlPauseDialogue.SetActive(false);

            // Resume game and music
            Time.timeScale = 1f;
            if (Time.timeScale == 1f)
            {
                AudioManager.getInstance().pauseMusicSource.Pause();
                AudioManager.getInstance().unPauseSoundtrack();
            }
        }
    }

    public void ShowPlayerPos2()
    {
        playerPos2.SetActive(true);
    }

    public void HidePlayerPos2()
    {
        playerPos2.SetActive(false);
    }

    public void showPnlPauseDialogue()
    {
        pnlPauseDialogue.SetActive(true);
    }

    public void hidePnlPauseDialogue()
    {
        pnlPauseDialogue.SetActive(false);
    }

    public void activateX2Pts()
    {
        x2PtsIsActive = true;
        Invoke("desactivateX2Pts", x2PtsDuration);
    }

    public void desactivateX2Pts()
    {
        x2PtsIsActive = false;
    }

    public void updateScore(int points)
    {

        if (x2PtsIsActive)
        {
            points *= 2;
        }
        score += points;

        if (gameManager.getInstance().cuantityOfPlayers == 2 && !gameManager.getInstance().getOneInsteadOfTwo())
        {
            if (gameManager.getInstance().getCurrentPlayer().username == playerContext1.getPlayer())
            {
                txtScore1.text = score.ToString();
            }
            else
            {
                txtScore2.text = score.ToString();
            }
        }
        else
        {
            txtScore1.text = score.ToString();
        }
    }

    // Generates a bonus in the game
    public void generateBonus()
    {
        Debug.Log("Something went wrong loading player information");

        Instantiate(bonus, transform.position, Quaternion.identity);
    }

    public void levelCompleted()
    {
        if (level < 3)
        {
            level++;
        }
        Debug.Log("Level Completed");
    }

    public int getLevel()
    {
        return level;
    }
}
