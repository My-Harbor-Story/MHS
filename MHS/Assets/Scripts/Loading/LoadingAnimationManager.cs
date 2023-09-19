using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingAnimationManager : MonoBehaviour
{
    int count = 0;
    private Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void OnEnterAnimationEnd()
    {
        count++;

        if (count == 3)
        {
            Time.timeScale = 0;
            SceneManager.LoadScene("DataLab");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
