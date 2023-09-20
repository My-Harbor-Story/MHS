using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NoticeBtn : MonoBehaviour
{
    public Image loadingBarImage;
    private bool isLoading = false;
    public bool isNext = false;
    public Image loadingPage;
    public Image loadingBackground;

    public Button yesBtn;
    public Button NoBtn;

    // 우선 3바퀴 돌면 넘어가게 설정
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        loadingPage.gameObject.SetActive(false);
        loadingBarImage.gameObject.SetActive(false);
        loadingBackground.gameObject.SetActive(false);

        yesBtn.gameObject.SetActive(true);
        NoBtn.gameObject.SetActive(true);

        isLoading = false;
    }

    // Update is called once per frame
    void Update()
    {
        // isLoading 변수를 모니터링하여 로딩바를 업데이트합니다.
        if (isLoading)
        {
            loadingBarImage.fillAmount += Time.deltaTime * 0.5f; // 예시: 시간에 따라 로딩바를 채우는 예제

            // 1을 넘으면 다시 0으로 초기화합니다.
            if (loadingBarImage.fillAmount >= 1.0f)
            {
                loadingBarImage.fillAmount = 0.0f;
                count++;
            }
        }
        // 로딩이 끝나고 다음 페이지로 넘어갈 수 있으면
        //else if(!isLoading && isNext)
        if(count >= 3)
        {
            SceneManager.LoadScene("DataLab");
        }
    }

    public void Btn_No()
    {
        SceneManager.LoadScene("Camp");
    }
    public void Btn_Yes()
    {
        PlayerPrefs.SetInt("step", 3);
        PlayerPrefs.Save();

        //hideBtn();
        //showLoadingPage();

        SceneManager.LoadScene("Loading");
    }

    private void showLoadingPage()
    {
        loadingPage.gameObject.SetActive(true);
        loadingBarImage.gameObject.SetActive(true);
        loadingBackground.gameObject.SetActive(true);

        isLoading = true;
    }

    private void hideBtn()
    {
        yesBtn.gameObject.SetActive(false);
        NoBtn.gameObject.SetActive(false);
    }
}
