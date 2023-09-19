using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WeatherMapApi : MonoBehaviour
{
    public Image[] weatherImage = new Image[24];
    public WeatherDataFB[] weatherData = new WeatherDataFB[24];
    public Sprite Rain;
    public Sprite Wind;
    public Sprite Both;
    private WeatherData weatherInfo;
    private string apiKey = "5bdccf9869de90e5f15337311bbdf4b9";

    // Start is called before the first frame update
    void Start()
    {
        float[] latitudes = LocationData.JapanLatitudes;
        float[] longitudes = LocationData.JapanLongitudes;
        for (int i = 0; i < latitudes.Length; i++)
        {
            CheckCityWeather(latitudes[i], longitudes[i], i);
        }
    }

    public void CheckCityWeather(float lat, float lon, int idx)
    {
        StartCoroutine(GetWeather(lat, lon, idx));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetWeather(float lat, float lon, int idx)
    {
        string url = "https://api.openweathermap.org/data/2.5/weather?units=metric&lat=" + lat + "&lon=" + lon + "&appid=" + apiKey;
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
            json = json.Replace("\"base\":", "\"basem\":");
            weatherInfo = JsonUtility.FromJson<WeatherData>(json);
            if (weatherInfo.weather.Length > 0)
            {
                //Debug.Log(weatherInfo.weather[0].main); //Rain, Snow, Clouds, Clear etc
                if (weatherInfo.weather[0].main == "Rain" && weatherInfo.wind.speed >= 5)
                {
                    weatherImage[idx].enabled = true;
                    weatherImage[idx].sprite = Both;
                    weatherData[idx].code = 3;
                }
                else if (weatherInfo.weather[0].main == "Rain")
                {
                    weatherImage[idx].enabled = true;
                    weatherImage[idx].sprite = Rain;
                    weatherData[idx].code = 1;
                }
                else if (weatherInfo.wind.speed >= 5)
                {
                    weatherImage[idx].enabled = true;
                    weatherImage[idx].sprite = Wind;
                    weatherData[idx].code = 2;
                }
                else
                {
                    weatherData[idx].code = 0;
                }
                weatherData[idx].temp = weatherInfo.main.temp;

                Debug.Log(idx + ", " + weatherData[idx].code + ", " + weatherData[idx].temp);
                FirebaseSender.SendWeatherData(idx, weatherData[idx].code, weatherData[idx].temp);
            }
        }
    }
}