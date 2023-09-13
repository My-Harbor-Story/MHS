using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

// ���� ��� ���� ���� ��ũ��Ʈ
public class RealAccident : MonoBehaviour
{
    [SerializeField] private TMP_Text[] RealText;

    List<Dictionary<string, object>> data;

    //���ӿ��� ��� �±� (�켱 �׽�Ʈ�� ���� ���⿡ ����)
    private int[] gameTags = new int[] {1, 2, 3 };

    // ���� ��� ��� ��� ����� �±� Ŭ����
    public class CsvData
    {
        public string Title;
        public int[] Tags;
    }

    // ���� ��� ��� ��� ������ ��� ����Ʈ
    List<CsvData> dataList = new List<CsvData>();

    // Start is called before the first frame update
    void Start()
    {
        data = CSVReader.Read("RealAccident");
        LoadRealAccident();
        //PrintRealAccident();

        // ���絵�� ���� ���� 3�� ���� ���
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

    // ���� ��� ������ ��� csv ���Ͽ��� ����� �±� ���� Ŭ���� ����Ʈ�� ����
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

    // �׽�Ʈ�� ��� �Լ�
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


    // �ڻ��� ���絵 ���
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
            return 0.0; // �и� 0�� ��� ���絵�� 0�Դϴ�.
        }
        else
        {
            return dotProduct / (magnitudeA * magnitudeB);
        }
    }

    // ������ �±׸� ���� �������� ������ ã�Ƽ� ��ȯ (���� n��)
    private List<string> FindSimilarTitles(int[] userTags, int topN)
    {
        // �����Ϳ� ���絵�� ������ ����Ʈ ����
        List<Tuple<string, double>> similarityList = new List<Tuple<string, double>>();

        foreach (CsvData data in dataList)
        {
            // �迭 ���� ����
            // ���� ���ö����� ������ ���絵 ������ ���Ŀ� ������ ����
            int[] dataTags = data.Tags;
            int maxLength = Math.Max(userTags.Length, dataTags.Length);
            Array.Resize(ref userTags, userTags.Length);
            Array.Resize(ref dataTags, userTags.Length);

            double similarity = CalculateCosineSimilarity(userTags, dataTags);
            //Debug.Log(data.Title + " "+ similarity);
            similarityList.Add(new Tuple<string, double>(data.Title, similarity));
        }

        // ���絵�� ���� ������������ ����
        List<string> similarTitles = similarityList
            .OrderByDescending(t => t.Item2) // ���絵 �������� ����
            .Take(topN) // ���� N�� ������ ����
            .Select(t => t.Item1) // ���� ����
            .ToList();

        return similarTitles;
    }
}
