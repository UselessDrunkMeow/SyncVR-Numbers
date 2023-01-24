using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberConector : MonoBehaviour
{
    public int _WhatNumber;
    public TextMeshProUGUI _Number;
    void Start()
    {
        _Number.text = _WhatNumber.ToString();
    }
 
    void Update()
    {

    }
}
