using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine.SceneManagement;


using GameManager;
using alertsManager;

public class loginSceneScript : MonoBehaviour
{
    public TMP_InputField inpUsernameEmailP1;
    public TMP_InputField inpPasswordP1;
    public TMP_InputField inpUsernameEmailP2;
    public TMP_InputField inpPasswordP2;

    string usersPath = Application.dataPath + "/Data/users.json";

    private bool player1isLoggedIn = false;
    private bool player2isLoggedIn = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Cuantity of players selected: " + gameManager.getInstance().cuantityOfPlayers);
    }

    // Update is called once per frame
    void Update()
    {
        
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

        if(users.Exists(user => (user.email == inpUsernameEmailP1.text && user.password == inpPasswordP1.text)))
        {
            gameManager.getInstance().player1Username = inpUsernameEmailP1.text;

            player1isLoggedIn = true;

            if((gameManager.getInstance().cuantityOfPlayers == 1) || (gameManager.getInstance().cuantityOfPlayers == 2 && player2isLoggedIn))
            {
                SceneManager.LoadScene("MainMenuScene");
            }
        }
        else
        {
            Debug.Log("Username or password are incorrect. Please try again.");
            player1isLoggedIn = false;
        }
    }

    public void loginP2ButtonOnClick()
    {
        List<User> users = getSignedUsers();

        if(users.Exists(user => (user.email == inpUsernameEmailP2.text && user.password == inpPasswordP2.text)))
        {
            gameManager.getInstance().player2Username = inpUsernameEmailP2.text;

            player2isLoggedIn = true;

            if(player1isLoggedIn)
            {
                SceneManager.LoadScene("MainMenuScene");
            }
        }
        else
        {
            Debug.Log("Username or password are incorrect. Please try again.");
            player2isLoggedIn = false;
        }
    }

    public void changePasswordButtonOnClick()
    {
        SceneManager.LoadScene("PasswordRecoveryScene");
    }
}
