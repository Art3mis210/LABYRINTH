using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using Firebase.Extensions;
using UnityEngine.UI;

public class FirebaseManager : MonoBehaviour
{
    private Firebase.FirebaseApp app;
    private string email;
    private string password;
    public Text RegisterWarning;
    public Text LoginWarning;

    // Start is called before the first frame update
    void Awake()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

    }
    private void Start()
    {
        if (app != null)
        {        // Log an event with no parameters.
            Firebase.Analytics.FirebaseAnalytics
              .LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLogin);

            // Log an event with a float parameter
            Firebase.Analytics.FirebaseAnalytics
              .LogEvent("progress", "percent", 0.4f);

            // Log an event with an int parameter.
            Firebase.Analytics.FirebaseAnalytics
              .LogEvent(
                Firebase.Analytics.FirebaseAnalytics.EventPostScore,
                Firebase.Analytics.FirebaseAnalytics.ParameterScore,
                42
              );

            // Log an event with a string parameter.
            Firebase.Analytics.FirebaseAnalytics
              .LogEvent(
                Firebase.Analytics.FirebaseAnalytics.EventJoinGroup,
                Firebase.Analytics.FirebaseAnalytics.ParameterGroupId,
                "spoon_welders"
              );

            // Log an event with multiple parameters, passed as a struct:
            Firebase.Analytics.Parameter[] LevelUpParameters = {
          new Firebase.Analytics.Parameter(
            Firebase.Analytics.FirebaseAnalytics.ParameterLevel, 5),
          new Firebase.Analytics.Parameter(
            Firebase.Analytics.FirebaseAnalytics.ParameterCharacter, "mrspoon"),
          new Firebase.Analytics.Parameter(
            "hit_accuracy", 3.14f)
        };
            Firebase.Analytics.FirebaseAnalytics.LogEvent(
              Firebase.Analytics.FirebaseAnalytics.EventLevelUp,
              LevelUpParameters);
        }
        if (app == null)
            return;
        //RegisterUser();
    }
    public void RegisterUser()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        var Register=auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            /*   if (AuthError.MissingPassword.CompareTo(password)
               {
                   RegisterWarning.text = "Invalid Email";
                   RegisterWarning.transform.gameObject.SetActive(true);
               }*/

            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                RegisterWarning.text = task.Exception.GetBaseException().Message;
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                RegisterWarning.text = task.Exception.GetBaseException().Message;
                return;
            }
            // Firebase user has been created.
            RegisterWarning.text = "";
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
            newUser.DisplayName, newUser.UserId);
            SceneManager.LoadScene("MainScene");
        });
        
    }
    public void LoginUser()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                LoginWarning.text = task.Exception.GetBaseException().Message;
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                LoginWarning.text = task.Exception.GetBaseException().Message;
                return;
            }
            LoginWarning.text = "";
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            SceneManager.LoadScene("MainScene");

        });
    }
    public void GoogleSignIn()
    {

    }
    public void OnEmailChanged(string newEmail)
    {
        email = newEmail;
    }
    public void OnPasswordChanged(string newPassword)
    {
        password = newPassword;
    }

}
