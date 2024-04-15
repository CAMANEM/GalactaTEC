using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        // ToDo: Updates quantity of players to 1
        Debug.Log("Updates quantity of players to 1");

        SceneManager.LoadScene("LoginScene");
    }

    public void _2PlayersButtonOnClick()
    {
        // ToDo: Updates quantity of players to 2
        Debug.Log("Updates quantity of players to 2");

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
