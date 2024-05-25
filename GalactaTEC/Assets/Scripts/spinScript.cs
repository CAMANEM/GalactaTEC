using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyUI.PickerWheelUI;
using TMPro;
using System.IO;

using GameManager;
using UserManager;

public class spinScript : MonoBehaviour
{
    [SerializeField] Button btnSpin;
    [SerializeField] TextMeshProUGUI txtSpin;
    [SerializeField] TextMeshProUGUI txtPlayer;
    [SerializeField] GameObject sceneLoader;

    [SerializeField] PickerWheel pickerWheel;
    private WheelPiece[] wheelPieces;

    // Start is called before the first frame update
    void Start()
    {
        wheelPieces = pickerWheel.GetWheelPieces();
        loadPickerWheelInfo();

        btnSpin.onClick.AddListener(() =>
        {
            btnSpin.interactable = false;
            txtSpin.text = "Spinning";

            pickerWheel.OnSpinStart(() =>
            {
                Debug.Log("Spin started...");
            });

            pickerWheel.OnSpinEnd(wheelPiece =>
            {
                btnSpin.interactable = true;
                txtSpin.text = "Spin";
                txtPlayer.text = wheelPiece.Label + "\nwill start the game";

                gameManager.getInstance().playerToPlay = wheelPiece.Label;
                gameManager.getInstance().setOneInsteadOfTwo(false);
                Invoke("LoadNewScene", 2.0f);
            });

            pickerWheel.Spin();
        });
    }

    void LoadNewScene()
    {
        // Make sure the GameObject sceneLoader is assigned in the Inspector
        if (sceneLoader != null)
        {
            // Get the loadingScene component from the GameObject sceneLoader
            var loadingScript = sceneLoader.GetComponent<loadingScene>();

            // Calls the LoadScene method of the loadingSceneScript script
            if (loadingScript != null)
            {
                loadingScript.LoadScene("GameScene");
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void loadPickerWheelInfo()
    {
        User user1 = userManager.getInstance().getUserByUsername(gameManager.getInstance().player1Username);
        User user2 = userManager.getInstance().getUserByUsername(gameManager.getInstance().player2Username);

        wheelPieces[0].Label = user1.username;
        wheelPieces[1].Label = user2.username;

        // Load player image from specified path
        if (File.Exists(Application.dataPath + user1.userImage))
        {
            byte[] imageData1 = File.ReadAllBytes(Application.dataPath + user1.userImage);
            Texture2D texture1 = new Texture2D(2, 2);
            texture1.LoadImage(imageData1);
            wheelPieces[0].Icon = Sprite.Create(texture1, new Rect(0, 0, texture1.width, texture1.height), new Vector2(0.5f, 0.5f));
        }
        else
        {
            Debug.LogWarning("Could not find player image: " + Application.dataPath + user1.userImage);
        }

        if (File.Exists(Application.dataPath + user2.userImage))
        {
            byte[] imageData2 = File.ReadAllBytes(Application.dataPath + user2.userImage);
            Texture2D texture2 = new Texture2D(2, 2);
            texture2.LoadImage(imageData2);
            wheelPieces[1].Icon = Sprite.Create(texture2, new Rect(0, 0, texture2.width, texture2.height), new Vector2(0.5f, 0.5f));
        }
        else
        {
            Debug.LogWarning("Could not find player image: " + Application.dataPath + user2.userImage);
        }
    }
}
