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
                instance = new gameManager();
            }
            return instance;
        }

        private gameManager() {}

        // variables
        public int cuantityOfPlayers = 0;
        public string player1Username = "";
        public string player2Username = "";

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
}
}