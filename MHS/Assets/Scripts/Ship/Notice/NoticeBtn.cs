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

    public Button yesBtn;
    public Button NoBtn;

    // Start is called before the first frame update
    void Start()
    {
        loadingPage.gameObject.SetActive(false);
        loadingBarImage.gameObject.SetActive(false);

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
            }
        }
        // �ε��� ������ ���� �������� �Ѿ �� ������
        else if(!isLoading && isNext)
        {
            SceneManager.LoadScene("DataLab");
        }
    }

    public void Btn_No()
    {
        SceneManager.LoadScene("SmartLighthouse");
    }
    public void Btn_Yes()
    {
        PlayerPrefs.SetInt("step", 3);
        PlayerPrefs.Save();
        showLoadingPage();
        hideBtn();
    }

    private void showLoadingPage()
    {
        loadingPage.gameObject.SetActive(true);
        loadingBarImage.gameObject.SetActive(true);

        isLoading = true;
    }

    private void hideBtn()
    {
        yesBtn.gameObject.SetActive(false);
        NoBtn.gameObject.SetActive(false);
    }
}
