using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoader : MonoBehaviour
{
    [SerializeField]
    private GameObject uiManager;
    [SerializeField]
    private bool isOpen = true;
    void Awake()
    {
        if (!isOpen)
            return;
        var gameObject = GameObject.Instantiate<GameObject>(uiManager);
        DontDestroyOnLoad(gameObject);
    }
}
