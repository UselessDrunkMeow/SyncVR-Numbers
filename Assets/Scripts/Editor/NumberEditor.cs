using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlacePoint))]
public class NumberEditor : Editor
{
    [SerializeField] GameObject _NumberObject;//the number object that needs to be instantiated
    [SerializeField] GameObject _SmallNumberObject;//the number object that needs to be instantiated

    private NumberConector _Number;//A refrence to the instantiated object
    private ListOfNumberPrefabs _List;//A list of all the numbers in the scene
    private NumberEditor _NumberEditor;//A list of all the numbers in the scene

    private int _WhatNumber;

    Vector3 _Spawnlocation;
    Quaternion _SpawnRotation;

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("SpawnNumber"))
        {
            _Spawnlocation = FindObjectOfType<PlacePoint>().transform.position;//Finds the placepoint object and sets the spawnlocation to tis location
            _List = FindObjectOfType<ListOfNumberPrefabs>();//grabs a refrence to the list

            _Number = Instantiate(_NumberObject, _Spawnlocation, _SpawnRotation).GetComponent<NumberConector>();//instantiate the object and save it for later use

            _WhatNumber = _WhatNumber + 1;
            _Number._WhatNumber = _WhatNumber;

            _List._NumberConectors.Add(_Number);//Adds the instantiated object to the list
        }
        if (GUILayout.Button("SpawnSmallNumber"))
        {
            _Spawnlocation = FindObjectOfType<PlacePoint>().transform.position;//Finds the placepoint object and sets the spawnlocation to tis location
            _List = FindObjectOfType<ListOfNumberPrefabs>();//grabs a refrence to the list

            _Number = Instantiate(_SmallNumberObject, _Spawnlocation, _SpawnRotation).GetComponent<NumberConector>();//instantiate the object and save it for later use

            _WhatNumber = _WhatNumber + 1;
            _Number._WhatNumber = _WhatNumber;

            _List._NumberConectors.Add(_Number);//Adds the instantiated object to the list
        }

        if (GUILayout.Button("RemoveNumber"))
        {           
            if (_WhatNumber <= 0)
            {
                _WhatNumber = 1;
            }

            if (_Number != null)
            {
                _WhatNumber = _WhatNumber - 1;
                _Number._WhatNumber = _WhatNumber;
                DestroyImmediate(_List._NumberConectors[_Number._WhatNumber].gameObject);//Destorys the object
                _List._NumberConectors.RemoveAt(_Number._WhatNumber);//Removes the objcet from the list
            }
        }

        EditorUtility.SetDirty(_Number);//saves the changes made to the object
        EditorUtility.SetDirty(_List);//saves the changes made to the object
        EditorUtility.SetDirty(_NumberEditor);//saves the changes made to the object
        base.OnInspectorGUI();
    }
}
