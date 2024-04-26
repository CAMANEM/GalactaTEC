using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager
{
    public class gameManager : MonoBehaviour
    {
        // Singleton design pattern variables and functions
        private static gameManager instance;

        public static gameManager getInstance()
        {
            if(instance == null)
            {
                // Si no hay una instancia en la escena, busca si ya existe
                instance = FindObjectOfType<gameManager>();
                // Si no existe, crea un nuevo objeto GameManager en la escena
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<gameManager>();
                }
            }
            return instance;
        }

        // variables
        public int cuantityOfPlayers = 0;
        public string player1Email = "";
        public string player2Email = "";
        public string player1Username = "";
        public string player2Username = "";

        public string playerToPlay = "";

        public string validResetPasswordCode = null;
        public string emailRecoveringPassword = "";

        public bool isUserEditingProfileInformation = false;
        public string playerEditingInformation = "";

        // Defines the dictionary to store attack levels and patterns
        private Dictionary<int, int> levelAttackPatterns = new Dictionary<int, int>();

        // Paths
        public string usersPath = Application.dataPath + "/Data/users.json";

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
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
    }
}
