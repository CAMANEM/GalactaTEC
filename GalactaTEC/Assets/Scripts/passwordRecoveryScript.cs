using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine.SceneManagement;


using GameManager;

public class passwordRecoveryScript : MonoBehaviour
{
    // GUI elements
    public TMP_InputField inpEmail;
    public TMP_InputField inpCode;

    string usersPath = Application.dataPath + "/Data/users.json";

    private static System.Random randomNumber = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        if (gameManager.getInstance().emailRecoveringPassword != "")
        {
            inpEmail.text = gameManager.getInstance().emailRecoveringPassword;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<User> getSignedUsers()
    {
        string usersJSON = File.ReadAllText(usersPath);

        Users users = JsonUtility.FromJson<Users>(usersJSON);

        return users.users;
    }

    private bool isEmailSignedUp(string email)
    {
        List<User> users = getSignedUsers();

        if(users.Exists(user => user.email == email))
        {
            //MessageBox.Show("The given email is already used", "Try another email please.");
            Debug.Log("The given email is signed up");
            return true;
        }
        else
        {
            Debug.Log("The given email is not signed up");
            return false;
        }
    }

    private string generateCode()
    {
        string codigo = "";
        for (int i = 0; i < 5; i++)
        {
            codigo += randomNumber.Next(10);
        }
        return codigo;
    }

    private void sendCodeByEmail(string email, string code)
    {
        string fromMail = "pipodevs.galactatec@gmail.com";
        string fromPassword = "frezafraqedljgwj";

        MailMessage message = new MailMessage();
        message.From = new MailAddress(fromMail);
        message.Subject = "Reset your GalactaTEC password";
        message.To.Add(new MailAddress(email));
        message.Body = "<html><body>Here is your code to reset your password: " + code + "<br>" +
        "Go back to GalactaTEC app and type that code, then you will be able to set a new password for your account!<br><br>" + 
        "See you soon,<br>" +
        "The GalactaTEC team" +
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
            Debug.Log("Mail sended succesfully");
            StartCoroutine(manageCodeValidity(code));
            Debug.Log("5 minutes running...");
        }
        catch
        {
            Debug.Log("We have troubles sending the code, please try again");
        }
    }

    IEnumerator manageCodeValidity(string code)
    {
        gameManager.getInstance().validResetPasswordCode = code;
        gameManager.getInstance().emailRecoveringPassword = inpEmail.text;

        yield return new WaitForSeconds(300);

        
    }

    public void sendButtonOnClick()
    {
        if(isEmailSignedUp(inpEmail.text))
        {
            sendCodeByEmail(inpEmail.text, generateCode());
        }
    }

    public void verifyButtonOnClick()
    {
        try
        {
            if(gameManager.getInstance().validResetPasswordCode == inpCode.text)
            {
                Debug.Log("Correct code");
                SceneManager.LoadScene("NewPasswordScene");
            }
            else
            {
                Debug.Log("Wrong code");
            }
        }
        catch
        {
            Debug.Log("Wrong code");
        }
    }

    public void backButtonOnClick()
    {
        gameManager.getInstance().validResetPasswordCode = null;
        gameManager.getInstance().emailRecoveringPassword = "";

        if (gameManager.getInstance().isUserEditingProfileInformation)
        {
            SceneManager.LoadScene("EditProfileScene");
        } else if (gameManager.getInstance().cuantityOfPlayers == 1)
        {
            SceneManager.LoadScene("1PLoginScene");
        }
        else
        {
            SceneManager.LoadScene("2PLoginScene");
        }
    }
}
