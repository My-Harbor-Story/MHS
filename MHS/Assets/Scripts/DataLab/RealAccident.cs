using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

// 실제 사고 정보 관련 스크립트
public class RealAccident : MonoBehaviour
{
    [SerializeField] private TMP_Text[] RealText;

    List<Dictionary<string, object>> data;

    //게임에서 사고난 태그 (우선 테스트를 위해 여기에 선언)
    private int[] gameTags = new int[] {1, 2, 3 };

    // 실제 사고 사례 기사 제목과 태그 클래스
    public class CsvData
    {
        public string Title;
        public int[] Tags;
    }

    // 실제 사고 사례 기사 정보가 담길 리스트
    List<CsvData> dataList = new List<CsvData>();

    // Start is called before the first frame update
    void Start()
    {
        data = CSVReader.Read("RealAccident");
        LoadRealAccident();
        //PrintRealAccident();

        // 유사도에 따라 상위 3개 제목 출력
        List<string> similarTitles = FindSimilarTitles(gameTags, 3);

        int index = 0;
        foreach (string title in similarTitles)
        {
            RealText[index].text = title;
            index++;
            Debug.Log("Similar Title: " + title);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 실제 사고 정보가 담긴 csv 파일에서 제목과 태그 정보 클래스 리스트에 저장
    void LoadRealAccident()
    {
        
        int dataCount = int.Parse(data[0]["num"].ToString());
        Debug.Log(dataCount);

        for (int i = 1; i <= dataCount; i++)
        {
            CsvData csvData = new CsvData();

            csvData.Title = data[i]["Title"].ToString();
            int tagsNum = int.Parse(data[i]["TagsNum"].ToString());
            csvData.Tags = new int[tagsNum];

            for (int j = 0; j < tagsNum; j++)
            {
                string columnName = $"Tags{j}";
                csvData.Tags[j] = int.Parse(data[i][columnName].ToString());
            }

            dataList.Add(csvData);
        }
    }

    // 테스트용 출력 함수
    void PrintRealAccident()
    {
        foreach (CsvData data in dataList)
        {
            Debug.Log("Title: " + data.Title);

            if (data.Tags != null && data.Tags.Length > 0)
            {
                Debug.Log("Tags: " + string.Join(", ", data.Tags));
            }
            else
            {
                Debug.Log("No tags available.");
            }
        }
    }


    // 코사인 유사도 계산
    private double CalculateCosineSimilarity(int[] tagsA, int[] tagsB)
    {
        double dotProduct = 0.0;
        double magnitudeA = 0.0;
        double magnitudeB = 0.0;

        for (int i = 0; i < tagsA.Length; i++)
        {
            dotProduct += tagsA[i] * tagsB[i];
            magnitudeA += tagsA[i] * tagsA[i];
            magnitudeB += tagsB[i] * tagsB[i];
        }

        magnitudeA = Math.Sqrt(magnitudeA);
        magnitudeB = Math.Sqrt(magnitudeB);

        if (magnitudeA == 0.0 || magnitudeB == 0.0)
        {
            return 0.0; // 분모가 0인 경우 유사도는 0입니다.
        }
        else
        {
            return dotProduct / (magnitudeA * magnitudeB);
        }
    }

    // 유사한 태그를 가진 데이터의 제목을 찾아서 반환 (상위 n개)
    private List<string> FindSimilarTitles(int[] userTags, int topN)
    {
        // 데이터와 유사도를 저장할 리스트 생성
        List<Tuple<string, double>> similarityList = new List<Tuple<string, double>>();

        foreach (CsvData data in dataList)
        {
            // 배열 길이 수정
            // 길이 관련때문에 나오는 유사도 오류는 추후에 수정할 예정
            int[] dataTags = data.Tags;
            int maxLength = Math.Max(userTags.Length, dataTags.Length);
            Array.Resize(ref userTags, userTags.Length);
            Array.Resize(ref dataTags, userTags.Length);

            double similarity = CalculateCosineSimilarity(userTags, dataTags);
            //Debug.Log(data.Title + " "+ similarity);
            similarityList.Add(new Tuple<string, double>(data.Title, similarity));
        }

        // 유사도에 따라 내림차순으로 정렬
        List<string> similarTitles = similarityList
            .OrderByDescending(t => t.Item2) // 유사도 내림차순 정렬
            .Take(topN) // 상위 N개 아이템 선택
            .Select(t => t.Item1) // 제목만 선택
            .ToList();

        return similarTitles;
    }
}
