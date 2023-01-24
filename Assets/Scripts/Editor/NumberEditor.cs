using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlacePoint))]
public class NumberEditor : Editor
{
    [SerializeField] GameObject _NumberObject;

    private NumberConector _Number;
    private ListOfNumberPrefabs _List;

    private int _WhatNumber;
    private GameObject _InstantiatedNumber;

    Vector3 _Spawnlocation;
    Quaternion _SpawnRotation;

    //public void OnEnable()
    //{
    //    if (_Number == null)
    //    {
    //        _Number = target as NumberConector;
    //    }
    //}
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("SpawnNumber"))
        {
            _Spawnlocation = FindObjectOfType<PlacePoint>().transform.position;
            _List = FindObjectOfType<ListOfNumberPrefabs>();

            _Number = Instantiate(_NumberObject, _Spawnlocation, _SpawnRotation).GetComponent<NumberConector>();
            _WhatNumber = _WhatNumber + 1;
            _Number._WhatNumber = _WhatNumber;

            _List._NumberConectors.Add(_Number);            
        }

        if (GUILayout.Button("RemoveNumber"))
        {
            _WhatNumber = _WhatNumber - 1;
            _Number._WhatNumber = _WhatNumber;
            DestroyImmediate(_List._NumberConectors[_Number._WhatNumber].gameObject);
            _List._NumberConectors.RemoveAt(_Number._WhatNumber);       
        }      
        EditorUtility.SetDirty(_Number);
        EditorUtility.SetDirty(_List);
        base.OnInspectorGUI();
    }
}
