using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


using GameManager;
using UserManager;
using audio_manager;

public class podiumSceneScript : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image imgUser1;
    [SerializeField] UnityEngine.UI.Image imgUser2;

    [SerializeField] TMPro.TextMeshProUGUI txtUsername1;
    [SerializeField] TMPro.TextMeshProUGUI txtUsername2;

    [SerializeField] TMPro.TextMeshProUGUI txtScore1;
    [SerializeField] TMPro.TextMeshProUGUI txtScore2;

    // Start is called before the first frame update
    void Start()
    {
        // Remove this later
        User user1 = userManager.getInstance().getUserByUsername("andresTEC"); 
        User user2 = userManager.getInstance().getUserByUsername("MarinGE23");

        txtUsername1.text = user1.username;
        txtUsername2.text = user2.username;

        txtScore1.text = txtScore1.text + user1.scoreRecord[0].ToString();
        txtScore2.text = txtScore2.text + user2.scoreRecord[0].ToString();

        // Load player image from specified path
        if (!string.IsNullOrEmpty(user1.userImage) && File.Exists(Application.dataPath + user1.userImage))
        {
            byte[] imageData1 = File.ReadAllBytes(Application.dataPath + user1.userImage);
            Texture2D texture1 = new Texture2D(2, 2);
            texture1.LoadImage(imageData1);
            imgUser1.sprite = Sprite.Create(texture1, new Rect(0, 0, texture1.width, texture1.height), new Vector2(0.5f, 0.5f));
        }

        if (!string.IsNullOrEmpty(user2.userImage) && File.Exists(Application.dataPath + user2.userImage))
        {
            byte[] imageData2 = File.ReadAllBytes(Application.dataPath + user2.userImage);
            Texture2D texture2 = new Texture2D(2, 2);
            texture2.LoadImage(imageData2);
            imgUser2.sprite = Sprite.Create(texture2, new Rect(0, 0, texture2.width, texture2.height), new Vector2(0.5f, 0.5f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
