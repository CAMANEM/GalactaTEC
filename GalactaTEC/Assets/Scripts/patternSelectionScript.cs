using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace patternSelectionManager
{
    public class patternSelectionScript : MonoBehaviour
    {
        [SerializeField] GameObject pnlPatternSelection;
        [SerializeField] Button btnClosePatternSelection;
        [SerializeField] TextMeshProUGUI  txtPatternSelectionTitle;
        public Toggle[] toggles;
        public string levelPrefix;

        // Start is called before the first frame update
        void Start()
        {
            InitializeToggleState();

            // The first time the cutscene opens, every level has attack pattern 1
            LoadToggleState();

            // Subscribe to each toggle's onValueChanged event
            foreach (Toggle toggle in toggles)
            {
                toggle.onValueChanged.AddListener(delegate { ToggleValueChanged(toggle); });
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        private static patternSelectionScript instance;

        public static patternSelectionScript getInstance()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<patternSelectionScript>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("patternSelectionManager");
                    instance = obj.AddComponent<patternSelectionScript>();
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

            btnClosePatternSelection.onClick.RemoveAllListeners();
            btnClosePatternSelection.onClick.AddListener(HidePatternSelection);
        }

        public void ShowPatternSelection()
        {
            pnlPatternSelection.SetActive(true);
        }

        public void HidePatternSelection()
        {
            //Debug.Log("Toggle state for level " + levelPrefix + ":");
            //for (int i = 0; i < toggles.Length; i++)
            //{
            //    int toggleState = PlayerPrefs.GetInt(levelPrefix + "Toggle" + i, 0);
            //    Debug.Log("Toggle " + i + ": " + toggleState);
            //}

            bool anyToggleSelected = false;
            for (int i = 0; i < toggles.Length; i++)
            {
                if (toggles[i].isOn)
                {
                    anyToggleSelected = true;
                }
            }

            if (!anyToggleSelected)
            {
                PlayerPrefs.SetInt(levelPrefix + "Toggle" + 0, 1);
                PlayerPrefs.Save();
            }

            pnlPatternSelection.SetActive(false);
            this.txtPatternSelectionTitle.text = "";
            this.levelPrefix = "";
        }

        public void Level1ButtonOnClick()
        {
            this.levelPrefix = "Level1";
            this.txtPatternSelectionTitle.text = "Attack pattern for level 1";
            LoadToggleState();
            ShowPatternSelection();
        }

        public void Level2ButtonOnClick()
        {
            this.levelPrefix = "Level2";
            this.txtPatternSelectionTitle.text = "Attack pattern for level 2";
            LoadToggleState();
            ShowPatternSelection();
        }

        public void Level3ButtonOnClick()
        {
            this.levelPrefix = "Level3";
            this.txtPatternSelectionTitle.text = "Attack pattern for level 3";
            LoadToggleState();
            ShowPatternSelection();
        }

        private void InitializeToggleState()
        {
            string level = "Level";
            for (int i = 1; i < 4; i++)
            {
                PlayerPrefs.SetInt(level + i + "Toggle" + 0, 1);
            }
            PlayerPrefs.Save();
        }

        private void ToggleValueChanged(Toggle changedToggle)
        {
            // If the toggle you changed is selected, deselect the others
            if (changedToggle.isOn)
            {
                foreach (Toggle toggle in toggles)
                {
                    if (toggle != changedToggle)
                    {
                        toggle.isOn = false;
                    }
                }

                // Save the state of the toggle that changed for this level
                SaveToggleState(changedToggle);
            }
        }

        private void SaveToggleState(Toggle toggle)
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                if (toggles[i] == toggle)
                {
                    PlayerPrefs.SetInt(levelPrefix + "Toggle" + i, 1);
                }
                else
                {
                    PlayerPrefs.SetInt(levelPrefix + "Toggle" + i, 0);
                }
            }
            PlayerPrefs.Save();
        }

        private void LoadToggleState()
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                int toggleState = PlayerPrefs.GetInt(levelPrefix + "Toggle" + i, 0);
                toggles[i].isOn = toggleState == 1;
            }
        }
    }

}