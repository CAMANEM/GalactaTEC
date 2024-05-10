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
using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine.SceneManagement;

using GameManager;
using alerts_manager;
using audio_manager;

[System.Serializable]
public class User
{
    public string name;
    public string email;
    public string username;
    public string password;
    public string userImage;
    public int ship;
    public int[] scoreRecord;
    public string[] favoriteSoundtracks;
}

[System.Serializable]
public class Users : IEnumerable<User>
{
    public List<User> users;
    public int cuantity;

    public IEnumerator<User> GetEnumerator()
    {
        return users.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

[System.Serializable]
public class signUpSceneScript : MonoBehaviour  
{
    // GUI elements
    public RawImage imgUserName;

    public TMP_InputField inpName;
    public TMP_InputField inpEmail;
    public TMP_InputField inpUsername;
    public TMP_InputField inpPassword;

    public TMP_Text txtShipName;
    public TMP_Text txtSoundtrack;

    public Button btnIsInFavorites;
    public Button btnisNotInFavoritesButton;
    public Button btnAddTofavorites;
    public Button btnRemoveFromFavorites;
    public Button btnPlaySoundtrack;
    public Button btnStopSoundtrack;

    public Transform pntShipGenerator;
    public GameObject sprKlaedFighterPrefab;
    public GameObject sprKlaedScoutPrefab;
    public GameObject sprNairanFighterPrefab;
    public GameObject sprNairanScoutPrefab;
    public GameObject sprNautolanBomberPrefab;
    public GameObject sprNautolanScoutPrefab;
    public GameObject sprPipOpsMarauderPrefab;

    // Variables
    private string userImagePath = "";
    private string shipImagePath = "";

    private byte[] imageBytesUser;

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

    private string usersPath = Application.dataPath + "/Data/users.json";

    // Start is called before the first frame update
    void Start()
    {
        this.sprPipOpsMarauder = Instantiate(this.sprPipOpsMarauderPrefab, this.pntShipGenerator.position, Quaternion.identity);
        this.sprPipOpsMarauder.transform.localScale = new Vector3(7.5f, 7.5f, 7.5f);
        this.txtShipName.text = "PipOps\nMarauder";
        this.shipIndex = 0;

        AudioManager.getInstance().playBackgroundSoundtrack();

        this.loadSountracks();
        this.userFavoriteSoundtracks = this.soundtracks;

        this.soundtrackIndex = 0;
        txtSoundtrack.text = this.showableSoundtracks[this.soundtrackIndex];

        updateFavoriteSoundtracksUI();
    }

    // Update is called once per frame
    void Update()
    {
        
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
                } else if (this.sprNautolanScout != null)
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
                } else
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

        if(this.shipIndex > 6)
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
        } catch
        {
            Debug.LogError("Something went wrong loading the selected user image. Image selected: " + userImagePath + ". Debug code: 1.");
        }
    }

    private List<User> getSignedUsers()
    {
        string usersJSON = File.ReadAllText(usersPath);

        Users users = JsonUtility.FromJson<Users>(usersJSON);

        return users.users;
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
            this.soundtrackIndex = this.soundtracks.Length-1;
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
        btnAddTofavorites.gameObject.SetActive(false);
    }
        else
        {
        btnIsInFavorites.gameObject.SetActive(false);
        btnisNotInFavoritesButton.gameObject.SetActive(true);

        btnRemoveFromFavorites.gameObject.SetActive(false);
        btnAddTofavorites.gameObject.SetActive(true);
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
            }else if (level1Soundtracks < 2)
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

    public void createAccountButtonOnClick()
    {
        if (File.Exists(usersPath))
        {
            if (emailIsUnique(inpEmail.text) && usernameIsUnique(inpUsername.text) && passwordIsValid(inpPassword.text) && CheckFavoriteSoundtracks())
            {
                if (emailExists(inpEmail.text))
                {
                    string usersJSON = File.ReadAllText(usersPath);

                    Users users = JsonUtility.FromJson<Users>(usersJSON);

                    User user = new User();
                    user.name = inpName.text;
                    user.email = inpEmail.text;
                    user.username = inpUsername.text;
                    user.password = this.generatePasswordHashKey(inpPassword.text);
                    user.ship = this.shipIndex;
                    user.favoriteSoundtracks = this.userFavoriteSoundtracks;

                    string newUserImage = "/Data/UserPhotos/" + Path.GetFileName(userImagePath);
                    if (userImagePath != "" && user.userImage != newUserImage)
                    {
                        // Update user image
                        user.userImage = newUserImage;

                        // Copy and paste the new user image in ../Data/UserPhotos
                        string savePath = Application.dataPath + newUserImage;
                        File.WriteAllBytes(savePath, imageBytesUser);
                    }

                    user.scoreRecord = new int[5];

                    users.users.Add(user);
                    users.cuantity += 1;

                    string updatedJSON = JsonUtility.ToJson(users);

                    File.WriteAllText(usersPath, updatedJSON);

                    AlertsManager.getInstance().showAlert("Welcome!", "Account created succesfully", "Awesome!");
                }
            }
        }
        else
        {
            if (passwordIsValid(inpPassword.text) && CheckFavoriteSoundtracks())
            {
                if (emailExists(inpEmail.text))
                {
                    User user = new User();
                    user.name = inpName.text;
                    user.email = inpEmail.text;
                    user.username = inpUsername.text;
                    user.password = this.generatePasswordHashKey(inpPassword.text);
                    user.ship = this.shipIndex;
                    user.favoriteSoundtracks = this.userFavoriteSoundtracks;

                    string newUserImage = "/Data/UserPhotos/" + Path.GetFileName(userImagePath);
                    if (userImagePath != "" && user.userImage != newUserImage)
                    {
                        // Update user image
                        user.userImage = newUserImage;

                        // Copy and paste the new user image in ../Data/UserPhotos
                        string savePath = Application.dataPath + newUserImage;
                        File.WriteAllBytes(savePath, imageBytesUser);
                    }

                    user.scoreRecord = new int[5];

                    List<User> userList = new List<User>();
                    userList.Add(user);
                    Users users = new Users();
                    users.users = userList;
                    users.cuantity = userList.Count;

                    string userJson = JsonUtility.ToJson(users);

                    File.WriteAllText(usersPath, userJson);
                    AlertsManager.getInstance().showAlert("Welcome!", "Account created succesfully", "Awesome!");
                }
            }     
        }
    }

    private bool usernameIsUnique(string username)
    {
        List<User> users = getSignedUsers();

        if(users.Exists(user => user.username == username) || username == "")
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

        if(users.Exists(user => user.email == email) || email == "")
        {
            AlertsManager.getInstance().showAlert("Wait", "The given email is empty or not available", "Retry");
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
                            AlertsManager.getInstance().showAlert("Wait", "The given password does not match the required conditions: At least one special character", "Retry");
                            return false;
                        }
                    }
                    else
                    {
                        /*MessageBox.Show("The given password does not match the required conditions", "Password conditions:\n\n" +
                                "- At least 7 characters\n- At least one uppercase character\n- At least one lowercase character\n" +
                                "- At least one number\n- At least one special character\n\n" +
                                "The given password does not have enough numbers");*/
                        AlertsManager.getInstance().showAlert("Wait", "The given password does not match the required conditions: At least one number", "Retry");
                        return false;
                    }
                }
                else
                {
                    /*MessageBox.Show("The given password does not match the required conditions", "Password conditions:\n\n" +
                                "- At least 7 characters\n- At least one uppercase character\n- At least one lowercase character\n" +
                                "- At least one number\n- At least one special character\n\n" +
                                "The given password does not have enough lowercase characters");*/
                    AlertsManager.getInstance().showAlert("Wait", "The given password does not match the required conditions: At least one lowercase character", "Retry");
                    return false;
                }
            }
            else
            {
                /*MessageBox.Show("The given password does not match the required conditions", "Password conditions:\n\n" +
                                "- At least 7 characters\n- At least one uppercase character\n- At least one lowercase character\n" +
                                "- At least one number\n- At least one special character\n\n" +
                                "The given password does not have enough uppercase characters");*/
                AlertsManager.getInstance().showAlert("Wait", "The given password does not match the required conditions: At least one uppercase character", "Retry");
                return false;
            }
        }
        else
        {
            /*MessageBox.Show("The given password does not match the required conditions", "Password conditions:\n\n" +
                                "- At least 7 characters\n- At least one uppercase character\n- At least one lowercase character\n" +
                                "- At least one number\n- At least one special character\n\n" +
                                "The given password does not have enough characters");*/
            AlertsManager.getInstance().showAlert("Wait", "The given password does not match the required conditions: At least 7 characters", "Retry");
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
            Debug.Log("We have troubles to confirm your email address. Debug code: 3.");
            return false;
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

    private void loadSountracks()
    {
        this.soundtracks = AudioManager.getInstance().backgroundSoundtracksPaths.Concat(AudioManager.getInstance().level1SoundtracksPaths).Concat(AudioManager.getInstance().level2SoundtracksPaths).Concat(AudioManager.getInstance().level3SoundtracksPaths).ToArray();
        this.showableSoundtracks = this.soundtracks.Select(s => s.Split('/').Last()).ToArray();
    }

    public void backButtonOnClik()
    {
        if (AudioManager.getInstance().clipPath.StartsWith("Audio/Soundtracks/Level/Level - Section "))
        {
            AudioManager.getInstance().stopSoundtrack();
        }

        goToLoginScene();
    }
}

