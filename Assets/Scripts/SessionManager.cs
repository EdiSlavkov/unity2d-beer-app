using Scripts;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    private User loggedUser;
    private const string activeUser = "@activeUser";
    private static SessionManager sessionManager;

    private void Awake()
    {
        if (sessionManager != null)
        {
            Destroy(gameObject);
        }
        else
        {
            sessionManager = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public bool HasActiveUser()
    {
        return HasUser(activeUser);
    }

    public bool IsBeerInFavorites(int beerID)
    {
        return loggedUser.favorites.Contains(beerID);
    }

    public bool ToggleFavoriteBeer(int beerID)
    {
        if (IsBeerInFavorites(beerID))
        {
            loggedUser.favorites.Remove(beerID);
            return false;
        }
        if (loggedUser.favorites.Count < Utils.MaxFavoriteBeerCount)
        {
            loggedUser.favorites.Add(beerID);
            return true;
        }
        return false;
    }

    public void LoadLoggedUser()
    {
        if (loggedUser == null && HasActiveUser())
        {
            loggedUser = GetUser(activeUser);
        }
    }

    public void HandleRegistration(string username, string password)
    {
        User user = new User(username, password);
        PlayerPrefs.SetString(username, user.ToJson());
        PlayerPrefs.Save();
        loggedUser = user;
    }

    public bool HasUser(string username)
    {
        return PlayerPrefs.HasKey(username);
    }

    public User GetUser(string username)
    {
        string userJson = PlayerPrefs.GetString(username);
        if (string.IsNullOrEmpty(userJson))
        {
            return null;
        }
        return JsonUtility.FromJson<User>(userJson);
    }

    public void ChangePassword(string password)
    {
        loggedUser.password = password;
    }
     
    public void SetLoggedUser(User user)
    {
        loggedUser = user;
        PlayerPrefs.SetString(activeUser, loggedUser.ToJson());
        PlayerPrefs.Save();
    }

    public void Logout()
    {
        SaveLoggedUserData();
        PlayerPrefs.DeleteKey(activeUser);
        loggedUser = null;
    }

    public User GetLoggedUser()
    {
        return loggedUser;
    }

    public List<int> GetLoggedUserFavorites()
    {
        return loggedUser.favorites;
    }

    public void SaveLoggedUserData()
    {
        if (loggedUser != null)
        {
            string loggedUserToJson = loggedUser.ToJson();
            PlayerPrefs.SetString(loggedUser.username, loggedUserToJson);
            PlayerPrefs.SetString(activeUser, loggedUserToJson);
            PlayerPrefs.Save();
        }
    }

    private void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            SaveLoggedUserData();
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SaveLoggedUserData();
        }
    }

    private void OnApplicationQuit()
    {
        SaveLoggedUserData();
    }
}