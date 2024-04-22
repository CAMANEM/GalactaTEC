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
        [SerializeField] GameObject sceneLoader;
        [SerializeField] TextMeshProUGUI txtDialogueTitle;
        [SerializeField] Button btnPlayer;
        [SerializeField] Button btnPlayers;
        [SerializeField] Button btnCloseDialogue;

        public string optionMenu;

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

        public void ConfigureGameButtonOnClick()
        {
            this.optionMenu = "ConfigureGame";
            this.txtDialogueTitle.text = "Settings for:";
            ShowDialogue();
        }

        public void StartGameButtonOnClick()
        {
            this.optionMenu = "StartGame";
            this.txtDialogueTitle.text = "Game for:";
            ShowDialogue();
        }

        public void PlayerButtonOnClick()
        {
            if (optionMenu == "ConfigureGame")
            {
                pnlDialogue.SetActive(false);
                SceneManager.LoadScene("ConfigureGameScene");
            }
            else if (optionMenu == "StartGame")
            {
                // Make sure the GameObject sceneLoader is assigned in the Inspector
                if (sceneLoader != null)
                {
                    // Get the loadingScene component from the GameObject sceneLoader
                    var loadingScript = sceneLoader.GetComponent<loadingScene>();

                    // Calls the LoadScene method of the loadingSceneScript script
                    if (loadingScript != null)
                    {
                        loadingScript.LoadScene(11);
                    }
                    else
                    {
                        Debug.LogError("The GameObject sceneLoader does not have the loadingScene script attached to it.");
                    }
                }
                else
                {
                    Debug.LogError("The GameObject sceneLoader has not been assigned in the Inspector.");
                }
            } 
        }

        public void PlayersButtonOnClick()
        {
            if (optionMenu == "ConfigureGame")
            {
                pnlDialogue.SetActive(false);
                SceneManager.LoadScene("ConfigureGameScene");
            }
            else if (optionMenu == "StartGame")
            {
                // Make sure the GameObject sceneLoader is assigned in the Inspector
                if (sceneLoader != null)
                {
                    // Get the loadingScene component from the GameObject sceneLoader
                    var loadingScript = sceneLoader.GetComponent<loadingScene>();

                    // Calls the LoadScene method of the loadingSceneScript script
                    if (loadingScript != null)
                    {
                        loadingScript.LoadScene(11);
                    }
                    else
                    {
                        Debug.LogError("The GameObject sceneLoader does not have the loadingScene script attached to it.");
                    }
                }
                else
                {
                    Debug.LogError("The GameObject sceneLoader has not been assigned in the Inspector.");
                }
            }
        }

        public void ShowDialogue()
        {
            pnlDialogue.SetActive(true);
        }

        public void HideDialogue()
        {
            pnlDialogue.SetActive(false);
            this.optionMenu = "";
            this.txtDialogueTitle.text = "";
        }
    }
}