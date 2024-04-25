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

[System.Serializable]
public class User
{
    public string name;
    public string email;
    public string username;
    public string password;
    public string userImage;
    public string shipImage;
    public int[] scoreRecord;
}

[System.Serializable]
public class Users
{
    public List<User> users;
    public int cuantity;
}

[System.Serializable]
public class signUpSceneScript : MonoBehaviour  
{
    // GUI elements
    public RawImage imgUserName;
    public RawImage imgShipName;

    public TMP_InputField inpName;
    public TMP_InputField inpEmail;
    public TMP_InputField inpUsername;
    public TMP_InputField inpPassword;

    // Variables
    string userImagePath = "";
    string shipImagePath = "";


    string usersPath = Application.dataPath + "/Data/users.json";


    // Start is called before the first frame update
    void Start()
    {
        // alertsScript.getInstance().setTitle("Testing title").setBody("Testing body").showAlert();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeImageButtonOnClick()
    {
        try
        {
            userImagePath = UnityEditor.EditorUtility.OpenFilePanel("Select a new user image", "", "png,jpg,jpeg,gif,bmp");

            if (!string.IsNullOrEmpty(userImagePath))
            {
                byte[] imageBytes = System.IO.File.ReadAllBytes(userImagePath);

                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(imageBytes);

                imgUserName.texture = texture;
            }
        } catch
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

    private List<User> getSignedUsers()
    {
        string usersJSON = File.ReadAllText(usersPath);

        Users users = JsonUtility.FromJson<Users>(usersJSON);

        return users.users;
    }

    public void createAccountButtonOnClick()
    {
        if (File.Exists(usersPath))
        {
            string usersJSON = File.ReadAllText(usersPath);

            Users users = JsonUtility.FromJson<Users>(usersJSON);

            User user = new User();
            user.name = inpName.text;
            user.email = inpEmail.text;
            user.username = inpUsername.text;
            user.password = inpPassword.text;
            user.userImage = userImagePath;
            user.shipImage = shipImagePath;
            user.scoreRecord = new int[5];

            if (emailIsUnique(user.email) && usernameIsUnique(user.username) && passwordIsValid(user.password))
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
            }
        }
        else
        {
            User user = new User();
            user.name = inpName.text;
            user.email = inpEmail.text;
            user.username = inpUsername.text;
            user.password = inpPassword.text;
            user.userImage = userImagePath;
            user.shipImage = shipImagePath;
            user.scoreRecord = new int[5];

            if (passwordIsValid(user.password))
            {
                if (emailExists(user.email))
                {
                    List<User> userList = new List<User>();
                    userList.Add(user);
                    Users users = new Users();
                    users.users = userList;
                    users.cuantity = userList.Count;

                    string userJson = JsonUtility.ToJson(users);

                    File.WriteAllText(usersPath, userJson);
                    //MessageBox.Show("What are you waiting for? Let's play!", "Account created succesfully!");
                    Debug.Log("Account created succesfully!");
                    goToLoginScene();
                }
            }     
        }
    }

    private bool usernameIsUnique(string username)
    {
        List<User> users = getSignedUsers();

        if(users.Exists(user => user.username == username) || username == "")
        {
            //MessageBox.Show("The given username is already used", "Try another username please.");
            Debug.Log("The given username is not available");
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

        if(users.Exists(user => user.email == email) || email == "")
        {
            //MessageBox.Show("The given email is already used", "Try another email please.");
            Debug.Log("The given email is not available");
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool passwordIsValid(string password)
    {
        if (Regex.IsMatch(password, ".{7,}"))
        {
            if (Regex.IsMatch(password, "[A-Z]"))
            {
                if (Regex.IsMatch(password, "[a-z]"))
                {
                    if (Regex.IsMatch(password, "[0-9]"))
                    {
                        if (Regex.IsMatch(password, @"[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]"))
                        {
                            return true;
                        }
                        else
                        {
                            /*MessageBox.Show("The given password does not match the required conditions", "Password conditions:\n\n" +
                                "- At least 7 characters\n- At least one uppercase character\n- At least one lowercase character\n" +
                                "- At least one number\n- At least one special character\n\n" +
                                "The given password does not have enough special characters");*/
                            Debug.Log("The given password does not have enough special characters");
                            return false;
                        }
                    }
                    else
                    {
                        /*MessageBox.Show("The given password does not match the required conditions", "Password conditions:\n\n" +
                                "- At least 7 characters\n- At least one uppercase character\n- At least one lowercase character\n" +
                                "- At least one number\n- At least one special character\n\n" +
                                "The given password does not have enough numbers");*/
                        Debug.Log("The given password does not have enough numbers");
                        return false;
                    }
                }
                else
                {
                    /*MessageBox.Show("The given password does not match the required conditions", "Password conditions:\n\n" +
                                "- At least 7 characters\n- At least one uppercase character\n- At least one lowercase character\n" +
                                "- At least one number\n- At least one special character\n\n" +
                                "The given password does not have enough lowercase characters");*/
                    Debug.Log("The given password does not have enough lowercase characters");
                    return false;
                }
            }
            else
            {
                /*MessageBox.Show("The given password does not match the required conditions", "Password conditions:\n\n" +
                                "- At least 7 characters\n- At least one uppercase character\n- At least one lowercase character\n" +
                                "- At least one number\n- At least one special character\n\n" +
                                "The given password does not have enough uppercase characters");*/
                Debug.Log("The given password does not have enough uppercase characters");
                return false;
            }
        }
        else
        {
            /*MessageBox.Show("The given password does not match the required conditions", "Password conditions:\n\n" +
                                "- At least 7 characters\n- At least one uppercase character\n- At least one lowercase character\n" +
                                "- At least one number\n- At least one special character\n\n" +
                                "The given password does not have enough characters");*/
            Debug.Log("The given password does not have enough characters");
            return false;
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
                /*MessageBox.Show("Wrong email domain", "The given email domain does not exist");*/
                Debug.Log("The given email domain does not exist");
                return false;
            }
        }
        else
        {
            //MessageBox.Show("Wrong email", "The given email does not exist");
            Debug.Log("The given email does not exist");
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
            Debug.Log("We have troubles to confirm your email address, please, check your email and try again");
            return false;
        }
    }

    private void goToLoginScene()
    {
        if (gameManager.getInstance().cuantityOfPlayers == 1)
        {
            SceneManager.LoadScene("1PLoginScene");
        }
        else
        {
            SceneManager.LoadScene("2PLoginScene");
        }
    }

    public void backButtonOnClik()
    {
        goToLoginScene();
    }
}

