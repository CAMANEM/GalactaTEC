using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager;
using System;
using System.IO;
using TMPro;

public class GameSceneScript : MonoBehaviour
{

    public RawImage player1Image;
    [SerializeField] TextMeshProUGUI userName;
    string imagePath;
    // Start is called before the first frame update
    void Start()
    {
        // userName.text = gameManager.getInstance().player1Username;

        userName.text = "Daniel";


        try
        {
            // string imagePath = gameManager.getInstance().player1Email; 

            string imagePath = "D:/Documents HDD/Repositories/GalactaTEC/GalactaTEC/Assets/Data/UserPhotos/andres.png";

            if (!string.IsNullOrEmpty(imagePath))
            {
                byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);

                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(imageBytes);

                player1Image.texture = texture;
            }
        } catch
        {
            Debug.LogError("Something went wrong loading the selected user image. Image selected: " + imagePath);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private User getUserByEmail(string email)
    {
        string usersJSON = File.ReadAllText(gameManager.getInstance().usersPath);

        Users users = JsonUtility.FromJson<Users>(usersJSON);

        User foundUser = null;

        foreach (User user in users.users)
        {
            if (user.email == email)
            {
                foundUser = user;
            }
            else
            {
                Debug.Log("Something went wrong loading player information");
            }
        }

        return foundUser;
    }

}
