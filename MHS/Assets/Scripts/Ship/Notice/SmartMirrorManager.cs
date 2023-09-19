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
    private string pMap; //선박 현재 구역

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            string cubeNumber = collision.gameObject.name;
            int cubeWeather = FirebaseReceiver.weatherData[int.Parse(cubeNumber)].code;

            rainPrefab.SetActive(false);
            if (cubeWeather == 0) weatherText.text = "Clear";
            else if (cubeWeather == 1)
            {
                rainPrefab.SetActive(true);
                rainPrefab.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
                weatherText.text = "Rain";
                if (pMap != cubeNumber)
                {
                    userInteraction.driveAble = false;
                    ShowCopingText(CopingText.RainWeatherText[0]);
                    Invoke("DriveAbleTrue", 4.0f);
                }
            }
            else if (cubeWeather == 2) weatherText.text = "Wind";
            else if (cubeWeather == 3) weatherText.text = "Rainstorm";

            int cubeTemp = FirebaseReceiver.weatherData[int.Parse(cubeNumber)].temp;
            tempText.text = cubeTemp.ToString();

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
