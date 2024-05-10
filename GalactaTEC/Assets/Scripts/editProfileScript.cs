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

    public TMP_InputField inpName;
    public TMP_InputField inpEmail;
    public TMP_InputField inpUsername;

    public TMP_Text txtShipName;
    public TMP_Text txtSoundtrack;

    public Button btnIsInFavorites;
    public Button btnisNotInFavoritesButton;
    public Button btnAddToFavorites;
    public Button btnRemoveFromFavorites;
    public Button btnPlaySoundtrack;
    public Button btnStopSoundtrack;

    public Button btnApplyChanges;

    public Transform pntShipGenerator;
    public GameObject sprKlaedFighterPrefab;
    public GameObject sprKlaedScoutPrefab;
    public GameObject sprNairanFighterPrefab;
    public GameObject sprNairanScoutPrefab;
    public GameObject sprNautolanBomberPrefab;
    public GameObject sprNautolanScoutPrefab;
    public GameObject sprPipOpsMarauderPrefab;

    // Variables
    string userImagePath = "";
    string shipImagePath = "";

    byte[] imageBytesUser;

    private GameObject sprKlaedFighter;
    private GameObject sprKlaedScout;
    private GameObject sprNairanFighter;
    private GameObject sprNairanScout;
    private GameObject sprNautolanBomber;
    private GameObject sprNautolanScout;
    private GameObject sprPipOpsMarauder;
    private int shipIndex;

    private string[] soundtracks;
    private string[] showableSoundtracks;
    private string[] userFavoriteSoundtracks;
    private int soundtrackIndex;

    private User previousUser;
    private bool isChange = false;
    private bool userChangedImage = false;
    private bool userChangedSoundtracks = false;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.getInstance().playBackgroundSoundtrack();

        this.loadSountracks();

        this.soundtrackIndex = 0;
        txtSoundtrack.text = this.showableSoundtracks[this.soundtrackIndex];

        gameManager.getInstance().isUserEditingProfileInformation = true;

        this.previousUser = getUserInformation();

        if (this.previousUser != null)
        {
            inpName.text = this.previousUser.name;
            inpEmail.text = this.previousUser.email;
            inpUsername.text = this.previousUser.username;
            this.shipIndex = this.previousUser.ship;
            this.userFavoriteSoundtracks = this.previousUser.favoriteSoundtracks;

            this.updateFavoriteSoundtracksUI();

            // Loads user image
            try
            {
                if (!string.IsNullOrEmpty(this.previousUser.userImage))
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(Application.dataPath + this.previousUser.userImage);

                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(imageBytes);

                    imgUserName.texture = texture;
                }
            }
            catch
            {

                Debug.LogError("Something went wrong loading the selected user image: " + Application.dataPath + this.previousUser.userImage + ". Debug code: 4.");
            }

            this.loadShipSprite();
        }

        this.isChange = false;
        this.btnApplyChanges.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.areSoundtracksEqual();

        if(this.previousUser.name != this.inpName.text || this.previousUser.email != this.inpEmail.text || this.previousUser.username != this.inpUsername.text || 
            this.previousUser.ship != this.shipIndex || this.userChangedSoundtracks || this.userChangedImage)
        {
            this.isChange = true;
        }
        else
        {
            this.isChange = false;
        }

        this.btnApplyChanges.interactable = this.isChange;
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

    private void loadShipSprite()
    {
        switch (this.shipIndex)
        {
            case 0:
                if (this.sprNautolanScout != null)
                {
                    Destroy(this.sprNautolanScout.gameObject);
                }
                else if (this.sprKlaedFighter != null)
                {
                    Destroy(this.sprKlaedFighter.gameObject);
                }
                else
                {
                    try
                    {
                        Destroy(this.sprNautolanScout.gameObject);
                        Destroy(this.sprKlaedFighter.gameObject);
                    }
                    catch
                    {
                        Debug.Log("Something went wrong loading PipOps Marauder Ship. Debug log: 11");
                    }
                }

                this.sprPipOpsMarauder = Instantiate(this.sprPipOpsMarauderPrefab, this.pntShipGenerator.position, Quaternion.identity);
                this.sprPipOpsMarauder.transform.localScale = new Vector3(7.5f, 7.5f, 7.5f);
                this.txtShipName.text = "PipOps\nMarauder";

                break;
            case 1:
                if (this.sprPipOpsMarauder != null)
                {
                    Destroy(this.sprPipOpsMarauder.gameObject);
                }
                else if (this.sprKlaedScout != null)
                {
                    Destroy(this.sprKlaedScout.gameObject);
                }
                else
                {
                    try
                    {
                        Destroy(this.sprPipOpsMarauder.gameObject);
                        Destroy(this.sprKlaedScout.gameObject);
                    }
                    catch
                    {
                        Debug.Log("Something went wrong loading Kla'ed Fighter Ship. Debug log: 12");
                    }
                }

                this.sprKlaedFighter = Instantiate(this.sprKlaedFighterPrefab, this.pntShipGenerator.position, Quaternion.identity);
                this.sprKlaedFighter.transform.localScale = new Vector3(7.5f, 7.5f, 7.5f);
                this.txtShipName.text = "Kla'ed\nFighter";

                break;
            case 2:
                if (this.sprKlaedFighter != null)
                {
                    Destroy(this.sprKlaedFighter.gameObject);
                }
                else if (this.sprNairanFighter != null)
                {
                    Destroy(this.sprNairanFighter.gameObject);
                }
                else
                {
                    try
                    {
                        Destroy(this.sprKlaedFighter.gameObject);
                        Destroy(this.sprNairanFighter.gameObject);
                    }
                    catch
                    {
                        Debug.Log("Something went wrong loading Kla'ed Scout Ship. Debug log: 13");
                    }
                }

                this.sprKlaedScout = Instantiate(this.sprKlaedScoutPrefab, this.pntShipGenerator.position, Quaternion.identity);
                this.sprKlaedScout.transform.localScale = new Vector3(7.5f, 7.5f, 7.5f);
                this.txtShipName.text = "Kla'ed\nScout";
                break;
            case 3:
                if (this.sprKlaedScout != null)
                {
                    Destroy(this.sprKlaedScout.gameObject);
                }
                else if (this.sprNairanScout != null)
                {
                    Destroy(this.sprNairanScout.gameObject);
                }
                else
                {
                    try
                    {
                        Destroy(this.sprKlaedScout.gameObject);
                        Destroy(this.sprNairanScout.gameObject);
                    }
                    catch
                    {
                        Debug.Log("Something went wrong loading Nairan Fighter Ship. Debug log: 14");
                    }
                }

                this.sprNairanFighter = Instantiate(this.sprNairanFighterPrefab, this.pntShipGenerator.position, Quaternion.identity);
                this.sprNairanFighter.transform.localScale = new Vector3(6f, 6f, 6f);
                this.txtShipName.text = "Nairan\nFighter";

                break;
            case 4:
                if (this.sprNairanFighter != null)
                {
                    Destroy(this.sprNairanFighter.gameObject);
                }
                else if (this.sprNautolanBomber != null)
                {
                    Destroy(this.sprNautolanBomber.gameObject);
                }
                else
                {
                    try
                    {
                        Destroy(this.sprNairanFighter.gameObject);
                        Destroy(this.sprNautolanBomber.gameObject);
                    }
                    catch
                    {
                        Debug.Log("Something went wrong loading Nairan Scout Ship. Debug log: 15");
                    }
                }

                this.sprNairanScout = Instantiate(this.sprNairanScoutPrefab, this.pntShipGenerator.position, Quaternion.identity);
                this.sprNairanScout.transform.localScale = new Vector3(6f, 6f, 6f);
                this.txtShipName.text = "Nairan\nScout";

                break;
            case 5:
                if (this.sprNairanScout != null)
                {
                    Destroy(this.sprNairanScout.gameObject);
                }
                else if (this.sprNautolanScout != null)
                {
                    Destroy(this.sprNautolanScout.gameObject);
                }
                else
                {
                    try
                    {
                        Destroy(this.sprNairanScout.gameObject);
                        Destroy(this.sprNautolanScout.gameObject);
                    }
                    catch
                    {
                        Debug.Log("Something went wrong loading Nautolan Bomber Ship. Debug log: 16");
                    }
                }

                this.sprNautolanBomber = Instantiate(this.sprNautolanBomberPrefab, this.pntShipGenerator.position, Quaternion.identity);
                this.sprNautolanBomber.transform.localScale = new Vector3(6f, 6f, 6f);
                this.txtShipName.text = "Nautolan\nBomber";

                break;
            case 6:
                if (this.sprNautolanBomber != null)
                {
                    Destroy(this.sprNautolanBomber.gameObject);
                }
                else if (this.sprPipOpsMarauder != null)
                {
                    Destroy(this.sprPipOpsMarauder.gameObject);
                }
                else
                {
                    try
                    {
                        Destroy(this.sprNautolanBomber.gameObject);
                        Destroy(this.sprPipOpsMarauder.gameObject);
                    }
                    catch
                    {
                        Debug.Log("Something went wrong loading Nautolan Scout Ship. Debug log: 17");
                    }
                }

                this.sprNautolanScout = Instantiate(this.sprNautolanScoutPrefab, this.pntShipGenerator.position, Quaternion.identity);
                this.sprNautolanScout.transform.localScale = new Vector3(6f, 6f, 6f);
                this.txtShipName.text = "Nautolan\nScout";

                break;
        }
    }

    public void nextShipButtonOnClick()
    {
        this.shipIndex++;

        if (this.shipIndex > 6)
        {
            this.shipIndex = 0;
        }

        this.loadShipSprite();
    }

    public void previousShipButtonOnClick()
    {
        this.shipIndex--;

        if (this.shipIndex < 0)
        {
            this.shipIndex = 6;
        }

        this.loadShipSprite();
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

                this.userChangedImage = true;
            }
        }
        catch
        {
            Debug.LogError("Something went wrong loading the selected user image. Image selected: " + userImagePath + ". Debug code: 7");
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
                user.ship = this.shipIndex;
                user.favoriteSoundtracks = this.userFavoriteSoundtracks;

                AudioManager.getInstance().loggedUserFavoriteSoundtracks = this.userFavoriteSoundtracks;

                if (this.userChangedImage)
                {
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
                }

                if (this.previousUser.email != inpEmail.text)
                {
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
                    string updatedJSON = JsonUtility.ToJson(users);

                    File.WriteAllText(gameManager.getInstance().usersPath, updatedJSON);

                    AlertsManager.getInstance().showAlert("Great!", "Account updated succesfully", "Awesome!");
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
    
    public void areSoundtracksEqual()
    {
        if (this.previousUser.favoriteSoundtracks.Length != this.userFavoriteSoundtracks.Length)
        {
            this.userChangedSoundtracks = true;
        }

        List<string> list1 = this.previousUser.favoriteSoundtracks.ToList();
        List<string> list2 = this.userFavoriteSoundtracks.ToList();

        list1.Sort();
        list2.Sort();

        this.userChangedSoundtracks = !list1.SequenceEqual(list2);
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
