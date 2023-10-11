using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LighthouseUI : MonoBehaviour
{
    public Sprite[] clickBtnSprite;
    public Sprite[] unclickBtnSprite;
    public Sprite[] flagSprite;
    public Button[] btn; //일본 중국 태국
    public GameObject flag;

    public GameObject Notice3_Tablet;
    public GameObject Notice4_Tablet;

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<btn.Length; i++)
        {
            int buttonIdx = i;
            btn[i].onClick.AddListener(() => ClickButton(buttonIdx));
        }

        Notice3_Tablet.SetActive(false);
        Notice4_Tablet.SetActive(false);
    }

    public void ClickButton(int num)
    {
        for(int i=0; i<btn.Length; i++)
        {
            btn[i].GetComponent<Image>().sprite = unclickBtnSprite[i];
        }
        btn[num].GetComponent<Image>().sprite = clickBtnSprite[num];
        flag.GetComponent<Image>().sprite = flagSprite[num];
    }

    public void CloseBtn()
    {
        Notice4_Tablet.SetActive(true);
        UniteData.isPen = false;
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("Camp");
    }

    public void CloseNotice4()
    {
        Notice4_Tablet.SetActive(false);
        UniteData.isPen = true;
    }

    public void ShowNotice3()
    {
        Notice3_Tablet.SetActive(true);
        UniteData.isPen = false;
    }

    public void GoToShipNoticeSave()
    {
        PlayerPrefs.SetInt("step", 2);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Camp");

        //SceneManager.LoadScene("Ship_Notice");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
