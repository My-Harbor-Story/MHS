using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWeatherData : MonoBehaviour
{
    private string code;

    // Start is called before the first frame update
    void Start()
    {
        //code = PlayerPrefs.GetString("userCode");
        code = "f4Va30";
        for (int i = 0; i < 24; i++)
        {
            GetWeather(i, "temp");
        }
    }

    void GetWeather(int idx, string sep)
    {
        FirebaseReceiver.GetWeatherData(code, idx);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("�ܺ� : " + FirebaseReceiver.weatherData[22].temp);
    }
}
