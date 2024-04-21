using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using GameManager;

public class cnvTitleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
        gameManager.getInstance().cuantityOfPlayers = 1;

        SceneManager.LoadScene("LoginScene");
    }

    public void _2PlayersButtonOnClick()
    {
        gameManager.getInstance().cuantityOfPlayers = 2;

        SceneManager.LoadScene("LoginScene");
    }

    public void closeButtonOnClick()
    {
        Application.Quit();
    }

    public void helpButtonOnClick()
    {
        // ToDo: Opens help window
        Debug.Log("Opens help window");
    }
}
