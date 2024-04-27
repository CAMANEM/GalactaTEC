using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using GameManager;

namespace alerts_manager
{
    public class AlertsManager : MonoBehaviour
    {
        [SerializeField] GameObject pnlAlert;
        [SerializeField] TextMeshProUGUI txtAlertTitle;
        [SerializeField] TextMeshProUGUI txtAlertBody;
        [SerializeField] Button btnCloseAlert;
        [SerializeField] TextMeshProUGUI txtCloseAlert;

        // Start is called before the first frame update
        void Start()
        {
            pnlAlert.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private static AlertsManager instance;

        public static AlertsManager getInstance()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AlertsManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("alerts_manager");
                    instance = obj.AddComponent<AlertsManager>();
                }
            }
            return instance;
        }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            btnCloseAlert.onClick.RemoveAllListeners();
            btnCloseAlert.onClick.AddListener(hideAlert);
        }

        public void showAlert(string alertTitle, string alertBody, string closeAlertText)
        {
            this.txtAlertTitle.text = alertTitle;
            this.txtAlertBody.text = alertBody;
            this.txtCloseAlert.text = closeAlertText;
            pnlAlert.SetActive(true);
        }

        public void hideAlert()
        {
            pnlAlert.SetActive(false);
            this.txtAlertTitle.text = "";
            this.txtAlertBody.text = "";
            this.txtCloseAlert.text = "";
        }
    }
}
