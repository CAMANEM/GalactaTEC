using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameManager;

public class patternSelectionScript : MonoBehaviour
{
    [SerializeField] GameObject pnlPatternSelection;
    [SerializeField] Button btnClosePatternSelection;
    [SerializeField] TextMeshProUGUI  txtPatternSelectionTitle;
    public Toggle[] toggles;
    public int level;

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

    public void Level1ButtonOnClick()
    {
        this.level = 1;
        this.txtPatternSelectionTitle.text = "Attack pattern for level 1";
        LoadToggleState();
        ShowPatternSelection();
    }

    public void Level2ButtonOnClick()
    {
        this.level = 2;
        this.txtPatternSelectionTitle.text = "Attack pattern for level 2";
        LoadToggleState();
        ShowPatternSelection();
    }

    public void Level3ButtonOnClick()
    {
        this.level = 3;
        this.txtPatternSelectionTitle.text = "Attack pattern for level 3";
        LoadToggleState();
        ShowPatternSelection();
    }

    public void closeButtonOnClick()
    {
        HidePatternSelection();
    }

    private void InitializeToggleState()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].isOn = false;
        }

        for (int i = 1; i < 4; i++)
        {
            gameManager.getInstance().addLevelAttackPattern(i, 1);
        }
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
                gameManager.getInstance().setAttackPatternForLevel(level, i + 1);
            }
        }
    }

    private void LoadToggleState()
    {
        int activeToggle = gameManager.getInstance().getAttackPatternForLevel(level) - 1;
        for (int i = 0; i < toggles.Length; i++)
        {
            if (i == activeToggle)
            {
                toggles[i].isOn = true;
            }
        }
    }

    public void ShowPatternSelection()
    {
        pnlPatternSelection.SetActive(true);
    }

    public void HidePatternSelection()
    {
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
            gameManager.getInstance().setAttackPatternForLevel(level, 1);
        }

        pnlPatternSelection.SetActive(false);
        this.txtPatternSelectionTitle.text = "";
    }
}
