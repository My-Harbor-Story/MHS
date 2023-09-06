using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearDestination : MonoBehaviour
{
    public GameObject clearPanel;
    public void ClickClearBtn()
    {
        clearPanel.SetActive(true);
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("Camp");
    }
}
