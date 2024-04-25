using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using GameManager;

public class mainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("P1: " + gameManager.getInstance().player1Username);
        Debug.Log("P2: " + gameManager.getInstance().player2Username);
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

    public void editProfileButtonOnClick()
    {
        SceneManager.LoadScene("EditProfileScene");
    }

    public void ExitButtonOnClick()
    {
        Application.Quit();
    }
}
