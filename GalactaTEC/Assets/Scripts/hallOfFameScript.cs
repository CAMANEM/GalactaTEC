using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
public class HallOfFameEntry
{
    public string photoPath;
    public string username;
    public int score;
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
        // Load JSON from file
        string jsonString = File.ReadAllText(jsonFilePath);

        // Deserialize the JSON into the list of hall of fame entries
        hallOfFameEntries = JsonUtility.FromJson<HallOfFameEntryList>(jsonString).hallOfFame;

        // Sort the list by scores from highest to lowest
        hallOfFameEntries.Sort((x, y) => y.score.CompareTo(x.score));

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

                byte[] imageData = File.ReadAllBytes(Application.dataPath + "/Data/UserPhotos/default-avatar.png");
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(imageData);
                imgUser.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
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

    public void _BackButtonOnClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
