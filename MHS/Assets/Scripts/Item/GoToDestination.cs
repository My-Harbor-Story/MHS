using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToDestination : MonoBehaviour
{
    public void GoToDestinationScene()
    {
        SceneManager.LoadScene("Destination");
    }
}
