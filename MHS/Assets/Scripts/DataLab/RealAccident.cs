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
            string str = data[i]["NewsPaper"].ToString();
            str = str + " ";
            str = str + data[i]["Title"].ToString();
            RealText[i].text = str;
            Debug.Log(data[i]["Contents"].ToString());
            Debug.Log(str);
        }
    }
}
