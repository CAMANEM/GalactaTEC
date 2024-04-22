using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace dialogueManager
{
    public class dialogueScript : MonoBehaviour
    {
        [SerializeField] GameObject pnlDialogue;
        [SerializeField] Button btnPlayer;
        [SerializeField] Button btnPlayers;
        [SerializeField] Button btnCloseDialogue;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private static dialogueScript instance;

        public static dialogueScript getInstance()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<dialogueScript>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("dialogueManager");
                    instance = obj.AddComponent<dialogueScript>();
                }
            }
            return instance;
        }

        void Awake()
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

            btnCloseDialogue.onClick.RemoveAllListeners();
            btnCloseDialogue.onClick.AddListener(HideDialogue);
        }

        public void ShowDialogue()
        {
            pnlDialogue.SetActive(true);
        }

        public void PlayerButtonOnClick()
        {
            pnlDialogue.SetActive(false);
            SceneManager.LoadScene("ConfigureGameScene");
        }

        public void PlayersButtonOnClick()
        {
            pnlDialogue.SetActive(false);
            SceneManager.LoadScene("ConfigureGameScene");
        }

        public void HideDialogue()
        {
            pnlDialogue.SetActive(false);
        }
    }
}