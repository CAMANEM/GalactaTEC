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

[System.Serializable]
public class editProfileScript : MonoBehaviour { 

    // GUI elements
    public RawImage imgUserName;
    public RawImage imgShipName;

    public TMP_InputField inpName;
    public TMP_InputField inpEmail;
    public TMP_InputField inpUsername;

    // Variables
    string userImagePath = "";
    string shipImagePath = "";

    byte[] imageBytesUser;

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
                    byte[] imageBytes = System.IO.File.ReadAllBytes(Application.dataPath + user.userImage);

                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(imageBytes);

                    imgUserName.texture = texture;
                }
            }
            catch
            {

                Debug.LogError("Something went wrong loading the selected user image: " + Application.dataPath + user.userImage);
            }

            // Loads ship image
            try
            {
                if (!string.IsNullOrEmpty(user.shipImage))
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(Application.dataPath + user.shipImage);

                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(imageBytes);

                    imgShipName.texture = texture;
                }
            }
            catch
            {
                Debug.LogError("Something went wrong loading the selected ship image: " + Application.dataPath + user.shipImage);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<User> getSignedUsers()
    {
        string usersJSON = File.ReadAllText(gameManager.getInstance().usersPath);

        Users users = JsonUtility.FromJson<Users>(usersJSON);

        return users.users;
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

    public void changeImageButtonOnClick()
    {
        try
        {
            userImagePath = UnityEditor.EditorUtility.OpenFilePanel("Select a new user image", "", "png,jpg,jpeg,gif,bmp");

            if (!string.IsNullOrEmpty(userImagePath))
            {
                imageBytesUser = System.IO.File.ReadAllBytes(userImagePath);

                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(imageBytesUser);

                imgUserName.texture = texture;
            }
        }
        catch
        {
            Debug.LogError("Something went wrong loading the selected user image. Image selected: " + userImagePath);
        }
    }

    public void changeShipButtonOnClick()
    {
        try
        {
            shipImagePath = UnityEditor.EditorUtility.OpenFilePanel("Select a new ship image", "", "png,jpg,jpeg,gif,bmp");

            if (!string.IsNullOrEmpty(shipImagePath))
            {
                byte[] imageBytes = System.IO.File.ReadAllBytes(shipImagePath);

                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(imageBytes);

                imgShipName.texture = texture;
            }
        }
        catch
        {

            Debug.LogError("Something went wrong loading the selected ship image. Image selected: " + shipImagePath);
        }
    }

    public void applyChangesButtonOnClick()
    {
        //List<User> users = getSignedUsers();

        string usersJSON = File.ReadAllText(gameManager.getInstance().usersPath);

        Users users = JsonUtility.FromJson<Users>(usersJSON);

        foreach (User user in users)
        {
            if (user.email == gameManager.getInstance().playerEditingInformation)
            {
                user.name = inpName.text;
                user.email = inpEmail.text;
                user.username = inpUsername.text;

                string newUserImage = "/Data/UserPhotos/" + Path.GetFileName(userImagePath);
                if (userImagePath != "" && user.userImage != newUserImage)
                {
                    // Delete previous user image
                    if (!string.IsNullOrEmpty(user.userImage) && File.Exists(Application.dataPath + user.userImage))
                    {
                        System.IO.File.Delete(Application.dataPath + user.userImage);
                    }

                    // Update user image
                    user.userImage = newUserImage;

                    // Copy and paste the new user image in ../Data/UserPhotos
                    string savePath = Application.dataPath + newUserImage;
                    File.WriteAllBytes(savePath, imageBytesUser);
                }

                // Do the same for shipImage
                user.shipImage = shipImagePath;

                /*if (emailIsUnique(user.email) && usernameIsUnique(user.username))
                {
                    if (emailExists(user.email))
                    {
                        users.users.Add(user);
                        users.cuantity += 1;

                        string updatedJSON = JsonUtility.ToJson(users);

                        File.WriteAllText(usersPath, updatedJSON);

                        //MessageBox.Show("What are you waiting for? Let's play!", "Account created succesfully!");
                        Debug.Log("Account created succesfully!");
                        goToLoginScene();
                    }
                }*/

                // After finish validations
                string updatedJSON = JsonUtility.ToJson(users);
                File.WriteAllText(gameManager.getInstance().usersPath, updatedJSON);
            }
            else
            {
                Debug.Log("Something went wrong loading player information");
            }
        }
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
