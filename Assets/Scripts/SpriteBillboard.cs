using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpriteBillboard : MonoBehaviour
{
    public bool Active;
    public bool QuickReset;

    void OnRenderObject()
    {
        if (Active) transform.rotation = Camera.main.transform.rotation;
        else if (QuickReset)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            QuickReset = false;
        }
    }
}
