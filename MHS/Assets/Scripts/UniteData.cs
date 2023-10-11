#define RELEASE_D
using System.Collections.Generic;
using UnityEngine;

public class UniteData
{
    public static string orderList;
    public static int releaseNum;
    public static int step;
    public static string userCode;
    public static string vrUserCode;
    public static bool isPen; // 펜인지 AI인지 판단 변수

    public static void ResetData()
    {
        PlayerPrefs.SetInt("step", 0);
        PlayerPrefs.SetString("userCode", null); // 유저코드 초기화
        PlayerPrefs.SetInt("code", 0);
        PlayerPrefs.Save();

        Debug.Log("PlayerPrefs 데이터 초기화");
    }
}
