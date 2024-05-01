using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

using GameManager;
using audio_manager;

public class cnvTitleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.getInstance().playBackgroundSoundtrack();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void _1PlayerButtonOnClick()
    {
        gameManager.getInstance().setCuantityOfPlayers(1);

        SceneManager.LoadScene("1PLoginScene");
    }

    public void _2PlayersButtonOnClick()
    {
        gameManager.getInstance().setCuantityOfPlayers(2);

        SceneManager.LoadScene("2PLoginScene");
    }

    public void helpButtonOnClick()
    {
        PlayerPrefs.SetString("HelpScene", "TitleScene");
        PlayerPrefs.Save();
        SceneManager.LoadScene("HelpScene");
    }

    public void closeButtonOnClick()
    {
        Application.Quit();
    }
}
