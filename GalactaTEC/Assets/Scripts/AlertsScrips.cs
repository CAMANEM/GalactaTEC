using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace alerts_manager
{
    public class AlertsManager : MonoBehaviour
    {
        [SerializeField] GameObject pnlAlert;
        [SerializeField] GameObject sceneLoader;
        [SerializeField] TextMeshProUGUI txtAlertTitle;
        [SerializeField] TextMeshProUGUI txtAlertBody;
        [SerializeField] Button btnCloseAlert;
        [SerializeField] TextMeshProUGUI txtCloseAlert;

        // Start is called before the first frame update
        void Start()
        {

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
                    GameObject obj = new GameObject("dialogueManager");
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

        public void test()
        {
            this.txtAlertTitle.text = "Test title";
            this.txtAlertBody.text = "Test body";
            this.txtCloseAlert.text = "Close";
        }

        public void showAlert()
        {
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
