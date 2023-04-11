using UnityEngine;
using System.Collections.Generic;

namespace Scripts
{
    public class User
    {
        public string username;
        public string password;
        public List<int> favorites;

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
            favorites = new List<int>();
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
