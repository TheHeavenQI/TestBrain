using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobaManager : MonoBehaviour {
    public static GlobaManager Instance;
    public GameObject GameMoPubManagerPrefabs;
    private GameObject _gameMoPubManager;
    public void Awake() {
        Instance = this;
    }
    public void CreateMoPubManager() {
        if (_gameMoPubManager == null) {
            _gameMoPubManager = Instantiate(GameMoPubManagerPrefabs);
            _gameMoPubManager.name = "MoPubManager";
            _gameMoPubManager.SetActive(true);
        }
    }
    
}
