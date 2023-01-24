using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public NumberConector[] _Connectors;
    public bool _Win;

    public UnityEvent _ActivateWin;
    public UnityEvent _SwitchScene;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Start()
    {
        _Connectors = FindObjectOfType<ListOfNumberPrefabs>()._NumberConectors.ToArray();
    }
    public IEnumerator Win()
    {
        yield return new WaitForSeconds(1);
        _ActivateWin.Invoke();
        yield return new WaitForSeconds(5);
        _SwitchScene.Invoke();
    }
}
