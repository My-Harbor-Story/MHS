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

    public static async void GetWeatherData(string code, int idx)
    {
        await ReceiveWeatherData(code, idx);
        Debug.Log("���� : " + weatherData[idx].temp);
    }

    public static async Task ReceiveWeatherData(string code, int idx)
    {
        m_Reference = FirebaseDatabase.DefaultInstance.RootReference;
        DataSnapshot snapshot = await m_Reference.Child("users").Child(code).Child(idx.ToString()).GetValueAsync();

        if (snapshot.Exists)
        {
            foreach (var data in snapshot.Children)
            {
                if (data.Key == "temp") FirebaseReceiver.weatherData[idx].temp = int.Parse(data.Value.ToString());
                if (data.Key == "Weather") FirebaseReceiver.weatherData[idx].code = int.Parse(data.Value.ToString());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
