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
        if(step == 1) // ���� �˸�â ���� �ʿ�
        {
            //FB���� PlayerPref userCode �ش��ϴ� �� ����
            //PlayerPrefs.SetString("userCode", null); //������ ����� ���� -> ���� ���� �� �ʱ�ȭ
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
