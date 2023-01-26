using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public NumberConector[] _Connectors;//a array with all of the connectors in this scene
    public bool _Win;//bool for when the game is won

    public UnityEvent _ActivateWin;//evemt to activate the win screen
    public UnityEvent _SwitchScene;//event to switch the scene
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
    public IEnumerator Win()//waits a few seconds before activating the win screen, and switchen scene
    {
        yield return new WaitForSeconds(1);
        _ActivateWin.Invoke();
        yield return new WaitForSeconds(5);
        _SwitchScene.Invoke();
    }
}
