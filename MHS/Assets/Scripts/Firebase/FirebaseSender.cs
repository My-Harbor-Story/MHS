using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseSender : MonoBehaviour
{
    private static DatabaseReference m_Reference;
    public InputField inputField;
    public Text codeText;
    private static string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    // Start is called before the first frame update
    void Start()
    {
        m_Reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void CodeBtn()
    {
        string code = GenerateCode(6);
        codeText.text = code;
        SendData(code);
    }

    public static string GenerateCode(int length)
    {
        char[] randomChars = new char[length];
        for (int i = 0; i < length; i++)
        {
            int randomIdx = Random.Range(0, chars.Length);
            randomChars[i] = chars[randomIdx];
        }
        return new string(randomChars);
    }

    public void SendData(string code)
    {
        string data = inputField.text;
        m_Reference.Child("users").Child(code).SetValueAsync(data);
    }

    public static void SendWeatherData(int idx, int weatherCode, int temp)
    {
        m_Reference = FirebaseDatabase.DefaultInstance.RootReference;
        string userCode = PlayerPrefs.GetString("userCode");
        m_Reference.Child("users").Child(userCode).Child(idx.ToString()).Child("Weather").SetValueAsync(weatherCode);
        m_Reference.Child("users").Child(userCode).Child(idx.ToString()).Child("temp").SetValueAsync(temp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
