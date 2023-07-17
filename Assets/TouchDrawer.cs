using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDrawer : MonoBehaviour
{
    public GameObject[] Pointer;
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Pointer[i].transform.position = Input.touches[i].position;
        }
        Pointer[2].transform.position = Input.mousePosition;
    }
}
