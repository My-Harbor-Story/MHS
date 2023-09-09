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

    // Update is called once per frame
    void Update()
    {

    }
}
