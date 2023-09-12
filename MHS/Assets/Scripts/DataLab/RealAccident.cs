using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RealAccident : MonoBehaviour
{
    [SerializeField] private TMP_Text[] RealText;

    List<Dictionary<string, object>> data;
    // Start is called before the first frame update
    void Start()
    {
        data = CSVReader.Read("RealAccident");
        ShowRealAccident();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowRealAccident()
    {
        
        for(int i = 0; i < RealText.Length; i++)
        {
            string str =  data[i]["Title"].ToString();
            RealText[i].text = str;
            int tagsNum = int.Parse(data[i]["TagsNum"].ToString());
            int[] tagNums = new int[tagsNum];

            for (int j = 0; j < tagsNum; j++)
            {
                string columnName = $"Tags{j}";
                tagNums[j] = int.Parse(data[i][columnName].ToString());
                str = str + tagNums[j] + " ";
            }
            Debug.Log(str);
        }
    }
}
