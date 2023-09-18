using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    int step;
    public GameObject[] stepObject = new GameObject[3];
    // Start is called before the first frame update
    void Start()
    {
        step = PlayerPrefs.GetInt("step", 0);
        for(int i=0; i<step; i++)
        {
            stepObject[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveDataLabScene()
    {
        SceneManager.LoadScene("DataLab");
    }

    public void MoveManageItemScene()
    {
        if(step == 1) // 추후 알림창 생성 필요
        {
            //FB에서 PlayerPref userCode 해당하는 값 삭제
            //PlayerPrefs.SetString("userCode", null); //데이터 덮어쓰기 가능 -> 게임 종료 시 초기화
            PlayerPrefs.SetInt("code", 0);
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene("ManageItem");
    }

    public void MoveSmartScene()
    {
        SceneManager.LoadScene("SmartLighthouse");
    }

    public void MoveShipScene()
    {
        SceneManager.LoadScene("Ship_Notice");
    }


    public void MoveCampScene()
    {
        SceneManager.LoadScene("Camp");
    }
}
