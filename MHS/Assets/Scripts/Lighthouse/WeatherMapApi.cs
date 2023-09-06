using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WeatherMapApi : MonoBehaviour
{
    public Text weatherText;
    public WeatherData weatherInfo;
    private string apiKey = "5bdccf9869de90e5f15337311bbdf4b9";

    // Start is called before the first frame update
    void Start()
    {
        CheckCityWeather(37.5f, 126.9f);
    }

    public void CheckCityWeather(float lat, float lon)
    {
        StartCoroutine(GetWeather(lat, lon));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GetWeather(float lat, float lon)
    {
        string url = "https://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lon + "&appid={API key}" + apiKey;

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            string json = www.downloadHandler.text;
            Debug.Log(json);
            json = json.Replace("\"base\":", "\"basem\":");
            weatherInfo = JsonUtility.FromJson<WeatherData>(json);
            if (weatherInfo.weather.Length > 0)
            {
                weatherText.text = weatherInfo.weather[0].main;
            }
        }
    }
}