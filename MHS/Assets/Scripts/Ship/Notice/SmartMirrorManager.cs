using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartMirrorManager : MonoBehaviour
{
    public Text weatherText;
    public Text tempText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            string cubeNumber = collision.gameObject.name;
            Debug.Log("Collision : " + cubeNumber);
            int cubeWeather = FirebaseReceiver.weatherData[int.Parse(cubeNumber)].code;
            if (cubeWeather == 0) weatherText.text = "Clear";
            else if (cubeWeather == 1) weatherText.text = "Rain";
            else if (cubeWeather == 2) weatherText.text = "Wind";
            else if (cubeWeather == 3) weatherText.text = "Rainstorm";

            int cubeTemp = FirebaseReceiver.weatherData[int.Parse(cubeNumber)].temp;
            tempText.text = cubeTemp.ToString();
        }

        if (collision.gameObject.CompareTag("Rock"))
        {
            Debug.Log("장애물 충돌");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
