using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using UserManager;
using HallOfFame;

namespace GameManager
{
    public class gameManager : MonoBehaviour
    {
        // Singleton design pattern
        private static gameManager instance;

        public static gameManager getInstance()
        {
            if(instance == null)
            {
                // If there is no instance in the scene, check to see if it already exists
                instance = FindObjectOfType<gameManager>();
                // If it does not exist, create a new GameManager object in the scene
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<gameManager>();
                }
            }
            return instance;
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Variables
        public int cuantityOfPlayers = 0;
        public string player1Email = "";
        public string player2Email = "";
        public string player1Username = "";
        public string player2Username = "";
        private User currentPlayer = null;

        public string playerToPlay = "";
        public bool oneInsteadOfTwo = true;

        public string validResetPasswordCode = null;
        public string emailRecoveringPassword = "";

        public bool isUserEditingProfileInformation = false;
        public string playerEditingInformation = "";

        // Defines the dictionary to store attack levels and patterns
        private Dictionary<int, int> levelAttackPatterns = new Dictionary<int, int>{
            { 1, 5 },
            { 2, 5 },
            { 3, 5 }
        };

        // Paths
        public string usersPath = Application.dataPath + "/Data/users.json";

        public void setOneInsteadOfTwo(bool oneInsteadOfTwo)
        {
            this.oneInsteadOfTwo = oneInsteadOfTwo;
        }

        public bool getOneInsteadOfTwo()
        {
            return this.oneInsteadOfTwo;
        }

        public void setCurrentPlayer(User currentPlayer)
        {
            this.currentPlayer = currentPlayer;
        }

        public User getCurrentPlayer()
        {
            return this.currentPlayer;
        }

        public void setCuantityOfPlayers(int cuantityOfPlayers)
        {
            this.cuantityOfPlayers = cuantityOfPlayers;
        }

        public int getAttackPatternForLevel(int level)
        {
            int attackPattern;
            // Try to get the attack pattern for the given level
            if (levelAttackPatterns.TryGetValue(level, out attackPattern))
            {
                return attackPattern;
            }
            else
            {
                Debug.LogError("The specified level does not have an attack pattern assigned.");
                return -1;
            }
        }

        public void setAttackPatternForLevel(int level, int attackPattern)
        {
            // Check if the level exists in the dictionary
            if (levelAttackPatterns.ContainsKey(level))
            {
                // Modify the attack pattern for the given level
                levelAttackPatterns[level] = attackPattern;
            }
            else
            {
                Debug.LogError("The specified level does not exist in the dictionary.");
            }
        }

        public void addLevelAttackPattern(int level, int attackPattern)
        {
            // Check if the level does not exist in the dictionary
            if (!levelAttackPatterns.ContainsKey(level))
            {
                // Add a new level and attack pattern
                levelAttackPatterns.Add(level, attackPattern);
            }
        }

        public Dictionary<int, int> getlevelAttackPatterns()
        {
            return levelAttackPatterns;
        }

        public bool isScoreNewRecord(int score)
        {
            hallOfFameScript hallOfFame = new hallOfFameScript();

            string usersJSON = File.ReadAllText(gameManager.getInstance().usersPath);

            Users users = JsonUtility.FromJson<Users>(usersJSON);

            HallOfFameEntriesAdapter hallOfFameEntriesAdapter = new HallOfFameEntriesAdapter(users);

            List<HallOfFameEntry> hallOfFameRanking = hallOfFameEntriesAdapter.adaptHallOfFameEntriesByPlayers();

            if (score > hallOfFameRanking[hallOfFameRanking.Count-1].score)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
