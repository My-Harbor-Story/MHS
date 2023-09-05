using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public struct Delivery
{
    public string country;
    public string data;
    public int count;
}

public class CreateItem : MonoBehaviour
{
    private List<string> countryList = new List<string>
    {
        "일본", "중국", "태국"
    };

    private List<string> dataList = new List<string>
    {
        "가평 잣", "김포 쌀", "나주 배", "여주 쌀", "이천 쌀", "의성 마늘",
        "양양 송이", "영월 고추", "정선 찰옥수수", "홍천 명이", "횡성 더덕",
        "하동 녹차", "영동 포도", "충주 사과", "고창 복분자", "광양 매실",
        "해남 고구마", "김천 포도", "상주 곶감", "성주 참외", "고려 홍삼",
        "이천 도자기", "천안 호두", "보성 녹차", "완도 김", "임실 한과",
        "장흥 표고버섯", "예천 참기름", "청도 복숭아", "경주 황남빵", "강원 고성 명태",
        "포항 과메기", "순천 딸기", "장흥 매생이", "제주 한라봉"
    };

    public Text[] orderText;
    public GameObject[] checkObject;
    public Text releaseText;
    public Sprite check, uncheck;
    public int releaseNum;

    // Start is called before the first frame update
    void Start()
    {
        releaseNum = PlayerPrefs.GetInt("releaseNum", -1);
        string[] loadedOrderList = LoadStringArray("orderList");
        List<Delivery> createData = GetRandomSelection(5 - loadedOrderList.Length);
        SaveStringArray("orderList", loadedOrderList, createData);

        string[] loadedOrderList2 = LoadStringArray("orderList");
        for(int i=0; i<orderText.Length; i++)
        {
            orderText[i].text = loadedOrderList2[i + 1];
        }

        if (releaseNum != -1)
        {
            releaseText.text = loadedOrderList2[releaseNum + 1];
            checkObject[releaseNum].GetComponent<Image>().sprite = check;
        }
    }

    List<Delivery> GetRandomSelection(int count)
    {
        List<Delivery> result = new List<Delivery>();
        List<string> tempList = new List<string>(dataList);

        for (int i = 0; i < count; i++)
        {
            Delivery delivery = new Delivery();
            int randomCountryIndex = Random.Range(0, countryList.Count);
            int randomDataIndex = Random.Range(0, tempList.Count);
            int randomCount = Random.Range(0, 19);
            delivery.country = countryList[randomCountryIndex];
            delivery.data = tempList[randomDataIndex];
            delivery.count = randomCount * 10 + 20;
            tempList.RemoveAt(randomDataIndex);
            result.Add(delivery);
        }
        return result;
    }

    string[] LoadStringArray(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string data = PlayerPrefs.GetString(key);
            return data.Split(',');
        }
        else
        {
            return new string[0];
        }
    }

    void SaveStringArray(string key, string[] loadedOrderList, List<Delivery> createData)
    {
        string data = string.Join(",", loadedOrderList);
        for(int i=0; i<createData.Count; i++)
        {
            string newData = ",[" + createData[i].country + "]" + createData[i].data + " " + createData[i].count + "개";
            data += newData;
        }
        PlayerPrefs.SetString(key, data);
        PlayerPrefs.Save();
    }

    private void GoToMain()
    {
        PlayerPrefs.SetInt("releaseNum", releaseNum);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Camp");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null)
            {
                int hitNum = -1;
                switch (hit.transform.gameObject.tag)
                {
                    case "Order1": hitNum = 0; break;
                    case "Order2": hitNum = 1; break;
                    case "Order3": hitNum = 2; break;
                    case "Order4": hitNum = 3; break;
                    case "Exit": GoToMain(); break; 
                }

                if (hitNum != -1)
                {
                    if (releaseNum == -1)
                    {
                        releaseNum = hitNum;
                        releaseText.text = orderText[hitNum].text;
                        checkObject[hitNum].GetComponent<Image>().sprite = check;
                    }
                    else
                    {
                        if (releaseNum == hitNum)
                        {
                            releaseNum = -1;
                            releaseText.text = "";
                            checkObject[hitNum].GetComponent<Image>().sprite = uncheck;
                        }
                    }
                }
            }
        }
    }
}
