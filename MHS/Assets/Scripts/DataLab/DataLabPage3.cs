using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

// 3페이지에 들어가는 정보 로드하는 함수
// Canvas-Tablet-Page3에 적용되어 있음
public class DataLabPage3 : MonoBehaviour
{
    [SerializeField] private TMP_Text DateText;
    [SerializeField] private TMP_Text CodeNumText;
    [SerializeField] private TMP_Text AccidentCountText;

    public int gameAccident; // 게임에서 발생한 사고 건수

    // Start is called before the first frame update
    void Start()
    {
        string codeData = PlayerPrefs.GetString("userCode", "");
        DateText.text = "항해 날짜 " + DateTime.Now.ToString("yyyy / MM / dd");
        CodeNumText.text = "코드 번호 " + codeData;
        AccidentCountText.text = "사고 건수 " + gameAccident;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
