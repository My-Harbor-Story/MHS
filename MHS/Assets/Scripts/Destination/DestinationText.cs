using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestinationText : MonoBehaviour
{
    private int releaseNum;
    private string[] loadedOrderList;
    public Text releaseText;

    void Start()
    {
        releaseNum = PlayerPrefs.GetInt("releaseNum");
        loadedOrderList = LoadStringArray("orderList");
        releaseText.text = loadedOrderList[releaseNum + 1];
        DeleteReleaseItem(releaseNum);
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

    void DeleteReleaseItem(int num)
    {
        string data = "";
        for(int i = 1; i < loadedOrderList.Length; i++)
        {
            if (i == num + 1) continue;
            string tmp = ","+loadedOrderList[i];
            data += tmp;
        }
        PlayerPrefs.SetInt("releaseNum", -1);
        PlayerPrefs.SetString("orderList", data);
        PlayerPrefs.Save();
    }


    void Update()
    {
        
    }
}
