using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuScript : MonoBehaviour
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

    public void HallofFameButtonOnClick()
    {
        SceneManager.LoadScene("HallOfFameScene");
    }

    public void ConfigureGameButtonOnClick()
    {
        SceneManager.LoadScene("ConfigureGameScene");
    }

    public void ExitButtonOnClick()
    {
        Application.Quit();
    }
}