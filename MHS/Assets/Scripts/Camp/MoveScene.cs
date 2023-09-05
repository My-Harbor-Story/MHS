using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
        SceneManager.LoadScene("ManageItem");
    }

    public void MoveSmartScene()
    {
        SceneManager.LoadScene("SmartLighthouse");
    }

    public void MoveShipScene()
    {
        SceneManager.LoadScene("Ship");
    }
}
