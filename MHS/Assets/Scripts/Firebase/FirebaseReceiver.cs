using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseReceiver : MonoBehaviour
{
    DatabaseReference m_Reference;
    public InputField inputField;
    public Text dataText;
    string data;

    // Start is called before the first frame update
    void Start()
    {
        m_Reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void GetBtn()
    {
        string code = inputField.text;
        ReceiveData(code);
        Debug.Log(data);
    }

    public void ReceiveData(string code)
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
                    data = (string)snapshot.Child(code).Value;
                    dataText.text = data;
                }
            });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
