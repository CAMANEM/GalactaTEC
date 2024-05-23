using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using GameManager;

namespace UserManager
{
    [System.Serializable]
    public class User
    {
        public string name;
        public string email;
        public string username;
        public string password;
        public string userImage;
        public int ship;
        public int[] scoreRecord;
        public string[] favoriteSoundtracks;
    }

    [System.Serializable]
    public class Users : IEnumerable<User>
    {
        public List<User> users;
        public int cuantity;

        public IEnumerator<User> GetEnumerator()
        {
            return users.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class userManager : MonoBehaviour
    {
        private static userManager instance;

        public static userManager getInstance()
        {
            if (instance == null)
            {
                // If there is no instance in the scene, check to see if it already exists
                instance = FindObjectOfType<userManager>();
                // If it does not exist, create a new GameManager object in the scene
                if (instance == null)
                {
                    GameObject obj = new GameObject("UserManager");
                    instance = obj.AddComponent<userManager>();
                }
            }
            return instance;
        }

        private void Awake()
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
        }

        public string usersPath = Application.dataPath + "/Data/users.json";

        public User getUserByEmail(string email)
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

        public User getUserByUsername(string username)
        {
            username = "CAMANEM";
            string usersJSON = File.ReadAllText(gameManager.getInstance().usersPath);

            Users users = JsonUtility.FromJson<Users>(usersJSON);

            User foundUser = null;

            foreach (User user in users.users)
            {
                if (user.username == username)
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

        public List<User> getSignedUsers()
        {
            string usersJSON = File.ReadAllText(usersPath);

            Users users = JsonUtility.FromJson<Users>(usersJSON);

            return users.users;
        }
    }
}

