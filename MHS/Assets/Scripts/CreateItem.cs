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
