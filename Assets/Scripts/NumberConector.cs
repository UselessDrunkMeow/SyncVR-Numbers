using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberConector : MonoBehaviour
{
    public int _WhatNumber;
    public TextMeshProUGUI _Number;

    [SerializeField] LineRenderer _Line;

    [SerializeField] bool _CanBeSelected;
    int _AmountOfConnectedLines;

    [SerializeField] NumberConector _Conector;

    Vector2 _MousePos;
    float _Distance;

    [SerializeField] LayerMask layerMask;

    [SerializeField] bool _DrawLine;

    [SerializeField] Vector3 _CenterPoint;
    void Start()
    {
        _CenterPoint = transform.position;
        _Number.text = _WhatNumber.ToString();
        if (_WhatNumber == 1)
        {
            _CanBeSelected = true;
        }
    }

    void Update()
    {
        MousePos();
        if (_CanBeSelected && _DrawLine)
        {
            DrawLine();
        }
    }

    private void OnMouseDown()
    {
        if (_CanBeSelected)
        {
            _DrawLine = true;
        }
        //gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }
    private void OnMouseUp()
    {
        if (_CanBeSelected)
        {
            RaycastHit2D hit = Physics2D.Raycast(_MousePos, Vector3.forward, 5, layerMask);            
            if (hit.collider)
            {
                _Conector = hit.transform.GetComponent<NumberConector>();
            }

            if( _Conector == null)
            {
                print("not correct :(");
                _DrawLine = false;
                _Line.enabled = false;
            }

            if (_Conector._WhatNumber == _WhatNumber + 1)
            {
                print("Correct!");
                _DrawLine = false;
                _CanBeSelected = false;
                _Conector._CanBeSelected = true;
            }
            else
            {
                _Conector = null;
                print("not correct :(");
                _DrawLine = false;
                _Line.enabled = false;
            }

        }      
    }
    void DrawLine()
    {
        _Line.enabled = true;
        _Line.SetPosition(0, _CenterPoint);
        _Line.SetPosition(1, _MousePos);
    }
    void MousePos()
    {
        _MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    void CheckCorrectLocation()
    {

    }
}
