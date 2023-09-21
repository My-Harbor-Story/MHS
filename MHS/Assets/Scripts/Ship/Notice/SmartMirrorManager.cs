using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartMirrorManager : MonoBehaviour
{
    public Text weatherText;
    public Text tempText;
    public Text copingText;
    public GameObject rainPrefab;
    private string pMap; //¼±¹Ú ÇöÀç ±¸¿ª

    private int[] tempData = {
        27, 27, 27, 28,
        27, 27, 27, 27,
        27, 27, 27, 27,
        28, 28, 28, 28,
        27, 26, 26, 26,
        23, 24, 24, 23
    };

    private int[] tempWeather = {
        0, 0, 0, 0,
        0, 0, 0, 0,
        0, 1, 0, 0,
        0, 0, 0, 0,
        0, 0, 1, 1,
        0, 0, 0, 0
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            string cubeNumber = collision.gameObject.name;
            //int cubeWeather = FirebaseReceiver.weatherData[int.Parse(cubeNumber)].code;
            int cubeWeather = tempWeather[int.Parse(cubeNumber)];

            rainPrefab.SetActive(false);
            if (cubeWeather == 0) weatherText.text = "³¯¾¾ : Clear";
            else if (cubeWeather == 1)
            {
                rainPrefab.SetActive(true);
                rainPrefab.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
                weatherText.text = "³¯¾¾ : Rain";
                if (pMap != cubeNumber)
                {
                    userInteraction.driveAble = false;
                    ShowCopingText(CopingText.RainWeatherText[0]);
                    Invoke("DriveAbleTrue", 4.0f);
                    Invoke("DriveAbleFalse", 8.0f);
                    Invoke("DriveAbleTrue", 9.5f);
                }
            }
            else if (cubeWeather == 2) weatherText.text = "³¯¾¾ : Wind";
            else if (cubeWeather == 3) weatherText.text = "³¯¾¾ : Rainstorm";

            //int cubeTemp = FirebaseReceiver.weatherData[int.Parse(cubeNumber)].temp;
            int cubeTemp = tempData[int.Parse(cubeNumber)];
            tempText.text = "¿Âµµ : " + cubeTemp.ToString();

            pMap = cubeNumber;
        }

        if (collision.gameObject.CompareTag("Rock"))
        {
            userInteraction.driveAble = false;
            FadeRock(collision.gameObject);
            ShowCopingText(CopingText.RockText[2]);
            Invoke("DriveAbleTrue", 4.0f);
        }
    }

    void ShowCopingText(string text)
    {
        copingText.text = text;
    }

    void FadeRock(GameObject rock)
    {
        StartCoroutine(FadeAway(rock));
    }

    IEnumerator FadeAway(GameObject rock)
    {
        yield return new WaitForSeconds(1);
        MeshRenderer meshRenderer = rock.GetComponent<MeshRenderer>();
        while (meshRenderer.material.color.a > 0)
        {
            var color = meshRenderer.material.color;
            color.a -= (.5f * Time.deltaTime);

            meshRenderer.material.color = color;
            yield return null;
        }
        Destroy(rock);
    }

    void DriveAbleTrue()
    {
        copingText.text = "";
        userInteraction.driveAble = true;
    }
    void DriveAbleFalse()
    {
        userInteraction.driveAble = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
