using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyUI.PickerWheelUI;
using TMPro;
using System.IO;

public class spinScript : MonoBehaviour
{
    [SerializeField] Button btnSpin;
    [SerializeField] TextMeshProUGUI txtSpin;

    [SerializeField] PickerWheel pickerWheel;

    private WheelPiece[] wheelPieces;

    // Start is called before the first frame update
    void Start()
    {
        wheelPieces = pickerWheel.GetWheelPieces();
        PlayerPrefs.SetString("username" + 0, "Emarin19");
        PlayerPrefs.SetString("photoPath" + 0, "/Data/UserPhotos/emanuel.png");

        PlayerPrefs.SetString("username" + 1, "Jose216");
        PlayerPrefs.SetString("photoPath" + 1, "/Data/UserPhotos/andres.png");

        PlayerPrefs.Save();

        foreach (WheelPiece wheelPiece in pickerWheel.GetWheelPieces())
        {
            //wheelPiece.Amount = "";
        }

        for (int i = 0; i < wheelPieces.Length; i++)
        {
            wheelPieces[i].Label = PlayerPrefs.GetString("username" + i);

            // Load player image from specified path
            if (File.Exists(Application.dataPath + PlayerPrefs.GetString("photoPath" + i)))
            {
                byte[] imageData = File.ReadAllBytes(Application.dataPath + PlayerPrefs.GetString("photoPath" + i));
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(imageData);
                wheelPieces[i].Icon = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
            else
            {
                Debug.LogWarning("Could not find player image: " + PlayerPrefs.GetString("photoPath" + i));
            }
        }

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
                Debug.Log("Spin end: Label: " + wheelPiece.Label + ", Amount: " + wheelPiece.Amount);
                btnSpin.interactable = true;
                txtSpin.text = "Spin";
            });

            pickerWheel.Spin();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
