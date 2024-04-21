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
        gameManager.getInstance().setCuantityOfPlayers(1);

        SceneManager.LoadScene("1PLoginScene");
    }

    public void _2PlayersButtonOnClick()
    {
        gameManager.getInstance().setCuantityOfPlayers(2);

        SceneManager.LoadScene("2PLoginScene");
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
