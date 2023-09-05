using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    // Start is called before the first frame update
    void Start()
    {
        string data = PlayerPrefs.GetString("orderList");
        string[] loadedOrderList = LoadStringArray("orderList");
        List<Delivery> createData = GetRandomSelection(5 - loadedOrderList.Length);
        SaveStringArray("orderList", loadedOrderList, createData);

        string[] loadedOrderList2 = LoadStringArray("orderList");
        for(int i=0; i<orderText.Length; i++)
        {
            orderText[i].text = loadedOrderList2[i + 1];
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
