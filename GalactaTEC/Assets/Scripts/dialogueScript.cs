using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

using GameManager;

namespace dialogueManager
{
    public class dialogueScript : MonoBehaviour
    {
        [SerializeField] GameObject pnlDialogue;
        [SerializeField] GameObject sceneLoader;
        [SerializeField] TextMeshProUGUI txtDialogueTitle;
        [SerializeField] TextMeshProUGUI txtOption1ButtonText;
        [SerializeField] TextMeshProUGUI txtOption2ButtonText;
        [SerializeField] Button btnOption1;
        [SerializeField] Button btnOption2;
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

        public void StartGameButtonOnClick()
        {
            this.optionMenu = "StartGame";
            this.txtDialogueTitle.text = "Game for:";
            this.txtOption1ButtonText.text = "1 Player";
            this.txtOption2ButtonText.text = "2 Players";
            ShowDialogue();
        }

        public void editPlayerButtonOnClick()
        {
            if (gameManager.getInstance().cuantityOfPlayers == 2)
            {
                this.optionMenu = "editProfile";
                this.txtDialogueTitle.text = "Profile to edit:";
                this.txtOption1ButtonText.fontSize = 30;
                this.txtOption2ButtonText.fontSize = 30;
                this.txtOption1ButtonText.text = gameManager.getInstance().player1Username;
                this.txtOption2ButtonText.text = gameManager.getInstance().player2Username;
                ShowDialogue();
            } else
            {
                gameManager.getInstance().playerEditingInformation = gameManager.getInstance().player1Username;
                SceneManager.LoadScene("EditProfileScene");
            }
        }

        public void Option1ButtonOnClick()
        {
            if (optionMenu == "StartGame")
            {
                // Make sure the GameObject sceneLoader is assigned in the Inspector
                if (sceneLoader != null)
                {
                    // Get the loadingScene component from the GameObject sceneLoader
                    var loadingScript = sceneLoader.GetComponent<loadingScene>();

                    // Calls the LoadScene method of the loadingSceneScript script
                    if (loadingScript != null)
                    {
                        loadingScript.LoadScene(10); // Load GameScene
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
            } else if (optionMenu == "editProfile")
            {
                gameManager.getInstance().playerEditingInformation = gameManager.getInstance().player1Username;
                SceneManager.LoadScene("EditProfileScene");
            }

            HideDialogue();
        }

        public void Option2ButtonOnClick()
        {
            if (optionMenu == "StartGame")
            {
                SceneManager.LoadScene("PickerWheelScene");
            }
            else if (optionMenu == "editProfile")
            {
                gameManager.getInstance().playerEditingInformation = gameManager.getInstance().player2Username;
                SceneManager.LoadScene("EditProfileScene");
            }
            HideDialogue();
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
            this.txtOption1ButtonText.text = "";
            this.txtOption2ButtonText.text = "";
            this.txtOption1ButtonText.fontSize = 40;
            this.txtOption2ButtonText.fontSize = 40;
        }
    }
}