using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;
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

    public TMP_Text txtSoundtrack;

    public Button btnIsInFavorites;
    public Button btnisNotInFavoritesButton;
    public Button btnAddToFavorites;
    public Button btnRemoveFromFavorites;
    public Button btnPlaySoundtrack;
    public Button btnStopSoundtrack;

    // Variables
    string userImagePath = "";
    string shipImagePath = "";

    byte[] imageBytesUser;
    byte[] imageBytesShip;

    private string[] soundtracks;
    private string[] showableSoundtracks;
    private string[] userFavoriteSoundtracks;
    private int soundtrackIndex;

    private bool isChange = false;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.getInstance().playBackgroundSoundtrack();

        this.loadSountracks();

        this.soundtrackIndex = 0;
        txtSoundtrack.text = this.showableSoundtracks[this.soundtrackIndex];

        gameManager.getInstance().isUserEditingProfileInformation = true;

        User user = getUserInformation();

        if (user != null)
        {
            inpName.text = user.name;
            inpEmail.text = user.email;
            inpUsername.text = user.username;
            this.userFavoriteSoundtracks = user.favoriteSoundtracks;

            this.updateFavoriteSoundtracksUI();

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

        this.isChange = false;
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

    public void addToFavoritesButtonOnClick()
    {
        int indexToAdd = Array.IndexOf(this.userFavoriteSoundtracks, this.soundtracks[this.soundtrackIndex]);
        if (indexToAdd == -1)
        {
            Array.Resize(ref this.userFavoriteSoundtracks, this.userFavoriteSoundtracks.Length + 1);
            this.userFavoriteSoundtracks[this.userFavoriteSoundtracks.Length - 1] = this.soundtracks[this.soundtrackIndex];
        }

        updateFavoriteSoundtracksUI();
    }

    public void removeFromFavoritesButtonOnClick()
    {
        int indexToRemove = Array.IndexOf(this.userFavoriteSoundtracks, this.soundtracks[this.soundtrackIndex]);
        if (indexToRemove != -1)
        {
            string[] copyUserFavoriteSoundtracks = new string[this.userFavoriteSoundtracks.Length];
            Array.Copy(this.userFavoriteSoundtracks, copyUserFavoriteSoundtracks, this.userFavoriteSoundtracks.Length);

            for (int i = indexToRemove; i < copyUserFavoriteSoundtracks.Length - 1; i++)
            {
                copyUserFavoriteSoundtracks[i] = copyUserFavoriteSoundtracks[i + 1];
            }
            Array.Resize(ref copyUserFavoriteSoundtracks, copyUserFavoriteSoundtracks.Length - 1);

            this.userFavoriteSoundtracks = copyUserFavoriteSoundtracks;
        }

        updateFavoriteSoundtracksUI();
    }

    public void playNextSoundtrackButtonOnClick()
    {
        this.soundtrackIndex++;

        if (this.soundtrackIndex >= this.soundtracks.Length)
        {
            this.soundtrackIndex = 0;
        }

        txtSoundtrack.text = this.showableSoundtracks[this.soundtrackIndex];

        updateFavoriteSoundtracksUI();

        if (btnStopSoundtrack.gameObject.activeSelf)
        {
            this.playSoundtrackButtonOnClick();
        }
    }

    public void playPreviousSoundtrackButtonOnClick()
    {
        this.soundtrackIndex--;

        if (this.soundtrackIndex < 0)
        {
            this.soundtrackIndex = this.soundtracks.Length - 1;
        }

        txtSoundtrack.text = this.showableSoundtracks[this.soundtrackIndex];

        updateFavoriteSoundtracksUI();

        if (btnStopSoundtrack.gameObject.activeSelf)
        {
            this.playSoundtrackButtonOnClick();
        }
    }

    public void playSoundtrackButtonOnClick()
    {
        AudioManager.getInstance().playSpecificSoundtrack(this.soundtracks[this.soundtrackIndex]);
        this.btnPlaySoundtrack.gameObject.SetActive(false);
        this.btnStopSoundtrack.gameObject.SetActive(true);
    }

    public void stopSoundtrackButtonOnClick()
    {
        AudioManager.getInstance().stopSoundtrack();
        this.btnPlaySoundtrack.gameObject.SetActive(true);
        this.btnStopSoundtrack.gameObject.SetActive(false);
    }

    private void updateFavoriteSoundtracksUI()
    {
        if (isSounditrackInUserFavorites(this.soundtracks[this.soundtrackIndex]))
        {
            btnIsInFavorites.gameObject.SetActive(true);
            btnisNotInFavoritesButton.gameObject.SetActive(false);

            btnRemoveFromFavorites.gameObject.SetActive(true);
            btnAddToFavorites.gameObject.SetActive(false);
        }
        else
        {
            btnIsInFavorites.gameObject.SetActive(false);
            btnisNotInFavoritesButton.gameObject.SetActive(true);

            btnRemoveFromFavorites.gameObject.SetActive(false);
            btnAddToFavorites.gameObject.SetActive(true);
        }
    }

    private bool isSounditrackInUserFavorites(string soundtrack)
    {
        foreach (string str in this.userFavoriteSoundtracks)
        {
            if (str == soundtrack)
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckFavoriteSoundtracks()
    {
        int menuSoundtracks = this.userFavoriteSoundtracks.Count(s => s.StartsWith("Audio/Soundtracks/Menu/"));
        int level1Soundtracks = this.userFavoriteSoundtracks.Count(s => s.StartsWith("Audio/Soundtracks/Level/Level - Section 1"));
        int level2Soundtracks = this.userFavoriteSoundtracks.Count(s => s.StartsWith("Audio/Soundtracks/Level/Level - Section 2"));
        int level3Soundtracks = this.userFavoriteSoundtracks.Count(s => s.StartsWith("Audio/Soundtracks/Level/Level - Section 3"));


        if (menuSoundtracks < 5 || level1Soundtracks < 2 || level2Soundtracks < 2 || level3Soundtracks < 2)
        {
            if (menuSoundtracks < 5)
            {
                AlertsManager.getInstance().showAlert("Wait", "You should select at least 5 soundtracks for menus", "Retry");
            }
            else if (level1Soundtracks < 2)
            {
                AlertsManager.getInstance().showAlert("Wait", "You should select at least 2 soundtracks for sections 100", "Retry");
            }
            else if (level2Soundtracks < 2)
            {
                AlertsManager.getInstance().showAlert("Wait", "You should select at least 2 soundtracks for sections 200", "Retry");
            }
            else if (level3Soundtracks < 2)
            {
                AlertsManager.getInstance().showAlert("Wait", "You should select at least 2 soundtracks for sections 300", "Retry");
            }

            return false;
        }
        else
        {
            return true;
        }

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
                user.favoriteSoundtracks = this.userFavoriteSoundtracks;

                AudioManager.getInstance().loggedUserFavoriteSoundtracks = this.userFavoriteSoundtracks;

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

                if ((emailIsUnique(user.email) || user.email == previousEmail) && (usernameIsUnique(user.username) || user.username == previousUsername) && CheckFavoriteSoundtracks())
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

    private void loadSountracks()
    {
        this.soundtracks = AudioManager.getInstance().backgroundSoundtracksPaths.Concat(AudioManager.getInstance().level1SoundtracksPaths).Concat(AudioManager.getInstance().level2SoundtracksPaths).Concat(AudioManager.getInstance().level3SoundtracksPaths).ToArray();
        this.showableSoundtracks = this.soundtracks.Select(s => s.Split('/').Last()).ToArray();
    }

    public void changePasswordButtonOnClick()
    {
        gameManager.getInstance().emailRecoveringPassword = gameManager.getInstance().playerEditingInformation;
        SceneManager.LoadScene("PasswordRecoveryScene");
    }

    public void backButtonOnClick()
    {
        gameManager.getInstance().isUserEditingProfileInformation = false;
        if (this.isChange || AudioManager.getInstance().clipPath.StartsWith("Audio/Soundtracks/Level/Level - Section ") || btnisNotInFavoritesButton.gameObject.activeSelf)
        {
            AudioManager.getInstance().stopSoundtrack();
        }
        SceneManager.LoadScene("MainMenuScene");
    }
}
