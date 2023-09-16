using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWeatherData : MonoBehaviour
{
    private string code;
    public static WeatherDataFB[] weatherData = new WeatherDataFB[24];

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
        FirebaseReceiver.CallReceiveWeatherData(code, idx);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(weatherData[22].temp);
    }
}
