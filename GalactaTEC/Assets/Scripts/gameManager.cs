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
        public string player1Username = "";
        public string player2Username = "";

        public string validResetPasswordCode = null;
        public string emailRecoveringPassword = "";

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
    }
}
