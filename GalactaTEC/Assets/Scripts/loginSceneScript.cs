using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine.SceneManagement;


using GameManager;
using alerts_manager;
using audio_manager;

public class loginSceneScript : MonoBehaviour
{
    public TMP_InputField inpUsernameEmailP1;
    public TMP_InputField inpPasswordP1;
    public TMP_InputField inpUsernameEmailP2;
    public TMP_InputField inpPasswordP2;

    public Button btnLoginP1;
    public Button btnLoginP2;
    public Button btnLogoutP1;
    public Button btnLogoutP2;

    public Button btnShowPasswordP1;
    public Button btnHidePasswordP1;
    public Button btnShowPasswordP2;
    public Button btnHidePasswordP2;

    public TMP_Text txtUserP1;
    public TMP_Text txtUserP2;

    string usersPath = Application.dataPath + "/Data/users.json";

    private bool player1isLoggedIn = false;
    private bool player2isLoggedIn = false;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.getInstance().playBackgroundSoundtrack();
    }

    // Update is called once per frame
    void Update()
    {
        
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
                Debug.Log("Something went wrong loading player information. Debug code: 0.");
            }
        }

        return foundUser;
    }

    public void signUpButtonOnClick()
    {
        SceneManager.LoadScene("SignUpScene");
    }

    private List<User> getSignedUsers()
    {
        string usersJSON = File.ReadAllText(usersPath);

        Users users = JsonUtility.FromJson<Users>(usersJSON);

        return users.users;
    }

    public void loginP1ButtonOnClick()
    {
        List<User> users = getSignedUsers();

        if (this.areDifferentUsers())
        {
            foreach (User user in users)
            {
                if ((user.email == inpUsernameEmailP1.text || user.username == inpUsernameEmailP1.text) && user.password == this.generatePasswordHashKey(inpPasswordP1.text))
                {
                    player1isLoggedIn = true;
                    gameManager.getInstance().player1Email = user.email;
                    gameManager.getInstance().player1Username = user.username;
                    AudioManager.getInstance().loggedUserFavoriteSoundtracks = user.favoriteSoundtracks;
                    AudioManager.getInstance().stopSoundtrack();

                    if ((gameManager.getInstance().cuantityOfPlayers == 1) || (gameManager.getInstance().cuantityOfPlayers == 2 && player2isLoggedIn))
                    {
                        SceneManager.LoadScene("MainMenuScene");
                    }
                    else
                    {
                        this.txtUserP1.fontSize = 40;
                        this.txtUserP1.text = "Player 1 logged in!";
                        
                        this.inpUsernameEmailP1.interactable = false;
                        this.inpPasswordP1.interactable = false;

                        this.btnLoginP1.gameObject.SetActive(false);
                        this.btnLogoutP1.gameObject.SetActive(true);

                        AlertsManager.getInstance().showAlert("Success!", "Player 1 logged in succesfully", "Awesome!");
                    }
                }
            }

            if (!player1isLoggedIn)
            {
                AlertsManager.getInstance().showAlert("Invalid credentials", "Username or password are incorrect. Please try again.", "Retry");
            }
        }
        else
        {
            AlertsManager.getInstance().showAlert("Invalid users", "Users must be different. Please try again.", "Retry");
            player1isLoggedIn = false;
        }
    }

    public void loginP2ButtonOnClick()
    {
        List<User> users = getSignedUsers();

        if (this.areDifferentUsers())
        {
            foreach (User user in users)
            {
                if ((user.email == inpUsernameEmailP2.text || user.username == inpUsernameEmailP2.text) && user.password == this.generatePasswordHashKey(inpPasswordP2.text))
                {
                    player2isLoggedIn = true;

                    gameManager.getInstance().player2Email = user.email;
                    gameManager.getInstance().player2Username = user.username;

                    if (player1isLoggedIn)
                    {
                        SceneManager.LoadScene("MainMenuScene");
                    }
                    else
                    {
                        this.txtUserP2.fontSize = 40;
                        this.txtUserP2.text = "Player 2 logged in!";

                        this.inpUsernameEmailP2.interactable = false;
                        this.inpPasswordP2.interactable = false;

                        this.btnLoginP2.gameObject.SetActive(false);
                        this.btnLogoutP2.gameObject.SetActive(true);

                        AlertsManager.getInstance().showAlert("Success!", "Player 2 logged in succesfully", "Awesome!");
                    }
                }
            }

            if (!player2isLoggedIn)
            {
                AlertsManager.getInstance().showAlert("Invalid credentials", "Username or password are incorrect. Please try again.", "Retry");
            }
        } 
        else
        {
            AlertsManager.getInstance().showAlert("Invalid users", "Users must be different. Please try again.", "Retry");
            player2isLoggedIn = false;
        }
    }

    private string generatePasswordHashKey(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hashedBytes)
            {
                stringBuilder.Append(b.ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }

    public void logoutP1ButtononClick()
    {
        gameManager.getInstance().player1Email = "";
        gameManager.getInstance().player1Username = "";

        this.player1isLoggedIn = false;

        this.txtUserP1.fontSize = 50;
        this.txtUserP1.text = "Player 1";

        this.inpUsernameEmailP1.text = "";
        this.inpPasswordP1.text = "";
        this.inpUsernameEmailP1.interactable = true;
        this.inpPasswordP1.interactable = true;

        this.btnLoginP1.gameObject.SetActive(true);
        this.btnLogoutP1.gameObject.SetActive(false);
    }

    public void logoutP2ButtononClick()
    {
        gameManager.getInstance().player2Email = "";
        gameManager.getInstance().player2Username = "";

        this.player2isLoggedIn = false;

        this.txtUserP2.fontSize = 50;
        this.txtUserP2.text = "Player 2";

        this.inpUsernameEmailP2.text = "";
        this.inpPasswordP2.text = "";
        this.inpUsernameEmailP2.interactable = true;
        this.inpPasswordP2.interactable = true;

        this.btnLoginP2.gameObject.SetActive(true);
        this.btnLogoutP2.gameObject.SetActive(false);
    }

    public void showPasswordP1ButtonOnClick()
    {
        this.inpPasswordP1.contentType = TMP_InputField.ContentType.Standard;
        this.btnShowPasswordP1.gameObject.SetActive(false);
        this.btnHidePasswordP1.gameObject.SetActive(true);
        this.inpPasswordP1.gameObject.SetActive(false);
        this.inpPasswordP1.gameObject.SetActive(true);
    }

    public void hidePasswordP1ButtonOnClick()
    {
        this.inpPasswordP1.contentType = TMP_InputField.ContentType.Password;
        this.btnHidePasswordP1.gameObject.SetActive(false);
        this.btnShowPasswordP1.gameObject.SetActive(true);
        this.inpPasswordP1.gameObject.SetActive(false);
        this.inpPasswordP1.gameObject.SetActive(true);

    }

    public void showPasswordP2ButtonOnClick()
    {
        this.inpPasswordP2.contentType = TMP_InputField.ContentType.Standard;
        this.btnShowPasswordP2.gameObject.SetActive(false);
        this.btnHidePasswordP2.gameObject.SetActive(true);
        this.inpPasswordP2.gameObject.SetActive(false);
        this.inpPasswordP2.gameObject.SetActive(true);
    }

    public void hidePasswordP2ButtonOnClick()
    {
        this.inpPasswordP2.contentType = TMP_InputField.ContentType.Password;
        this.btnHidePasswordP2.gameObject.SetActive(false);
        this.btnShowPasswordP2.gameObject.SetActive(true);
        this.inpPasswordP2.gameObject.SetActive(false);
        this.inpPasswordP2.gameObject.SetActive(true);

    }

    private bool areDifferentUsers()
    {
        if (gameManager.getInstance().cuantityOfPlayers == 2)
        {
            if (inpUsernameEmailP1.text != inpUsernameEmailP2.text)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    public void changePasswordButtonOnClick()
    {
        SceneManager.LoadScene("PasswordRecoveryScene");
    }

    public void backButtonOnClik()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
