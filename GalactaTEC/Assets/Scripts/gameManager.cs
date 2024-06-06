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

        private string podiumSceneTitle = "";
        private int score1 = 0;
        private int score2 = 0;

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

        // Defines the dictionary to store attack levels and patterns
        private Dictionary<int, int> levelAttackPatternsInGame = new Dictionary<int, int>{
            { 1, 1 },
            { 2, 2 },
            { 3, 3 }
        };

        public bool didGameJustFinished = false;

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

        public void setPodiumSceneTitle(string podiumSceneTitle)
        {
            this.podiumSceneTitle = podiumSceneTitle;
        }

        public string getPodiumSceneTitle()
        {
            return this.podiumSceneTitle;
        }

        public void setScore1(int score1)
        {
            this.score1 = score1;
        }

        public int getScore1()
        {
            return this.score1;
        }

        public void setScore2(int score2)
        {
            this.score2 = score2;
        }

        public int getScore2()
        {
            return this.score2;
        }

        public void setCuantityOfPlayers(int cuantityOfPlayers)
        {
            this.cuantityOfPlayers = cuantityOfPlayers;
        }

        public void calculateRandomAttackPatterns()
        {
            Debug.Log("Original dictionary:\nLevel 1: " + getAttackPatternForLevel(1) + "\nLevel 2: " + getAttackPatternForLevel(2) + "\nLevel 3: " + getAttackPatternForLevel(3));

            Debug.Log("In game dictionary:\nLevel 1: " + getInGameAttackPatternForLevel(1) + "\nLevel 2: " + getInGameAttackPatternForLevel(2) + "\nLevel 3: " + getInGameAttackPatternForLevel(3));

            for (int i = 1; i <= 3; i++)
            {
                setInGameAttackPatternForLevel(i, getAttackPatternForLevel(i));

                if (this.getAttackPatternForLevel(i) == 5)
                {
                    bool isAttackPatternOk = false;

                    while(!isAttackPatternOk)
                    {
                        setInGameAttackPatternForLevel(i, UnityEngine.Random.Range(1, 5));

                        if ((this.getInGameAttackPatternForLevel(1) != this.getInGameAttackPatternForLevel(2) || (this.getInGameAttackPatternForLevel(1) == 5 && this.getInGameAttackPatternForLevel(2) == 5)) &&
                            (this.getInGameAttackPatternForLevel(1) != this.getInGameAttackPatternForLevel(3) || (this.getInGameAttackPatternForLevel(1) == 5 && this.getInGameAttackPatternForLevel(3) == 5)) &&
                            (this.getInGameAttackPatternForLevel(2) != this.getInGameAttackPatternForLevel(3) || (this.getInGameAttackPatternForLevel(2) == 5 && this.getInGameAttackPatternForLevel(3) == 5)))
                        {
                            isAttackPatternOk = true;
                        }
                    }
                }
            }

            Debug.Log("Calculated attack patterns:\nLevel 1: " + getInGameAttackPatternForLevel(1) + "\nLevel 2: " + getInGameAttackPatternForLevel(2) + "\nLevel 3: " + getInGameAttackPatternForLevel(3));
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

        public int getInGameAttackPatternForLevel(int level)
        {
            int attackPattern;
            // Try to get the attack pattern for the given level
            if (levelAttackPatternsInGame.TryGetValue(level, out attackPattern))
            {
                return attackPattern;
            }
            else
            {
                Debug.LogError("The specified level does not have an in game attack pattern assigned.");
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

        public void setInGameAttackPatternForLevel(int level, int attackPattern)
        {
            // Check if the level exists in the dictionary
            if (levelAttackPatternsInGame.ContainsKey(level))
            {
                // Modify the attack pattern for the given level
                levelAttackPatternsInGame[level] = attackPattern;
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

            if(hallOfFameRanking.Count <= 4)
            {
                return true;
            } else
            {
                if(score > hallOfFameRanking[4].score)
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
}
