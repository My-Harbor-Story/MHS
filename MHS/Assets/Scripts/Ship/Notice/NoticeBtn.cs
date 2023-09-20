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

    // �켱 3���� ���� �Ѿ�� ����
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
        // isLoading ������ ����͸��Ͽ� �ε��ٸ� ������Ʈ�մϴ�.
        if (isLoading)
        {
            loadingBarImage.fillAmount += Time.deltaTime * 0.5f; // ����: �ð��� ���� �ε��ٸ� ä��� ����

            // 1�� ������ �ٽ� 0���� �ʱ�ȭ�մϴ�.
            if (loadingBarImage.fillAmount >= 1.0f)
            {
                loadingBarImage.fillAmount = 0.0f;
                count++;
            }
        }
        // �ε��� ������ ���� �������� �Ѿ �� ������
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
