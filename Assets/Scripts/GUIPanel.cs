using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIPanel : MonoBehaviour
{
    public static GUIPanel Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}
