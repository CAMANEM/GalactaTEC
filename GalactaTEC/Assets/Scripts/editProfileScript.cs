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
using alerts_manager;
using audio_manager;

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
    byte[] imageBytesShip;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.getInstance().playBackgroundSoundtrack();

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

                Debug.LogError("Something went wrong loading the selected user image: " + Application.dataPath + user.userImage + ". Debug code: 4.");
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
                Debug.LogError("Something went wrong loading the selected ship image: " + Application.dataPath + user.shipImage + ". Debug code: 5.");
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
                Debug.Log("Something went wrong loading player information. Debug code: 6.");
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
            Debug.LogError("Something went wrong loading the selected user image. Image selected: " + userImagePath + ". Debug code: 7");
        }
    }

    public void changeShipButtonOnClick()
    {
        try
        {
            shipImagePath = UnityEditor.EditorUtility.OpenFilePanel("Select a new ship image", "", "png,jpg,jpeg,gif,bmp");

            if (!string.IsNullOrEmpty(shipImagePath))
            {
                imageBytesShip = System.IO.File.ReadAllBytes(shipImagePath);

                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(imageBytesShip);

                imgShipName.texture = texture;
            }
        }
        catch
        {

            Debug.LogError("Something went wrong loading the selected ship image. Image selected: " + shipImagePath + ". Debug code: 8.");
        }
    }

    public void applyChangesButtonOnClick()
    {
        string usersJSON = File.ReadAllText(gameManager.getInstance().usersPath);

        Users users = JsonUtility.FromJson<Users>(usersJSON);

        foreach (User user in users)
        {
            if (user.email == gameManager.getInstance().playerEditingInformation)
            {
                string previousEmail = user.email;
                string previousUsername = user.username;

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

                Debug.Log("Updated user image path: " + user.userImage);

                // Do the same for shipImage
                string newShipImage = "/Data/ShipPhotos/" + Path.GetFileName(shipImagePath);
                if (shipImagePath != "" && user.shipImage != newShipImage)
                {
                    // Delete previous ship image
                    if (!string.IsNullOrEmpty(user.shipImage) && File.Exists(Application.dataPath + user.shipImage))
                    {
                        System.IO.File.Delete(Application.dataPath + user.shipImage);
                    }

                    // Update ship image
                    user.shipImage = newShipImage;

                    // Copy and paste the new ship image in ../Data/ShipPhotos
                    string savePath = Application.dataPath + newShipImage;
                    File.WriteAllBytes(savePath, imageBytesShip);
                }

                if ((emailIsUnique(user.email) || user.email == previousEmail) && (usernameIsUnique(user.username) || user.username == previousUsername))
                {
                    if (emailExists(user.email))
                    {
                        string updatedJSON = JsonUtility.ToJson(users);

                        File.WriteAllText(gameManager.getInstance().usersPath, updatedJSON);

                        AlertsManager.getInstance().showAlert("Great!", "Account updated succesfully", "Awesome!");
                    }
                }
            }
            else
            {
                Debug.Log("Something went wrong loading player information. Debug code: 9.");
            }
        }
    }

    private bool usernameIsUnique(string username)
    {
        List<User> users = getSignedUsers();

        if (users.Exists(user => user.username == username) || username == "")
        {
            AlertsManager.getInstance().showAlert("Wait", "The given username is empty or not available", "Retry");
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool emailIsUnique(string email)
    {
        List<User> users = getSignedUsers();

        if (users.Exists(user => user.email == email) || email == "")
        {
            AlertsManager.getInstance().showAlert("Wait", "The given email is empty or not available", "Retry");
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool emailExists(string email)
    {
        if (isEmailValid(email))
        {
            string[] parts = email.Split('@');
            string domain = parts[1];

            if (IsDomainValid(domain))
            {
                return sendConfirmationEmail(email);
            }
            else
            {
                AlertsManager.getInstance().showAlert("Wait", "The given email domain does not exist", "Retry");
                return false;
            }
        }
        else
        {
            AlertsManager.getInstance().showAlert("Wait", "The given email does not exist", "Retry");
            return false;
        }
    }

    private bool isEmailValid(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private bool IsDomainValid(string domain)
    {
        try
        {
            IPHostEntry ipHost = Dns.GetHostEntry(domain);
            return ipHost.AddressList.Length > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private bool sendConfirmationEmail(string email)
    {
        string fromMail = "pipodevs.galactatec@gmail.com";
        string fromPassword = "frezafraqedljgwj";

        MailMessage message = new MailMessage();
        message.From = new MailAddress(fromMail);
        message.Subject = "Welcome to GalactaTEC!";
        message.To.Add(new MailAddress(email));
        message.Body = "<html><body>" +
            "We are very excited to have you here!<br>" +
            "This help us to confirm this email is yours, now we can be in touch!<br>" +
            "Don't worry, you are not expected to do anything, just close this mail and start enjoying the game.<br>" +
            "This is the beggining of something big!<br><br>" +
            "Welcome,<br>" +
            "The GalactaTEC team." +
            "</body></html>";
        message.IsBodyHtml = true;

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(fromMail, fromPassword),
            EnableSsl = true,
        };

        try
        {
            smtpClient.Send(message);
            return true;
        }
        catch
        {
            //MessageBox.Show("Wrong email", "We have troubles to confirm your email address, please, check your email and try again");
            Debug.Log("We have troubles to confirm your email address, please, check your email and try again. Debug code: 10.");
            return false;
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
