using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace alertsManager
{
    public class Alert
    {
        public string alertTitle = "Alert Title";
        public string alertBody = "Alert Body";

    }

    public class alertsScript : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        [SerializeField] GameObject alertCanvas;
        [SerializeField] TMP_Text alertTitleText;
        [SerializeField] TMP_Text alertBodyText;
        [SerializeField] Button alertCloseButton;

        Alert alert = new Alert();

        private static alertsScript instance;

        public static alertsScript getInstance()
        {
            if(instance == null)
            {
                instance = FindObjectOfType<alertsScript>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("alertsManager");
                    instance = obj.AddComponent<alertsScript>();
                }
            }
            return instance;
        }
        
        void Awake()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            alertCloseButton.onClick.RemoveAllListeners();
            alertCloseButton.onClick.AddListener(hideAlert);
        }

        public alertsScript setTitle(string alertTitle)
        {
            this.alert.alertTitle = alertTitle;
            return instance;
        }

        public alertsScript setBody(string alertBody)
        {
            this.alert.alertBody = alertBody;
            return instance;
        }

        public void showAlert()
        {
            this.alertTitleText.text = this.alert.alertTitle;
            this.alertBodyText.text = this.alert.alertBody;

            this.alert.alertTitle = "";
            this.alert.alertBody = "";

            alertCanvas.SetActive(true);
        }

        public void hideAlert()
        {
            alertCanvas.SetActive(false);

            this.alert.alertTitle = "";
            this.alert.alertBody = "";
        }
    }

}