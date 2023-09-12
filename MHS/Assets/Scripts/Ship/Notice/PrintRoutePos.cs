using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintRoutePos : MonoBehaviour
{
    private List<Vector3> RoutePos;

    // Start is called before the first frame update
    void Start()
    {
        RoutePos = SaveRoutePos.RoutePos;

        for (int i = 0; i < RoutePos.Count; i++)
        {
            Debug.Log("Point " + i + ": " + RoutePos[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
