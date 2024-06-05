using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

using GameManager;
using UserManager;
using audio_manager;

public class podiumSceneScript : MonoBehaviour
{
    [SerializeField] GameObject pnlFirstPlace;
    [SerializeField] GameObject pnlSecondPlace;
    [SerializeField] GameObject pnlSinglePlayer;

    [SerializeField] UnityEngine.UI.Image imgUser;
    [SerializeField] UnityEngine.UI.Image imgUser1;
    [SerializeField] UnityEngine.UI.Image imgUser2;

    [SerializeField] TMPro.TextMeshProUGUI txtTitle;
    [SerializeField] TMPro.TextMeshProUGUI txtUsername;
    [SerializeField] TMPro.TextMeshProUGUI txtUsername1;
    [SerializeField] TMPro.TextMeshProUGUI txtUsername2;

    [SerializeField] TMPro.TextMeshProUGUI txtScore;
    [SerializeField] TMPro.TextMeshProUGUI txtScore1;
    [SerializeField] TMPro.TextMeshProUGUI txtScore2;

    private User user1;
    private User user2;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.getInstance().playPodiumSoundtrack();

        string title = gameManager.getInstance().getPodiumSceneTitle();
        if (title == "Game Over" || title == "Game Completed")
        {
            singlePlayer(title);
        }
        else
        {
            multiPlayer(title);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void singlePlayer(string title)
    {
        txtTitle.text = title;

        User user = gameManager.getInstance().getCurrentPlayer();
        txtUsername.text = user.username;
        txtScore.text = "Score: " + gameManager.getInstance().getScore1().ToString();

        // Load player image from specified path
        if (!string.IsNullOrEmpty(user.userImage) && File.Exists(Application.dataPath + user.userImage))
        {
            byte[] imageData = File.ReadAllBytes(Application.dataPath + user.userImage);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageData);
            imgUser.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }

        pnlFirstPlace.SetActive(false);
        pnlSecondPlace.SetActive(false);
        pnlSinglePlayer.SetActive(true);
    }

    public void multiPlayer(string title)
    {
        txtTitle.text = title;

        if (gameManager.getInstance().playerToPlay == gameManager.getInstance().player1Username)
        {
            Debug.Log("A");
            user1 = userManager.getInstance().getUserByUsername(gameManager.getInstance().player1Username);
            user2 = userManager.getInstance().getUserByUsername(gameManager.getInstance().player2Username);
        }
        else
        {
            Debug.Log("B");
            user1 = userManager.getInstance().getUserByUsername(gameManager.getInstance().player2Username);
            user2 = userManager.getInstance().getUserByUsername(gameManager.getInstance().player1Username);
        }

        txtUsername1.text = user1.username;
        txtUsername2.text = user2.username;

        txtScore1.text = "Score: " + gameManager.getInstance().getScore1().ToString();
        txtScore2.text = "Score: " + gameManager.getInstance().getScore2().ToString();

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

        pnlFirstPlace.SetActive(true);
        pnlSecondPlace.SetActive(true);
        pnlSinglePlayer.SetActive(false);
    }

    public void hallOfFameButtonOnClick()
    {
        SceneManager.LoadScene("HallOfFameScene");
    }

    public void playAgainButtonOnClick()
    {
        
    }

    public void mainMenuButtonOnClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
