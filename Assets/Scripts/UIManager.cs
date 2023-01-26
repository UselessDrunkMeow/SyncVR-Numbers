using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _Winscreen;     
    public void WinScreen()
    {
        _Winscreen.SetActive(true);
    }
}
