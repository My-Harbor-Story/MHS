using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Text codeText;
    // Start is called before the first frame update
    void Start()
    {
        string code = PlayerPrefs.GetString("userCode");
        codeText.text = code;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
