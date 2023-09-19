using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartMirrorManager : MonoBehaviour
{
    public Text weatherText;
    public Text tempText;
    public GameObject rainPrefab;

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
                userInteraction.driveAble = false;
                weatherText.text = "Rain";
                rainPrefab.SetActive(true);
                rainPrefab.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
                Invoke("DriveAbleTrue", 4.0f);
            }
            else if (cubeWeather == 2) weatherText.text = "Wind";
            else if (cubeWeather == 3) weatherText.text = "Rainstorm";

            int cubeTemp = FirebaseReceiver.weatherData[int.Parse(cubeNumber)].temp;
            tempText.text = cubeTemp.ToString();
        }

        if (collision.gameObject.CompareTag("Rock"))
        {
            userInteraction.driveAble = false;
            Debug.Log("장애물 충돌");
            FadeRock(collision.gameObject);
            Invoke("DriveAbleTrue", 6.0f);
        }
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
        userInteraction.driveAble = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
