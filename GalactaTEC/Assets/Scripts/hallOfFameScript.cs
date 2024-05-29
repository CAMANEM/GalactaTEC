using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

using audio_manager;
using GameManager;
using UserManager;

[System.Serializable]
public class HallOfFameEntry
{
    public string photoPath;
    public string username;
    public int score;
    public int scoreAverage;
}

[System.Serializable]
public class HallOfFameEntryList
{
    public List<HallOfFameEntry> hallOfFame;
}

[System.Serializable]
public class hallOfFameScript : MonoBehaviour
{
    // Path to JSON file
    private string jsonFilePath = Application.dataPath + "/Data/hallOfFame.json";

    // Hall of Fame Entry List
    public List<HallOfFameEntry> hallOfFameEntries = new List<HallOfFameEntry>();

    // Prefab to add players
    public GameObject playerEntryPrefab;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.getInstance().playBackgroundSoundtrack();

        this.setHallOfFameEntriesByPlayers();

        this.hallOfFameEntries.OrderBy(hallOfFameEntry => hallOfFameEntry.score).ThenByDescending(hallOfFameEntry => hallOfFameEntry.scoreAverage).ToList();

        // Show data in console and instantiate player entries
        int numEntries = Mathf.Min(hallOfFameEntries.Count, 5); // Limit to top 5 entries

        // Instantiate and configure entries for each player
        for (int i = 0; i < 5; i++)
        {   
            // Instantiate the player input prefab
            GameObject playerEntry = Instantiate(playerEntryPrefab, transform);

            // Get prefab components
            TMPro.TextMeshProUGUI txtNum = playerEntry.transform.Find("txtNum").GetComponent<TMPro.TextMeshProUGUI>();
            UnityEngine.UI.Image imgUser = playerEntry.transform.Find("imgUser").GetComponent<UnityEngine.UI.Image>();
            TMPro.TextMeshProUGUI txtUsername = playerEntry.transform.Find("txtUsername").GetComponent<TMPro.TextMeshProUGUI>();
            TMPro.TextMeshProUGUI txtScore = playerEntry.transform.Find("txtScore").GetComponent<TMPro.TextMeshProUGUI>();

            if (i < numEntries)
            {
                // Get the Hall of Fame entry
                HallOfFameEntry entry = hallOfFameEntries[i];

                // Configure player data in prefab components
                txtNum.text = (i + 1).ToString();
                txtUsername.text = entry.username;
                txtScore.text = entry.score.ToString();

                // Load player image from specified path
                if (!string.IsNullOrEmpty(entry.photoPath) && File.Exists(Application.dataPath + entry.photoPath))
                {
                    byte[] imageData = File.ReadAllBytes(Application.dataPath + entry.photoPath);
                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(imageData);
                    imgUser.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                }
                else
                {
                    Debug.LogWarning("Could not find player image: " + entry.photoPath);
                }
            }
            else
            {
                txtNum.text = (i + 1).ToString();
                txtUsername.text = "Available";
                txtScore.text = "---";
            }

            // Move down 130 pixels per instance
            float yOffset = -130f;
            playerEntry.transform.localPosition += new Vector3(0f, yOffset * i, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setHallOfFameEntriesByPlayers()
    {
        string usersJSON = File.ReadAllText(gameManager.getInstance().usersPath);

        Users users = JsonUtility.FromJson<Users>(usersJSON);

        HallOfFameEntry hallOfFameEntry;

        foreach (User user in users)
        {
            int userTotalScore = 0;
            int userScoresNonZero = 0;
            int userScoreAverage = 0;
            
            foreach (int score in user.scoreRecord)
            {
                if (score > 0)
                {
                    userScoresNonZero++;
                    userTotalScore += score;
                }
            }

            if (userScoresNonZero != 0)
            {
                userScoreAverage = userTotalScore / userScoresNonZero;
            }

            for (int i = 0; i < userScoresNonZero; i++)
            {
                hallOfFameEntry = new HallOfFameEntry();

                hallOfFameEntry.photoPath = user.userImage;
                hallOfFameEntry.username = user.username;
                hallOfFameEntry.scoreAverage = userScoreAverage;
                hallOfFameEntry.score = user.scoreRecord[i];
                this.hallOfFameEntries.Add(hallOfFameEntry);
            }
        }

        IEnumerable<HallOfFameEntry> hallOfFameEntriesToOrder = this.hallOfFameEntries.OrderByDescending(hallOfFameEntry => hallOfFameEntry.score).ThenByDescending(hallOfFameEntry => hallOfFameEntry.scoreAverage);

        this.hallOfFameEntries = hallOfFameEntriesToOrder.ToList();
    }

    public void _BackButtonOnClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
