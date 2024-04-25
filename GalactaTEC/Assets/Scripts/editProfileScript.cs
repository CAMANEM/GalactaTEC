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

public class editProfileScript : MonoBehaviour { 

    // GUI elements
    public RawImage imgUserName;
    public RawImage imgShipName;

    public TMP_InputField inpName;
    public TMP_InputField inpEmail;
    public TMP_InputField inpUsername;

    // Start is called before the first frame update
    void Start()
    {
        gameManager.getInstance().isUserEditingProfileInformation = true;

        User user = getUserInformation();

        if (user != null)
        {
            inpName.text = user.name;
            inpEmail.text = user.email;
            inpUsername.text = user.username;

            // Loads user image

            try
            {
                if (!string.IsNullOrEmpty(user.userImage))
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(user.userImage);

                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(imageBytes);

                    imgUserName.texture = texture;
                }
            }
            catch
            {

                Debug.LogError("Something went wrong loading the selected user image: " + user.userImage);
            }

            // Loads ship image

            try
            {
                if (!string.IsNullOrEmpty(user.shipImage))
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(user.shipImage);

                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(imageBytes);

                    imgShipName.texture = texture;
                }
            }
            catch
            {

                Debug.LogError("Something went wrong loading the selected ship image: " + user.shipImage);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private User getUserInformation()
    {
        string usersJSON = File.ReadAllText(gameManager.getInstance().usersPath);

        Users users = JsonUtility.FromJson<Users>(usersJSON);

        User foundUser = null;

        foreach (User user in users.users)
        {
            if (user.email == gameManager.getInstance().playerEditingInformation)
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

    public void changePasswordButtonOnClick()
    {
        gameManager.getInstance().emailRecoveringPassword = gameManager.getInstance().playerEditingInformation;
        SceneManager.LoadScene("PasswordRecoveryScene");
    }

    public void backButtonOnClick()
    {
        gameManager.getInstance().isUserEditingProfileInformation = false;
        SceneManager.LoadScene("MainMenuScene");
    }
}
