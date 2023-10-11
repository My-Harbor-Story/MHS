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
    public static bool isPen; // ������ AI���� �Ǵ� ����

    public static void ResetData()
    {
        PlayerPrefs.SetInt("step", 0);
        PlayerPrefs.SetString("userCode", null); // �����ڵ� �ʱ�ȭ
        PlayerPrefs.SetInt("code", 0);
        PlayerPrefs.Save();

        Debug.Log("PlayerPrefs ������ �ʱ�ȭ");
    }
}
