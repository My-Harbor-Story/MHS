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
        "�Ϻ�", "�߱�", "�±�"
    };

    private List<string> dataList = new List<string>
    {
        "���� ��", "���� ��", "���� ��", "���� ��", "��õ ��", "�Ǽ� ����",
        "��� ����", "���� ����", "���� ��������", "ȫõ ����", "Ⱦ�� ����",
        "�ϵ� ����", "���� ����", "���� ���", "��â ������", "���� �Ž�",
        "�س� ����", "��õ ����", "���� ����", "���� ����", "��� ȫ��",
        "��õ ���ڱ�", "õ�� ȣ��", "���� ����", "�ϵ� ��", "�ӽ� �Ѱ�",
        "���� ǥ�����", "��õ ���⸧", "û�� ������", "���� Ȳ����", "���� �� ����",
        "���� ���ޱ�", "��õ ����", "���� �Ż���", "���� �Ѷ��"
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
            string newData = ",[" + createData[i].country + "]" + createData[i].data + " " + createData[i].count + "��";
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
