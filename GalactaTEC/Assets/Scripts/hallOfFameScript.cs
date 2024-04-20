using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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

    // Start is called before the first frame update
    void Start()
    {
        // Load JSON from file
        string jsonString = File.ReadAllText(jsonFilePath);

        // Deserialize the JSON into the list of hall of fame entries
        hallOfFameEntries = JsonUtility.FromJson<HallOfFameEntryList>(jsonString).hallOfFame;

        // Sort the list by scores from highest to lowest
        hallOfFameEntries.Sort((x, y) => y.score.CompareTo(x.score));

        // Show data in console
        foreach (var entry in hallOfFameEntries)
        {
            Debug.Log("Photo Path: " + entry.photoPath + ", Username: " + entry.username + ", Score: " + entry.score);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
