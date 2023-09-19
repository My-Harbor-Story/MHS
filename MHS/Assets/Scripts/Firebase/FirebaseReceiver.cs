using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseReceiver : MonoBehaviour
{
    private static DatabaseReference m_Reference;
    public static WeatherDataFB[] weatherData = new WeatherDataFB[24];
    public static int userExists = 0; //0 : default, -1 : None, 1 : Exists
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

    //public static void ReceiveWeatherData(string code, int idx)
    //{
    //    m_Reference = FirebaseDatabase.DefaultInstance.RootReference;
    //    m_Reference.Child("users").Child(code).Child(idx.ToString()).GetValueAsync().ContinueWith(task =>
    //        {
    //            if (task.IsFaulted)
    //            {
    //                Debug.Log("Error");
    //            }
    //            else if (task.IsCompleted)
    //            {
    //                DataSnapshot snapshot = task.Result;
    //                foreach (var data in snapshot.Children)
    //                {
    //                    Debug.Log(data.Key + " " + data.Value);
    //                    if (data.Key == "temp") weatherData[idx].temp = (int)data.Value;
    //                }
    //            }
    //        });
    //}

    public static async void CallReceiveWeatherData(string code, int idx)
    {
        await ReceiveWeatherData(code, idx);
    }

    public static async Task ReceiveWeatherData(string code, int idx)
    {
        m_Reference = FirebaseDatabase.DefaultInstance.RootReference;
        DataSnapshot snapshot = await m_Reference.Child("users").Child(code).Child(idx.ToString()).GetValueAsync();

        if (snapshot.Exists)
        {
            foreach (var data in snapshot.Children)
            {
                Debug.Log(data.Key + ", " + data.Value);
                if (data.Key == "temp") weatherData[idx].temp = int.Parse(data.Value.ToString());
                if (data.Key == "Weather") weatherData[idx].code = int.Parse(data.Value.ToString());
            }
        }
    }

    public static async void CallReceiveUserCode(string code)
    {
        await ReceiveUserCode(code);
    }

    public static async Task ReceiveUserCode(string code)
    {
        m_Reference = FirebaseDatabase.DefaultInstance.RootReference;
        DataSnapshot snapshot = await m_Reference.Child("users").Child(code).GetValueAsync();

        if (snapshot.Exists)
        {
            userExists = 1;
        }
        else
        {
            userExists = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
