using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckUserCode : MonoBehaviour
{
    public InputField input;
    public GameObject loadingObject;
    public Text resultText;
    public Button enterBtn;
    private bool loading;
    private TouchScreenKeyboard keyboard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnInputFieldSelected()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    public void InputCode()
    {
        string code = input.text;
        FirebaseReceiver.CallReceiveUserCode(code);
        loading = true;
        input.interactable = false;
        enterBtn.interactable = false;
        loadingObject.SetActive(true);
        resultText.text = "�ڵ� Ȯ�����Դϴ�. ��ø� ��ٷ��ּ���.";
    }

    public void FinishLoading()
    {
        loadingObject.SetActive(false);
        if (FirebaseReceiver.userExists == 1)
        {
            PlayerPrefs.SetString("vrUserCode", input.text);
            SceneManager.LoadScene("JapanMap");
        }
        else if (FirebaseReceiver.userExists == -1)
        {
            input.interactable = true;
            enterBtn.interactable = true;
            input.text = "";
            resultText.text = "�߸��� �ڵ��Դϴ�. �ٽ� �Է����ּ���.";
        }
        FirebaseReceiver.userExists = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(loading)
        {
            if (FirebaseReceiver.userExists != 0)
            {
                FinishLoading();
                loading = false;
            }
        }

        if (keyboard != null)
        {
            input.text = keyboard.text;
            keyboard = null;
        }

        if(input.isFocused)
        {
            OnInputFieldSelected();
        }
    }
}
