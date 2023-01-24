using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberConector : MonoBehaviour
{
    public int _WhatNumber;
    public TextMeshProUGUI _Number;

    public GameObject _Particles;

    [SerializeField] LineRenderer _Line;

    bool _CanBeSelected;   
    bool _DrawLine;

    [SerializeField] NumberConector _Conector;

    Vector2 _MousePos;

    [SerializeField] LayerMask layerMask;

    [SerializeField] Vector3 _CenterPoint;
    [SerializeField] Quaternion _Rotation;
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
        if(_CanBeSelected &&_WhatNumber == GameManager.Instance._Connectors.Length)
        {
            _CanBeSelected = false;
            GameManager.Instance.StartCoroutine("Win");
        }
    }

    private void OnMouseDown()
    {
        if (_CanBeSelected)
        {
            _DrawLine = true;
        }       
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
                _Conector.Particels();


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
    public void Particels()
    {
       GameObject particles= Instantiate(_Particles, _CenterPoint, _Rotation);
        particles.transform.localScale = transform.localScale;
    }
}
