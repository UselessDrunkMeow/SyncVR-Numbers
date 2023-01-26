using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberConector : MonoBehaviour
{
    public int _WhatNumber;//what number the text object needs to display
    public TextMeshProUGUI _Number;//The text object on the object

    public GameObject _Particles;

    [SerializeField] LineRenderer _Line;
    [SerializeField] NumberConector _Conector;//a refrence to the other conectors

    bool _CanBeSelected;//bool for if the object can be selected or not
    bool _DrawLine;//bool for if the linerenderer needs to stay on or not, and follow the cursor or not

    [SerializeField] LayerMask layerMask;

    [SerializeField] Vector3 _CenterPoint;//center point of this object
    [SerializeField] Quaternion _Rotation;

    Vector2 _MousePos;
    void Start()
    {
        _CenterPoint = transform.position;
        _Number.text = _WhatNumber.ToString();

        if (_WhatNumber == 1)//only enables the first object to be interacteble
        {
            _CanBeSelected = true;
        }
    }

    void Update()
    {
        MousePos();
        if (_CanBeSelected && _DrawLine)//draws the line to the curser of allowd
        {
            DrawLine();
        }        
        if(_CanBeSelected &&_WhatNumber == GameManager.Instance._Connectors.Length)//check if the current objects nuber is the same as the lenght of the array. if so, end the game
        {
            _CanBeSelected = false;
            GameManager.Instance.StartCoroutine("Win");
        }
    }

    private void OnMouseDown()
    {
        if (_CanBeSelected)//if the object can be sellected and the mouse is pressed, start drawing the line
        {
            _DrawLine = true;
        }       
    }
    private void OnMouseUp()
    {
        if (_CanBeSelected)
        {
            RaycastHit2D hit = Physics2D.Raycast(_MousePos, Vector3.forward, 5, layerMask);   //raycast that checks if it hits a connector         
            if (hit.collider)//if the raycast hits a colider, get a refrence to NumberConector and asign it to _Connector
            {
                _Conector = hit.transform.GetComponent<NumberConector>();
            }

            if( _Conector == null)//if no connector has been hit, disable the line.
            {
                print("not correct :(");
                _DrawLine = false;
                _Line.enabled = false;
            }

            if (_Conector._WhatNumber == _WhatNumber + 1)//if the raycast hits an connector, and the number of the connector is the same as this connectors number +1, enalbe the other connector
            {
                print("Correct!");
                _DrawLine = false;
                _CanBeSelected = false;
                _Conector._CanBeSelected = true;
                _Conector.Particels();
            }
            else//else if the number is not correct, disable the line.
            {
                _Conector = null;
                print("not correct :(");
                _DrawLine = false;
                _Line.enabled = false;
            }
        }      
    }
    void DrawLine()//Ennables the line and set one of its points to the mouse
    {
        _Line.enabled = true;
        _Line.SetPosition(0, _CenterPoint);
        _Line.SetPosition(1, _MousePos);
    }
    void MousePos()//Mouse tracking
    {
        _MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }    
    public void Particels()//Spawns the particles with the same size as the connector
    {
       GameObject particles= Instantiate(_Particles, _CenterPoint, _Rotation);
        particles.transform.localScale = transform.localScale;
    }
}
