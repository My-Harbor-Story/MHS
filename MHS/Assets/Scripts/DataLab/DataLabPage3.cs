using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

// 3�������� ���� ���� �ε��ϴ� �Լ�
// Canvas-Tablet-Page3�� ����Ǿ� ����
public class DataLabPage3 : MonoBehaviour
{
    [SerializeField] private TMP_Text DateText;
    [SerializeField] private TMP_Text CodeNumText;
    [SerializeField] private TMP_Text AccidentCountText;

    public int gameAccident; // ���ӿ��� �߻��� ��� �Ǽ�

    // Start is called before the first frame update
    void Start()
    {
        string codeData = PlayerPrefs.GetString("userCode", "");
        DateText.text = "���� ��¥ " + DateTime.Now.ToString("yyyy / MM / dd");
        CodeNumText.text = "�ڵ� ��ȣ " + codeData;
        AccidentCountText.text = "��� �Ǽ� " + gameAccident;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
