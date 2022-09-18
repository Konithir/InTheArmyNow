using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton { get; private set; }

    private bool _gameHasStarted = false;

    [SerializeField]
    private Camera _mainCamera;

    public Camera MainCamera
    {
        get { return _mainCamera; }
    }

    public bool GameHasStarted
    {
        get { return _gameHasStarted; }
        set { _gameHasStarted = value; }
    }

    private void Awake()
    {
        Singleton = this;
    }
}
