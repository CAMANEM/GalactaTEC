using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine.SceneManagement;

using GameManager;
using UserManager;
using audio_manager;

public class newPasswordScript : MonoBehaviour
{
    // GUI elements
    public TMP_InputField inpNewPassword;
    public TMP_Text txtEmail;

    public Button btnShowPassword;
    public Button btnHidePassword;

    string usersPath = Application.dataPath + "/Data/users.json";

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.getInstance().playBackgroundSoundtrack();
        txtEmail.text = gameManager.getInstance().emailRecoveringPassword;
    }

    // Update is called once per frame
    void Update()
    {

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

    public void doneButtonOnClick()
    {
        if (passwordIsValid(inpNewPassword.text))
        {
            List<User> signedUsers = userManager.getInstance().getSignedUsers();

            foreach (User user in signedUsers)
            {
                if (user.email == gameManager.getInstance().emailRecoveringPassword)
                {
                    user.password = this.generatePasswordHashKey(inpNewPassword.text);
                }
            }
            Users updatedUsers = new Users();
            updatedUsers.users = signedUsers;
            updatedUsers.cuantity = signedUsers.Count;

            string updatedJSON = JsonUtility.ToJson(updatedUsers);

            File.WriteAllText(usersPath, updatedJSON);

            Debug.Log("Password changed succesfully!");

            gameManager.getInstance().validResetPasswordCode = null;
            gameManager.getInstance().emailRecoveringPassword = "";

            if (gameManager.getInstance().isUserEditingProfileInformation)
            {
                SceneManager.LoadScene("EditProfileScene");
            }
            else if (gameManager.getInstance().cuantityOfPlayers == 1)
            {
                SceneManager.LoadScene("1PLoginScene");
            }
            else
            {
                SceneManager.LoadScene("2PLoginScene");
            }
        }
        else
        {
            Debug.Log("There was a problem changing your password, please try again.");
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

    public void showPasswordButtonOnClick()
    {
        this.inpNewPassword.contentType = TMP_InputField.ContentType.Standard;
        this.btnShowPassword.gameObject.SetActive(false);
        this.btnHidePassword.gameObject.SetActive(true);
        this.inpNewPassword.gameObject.SetActive(false);
        this.inpNewPassword.gameObject.SetActive(true);
    }

    public void hidePasswordButtonOnClick()
    {
        this.inpNewPassword.contentType = TMP_InputField.ContentType.Password;
        this.btnHidePassword.gameObject.SetActive(false);
        this.btnShowPassword.gameObject.SetActive(true);
        this.inpNewPassword.gameObject.SetActive(false);
        this.inpNewPassword.gameObject.SetActive(true);

    }

    public void backButtonOnClick()
    {
        gameManager.getInstance().validResetPasswordCode = null;
        SceneManager.LoadScene("PasswordRecoveryScene");
    }
}
