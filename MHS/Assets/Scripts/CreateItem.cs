using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Delivery
{
    public string country;
    public string data;
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

    // Start is called before the first frame update
    void Start()
    {
        List<Delivery> selectedData = GetRandomSelection(3);
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
            delivery.country = countryList[randomCountryIndex];
            delivery.data = tempList[randomDataIndex];
            tempList.RemoveAt(randomDataIndex);
            result.Add(delivery);
        }
        return result;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
