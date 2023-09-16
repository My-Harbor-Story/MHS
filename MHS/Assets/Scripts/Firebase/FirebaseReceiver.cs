using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseReceiver : MonoBehaviour
{
    public static DatabaseReference m_Reference;
    public InputField inputField;
    public Text dataText;

    // Start is called before the first frame update
    void Start()
    {
        m_Reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void GetBtn()
    {
        string code = inputField.text;
        string data = "";
        ReceiveData(code, receivedData =>
        {
            data = receivedData;
            dataText.text = data;
        });
    }

    public void ReceiveData(string code, System.Action<string> onDataReceived)
    {
        FirebaseDatabase.DefaultInstance.GetReference("users")
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("Error");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    string receivedData = (string)snapshot.Child(code).Value;
                    onDataReceived(receivedData);
                }
            });
    }

    public static void ReceiveWeatherData(string code, int idx)
    {
        m_Reference = FirebaseDatabase.DefaultInstance.RootReference;
        m_Reference.Child("users").Child(code).Child(idx.ToString()).GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("Error");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    foreach (var data in snapshot.Children)
                    {
                        Debug.Log(data.Key + " " + data.Value);
                    }
                }
            });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
