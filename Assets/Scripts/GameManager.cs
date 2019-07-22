using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Singleton
    public static GameManager instance;

    void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    [HideInInspector] public GameObject player;

    void Start() {
        player = FindObjectOfType<PlatformerMovement2D>().gameObject;
    }
}
