using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager;
using System;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

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

    [SerializeField] TMPro.TextMeshProUGUI txtUsername1;
    [SerializeField] TMPro.TextMeshProUGUI txtUsername2;

    [SerializeField] TMPro.TextMeshProUGUI txtScore1;
    [SerializeField] TMPro.TextMeshProUGUI txtScore2;

    [SerializeField] int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.getInstance().playLevel1Soundtrack();

        if (gameManager.getInstance().cuantityOfPlayers == 1)
        {
            User user = getUserByUsername(gameManager.getInstance().player1Username);
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
        else if (gameManager.getInstance().cuantityOfPlayers == 2 && gameManager.getInstance().playerToPlay == "")
        {
            User user1 = getUserByUsername(gameManager.getInstance().player1Username);
            User user2 = getUserByUsername(gameManager.getInstance().player2Username);

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
            User user = getUserByUsername(gameManager.getInstance().playerToPlay);
            txtUsername1.text = user.username;
            //User user = getUserByUsername("joseandres216");
            //txtUsername1.text = user.username;

            // Load player image from specified path
            if (!string.IsNullOrEmpty(user.userImage) && File.Exists(Application.dataPath + user.userImage))
            {
                byte[] imageData = File.ReadAllBytes(Application.dataPath + user.userImage);
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(imageData);
                imgUser1.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }
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
                // Activate the pause panel
                pnlPauseDialogue.SetActive(true);

                // Pause game time and music
                Time.timeScale = 0f;
                AudioManager.getInstance().isAudioPaused = true;
            }
        }
    }

    public void option1ButtonOnClick()
    {
        
    }

    public void option2ButtonOnClick()
    {
        PlayerPrefs.SetString("HelpScene", "GameScene");
        PlayerPrefs.Save();
        SceneManager.LoadScene("HelpScene");
    }

    public void option3ButtonOnClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void closePauseDialogueButtonOnClick()
    {
        if (pnlPauseDialogue.activeSelf)
        {
            // Deactivate the pause panel
            pnlPauseDialogue.SetActive(false);

            // Resume game and music
            Time.timeScale = 1f;
            AudioManager.getInstance().isAudioPaused = false;
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

    private User getUserByEmail(string email)
    {
        string usersJSON = File.ReadAllText(gameManager.getInstance().usersPath);

        Users users = JsonUtility.FromJson<Users>(usersJSON);

        User foundUser = null;

        foreach (User user in users.users)
        {
            if (user.email == email)
            {
                foundUser = user;
            }
            else
            {
                Debug.Log("Something went wrong loading player information");
            }
        }

        return foundUser;
    }

    private User getUserByUsername(string username)
    {
        string usersJSON = File.ReadAllText(gameManager.getInstance().usersPath);

        Users users = JsonUtility.FromJson<Users>(usersJSON);

        User foundUser = null;

        foreach (User user in users.users)
        {
            if (user.username == username)
            {
                foundUser = user;
            }
            else
            {
                Debug.Log("Something went wrong loading player information");
            }
        }

        return foundUser;
    }


    public void updateScore(int points){

        score += points;
        txtScore1.text = score.ToString();
    }
}
