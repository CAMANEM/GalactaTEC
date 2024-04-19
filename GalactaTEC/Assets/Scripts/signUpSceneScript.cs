using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class signUpSceneScript : MonoBehaviour  
{
    string imagePath;
    
    public RawImage userImage;
    public RawImage shipImage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectUserImage()
    {
        try
        {
            string imagePath = UnityEditor.EditorUtility.OpenFilePanel("Select a new user image", "", "png,jpg,jpeg,gif,bmp");

            if (!string.IsNullOrEmpty(imagePath))
            {
                byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);

                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(imageBytes);

                userImage.texture = texture;
            }
        } catch
        {
            Debug.LogError("Something went wrong loading the selected user image. Image selected: " + userImage);
        }
    }

    public void selectShipImage()
    {
        try
        {
            string imagePath = UnityEditor.EditorUtility.OpenFilePanel("Select a new ship image", "", "png,jpg,jpeg,gif,bmp");

            if (!string.IsNullOrEmpty(imagePath))
            {
                byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);

                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(imageBytes);

                shipImage.texture = texture;
            }
        }
        catch
        {
            Debug.LogError("Something went wrong loading the selected ship image. Image selected: " + shipImage);
        }
    }
}
