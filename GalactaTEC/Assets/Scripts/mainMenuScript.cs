using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameManager;
using audio_manager;

public class mainMenuScript : MonoBehaviour
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

    public void HallOfFameButtonOnClick()
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

    public void BackButtonOnClick()
    {
        // Agregar ventana emergente indicando si est� seguro que quiere regresar y de ser as� se eliminar� el/los inicios de sesi�n

        gameManager.getInstance().cuantityOfPlayers = 0;
        gameManager.getInstance().player1Email = "";
        gameManager.getInstance().player2Email = "";
        gameManager.getInstance().player1Username = "";
        gameManager.getInstance().player2Username = "";

        SceneManager.LoadScene("TitleScene");
    }
}
