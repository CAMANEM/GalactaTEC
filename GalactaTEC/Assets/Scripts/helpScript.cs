using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace helpManager
{
    public class helpScript : MonoBehaviour
    {
        [SerializeField] GameObject pnlHelp;
        [SerializeField] Button btnCloseHelp;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private static helpScript instance;

        public static helpScript getInstance()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<helpScript>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("helpManager");
                    instance = obj.AddComponent<helpScript>();
                }
            }
            return instance;
        }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            btnCloseHelp.onClick.RemoveAllListeners();
            btnCloseHelp.onClick.AddListener(HideDialogue);
        }

        public void ShowDialogue()
        {
            pnlHelp.SetActive(true);
        }

        public void HideDialogue()
        {
            pnlHelp.SetActive(false);
        }
    }
}
