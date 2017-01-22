using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameStateController : MonoBehaviour {

    public static GameStateController gameState;

    bool gameEnd = false;
    int chapter;

    void Awake() {
        if (gameState == null) {
            DontDestroyOnLoad(gameObject);
            gameState = this;
        } else if (gameState != this) {
            Destroy(gameObject);
        }
        
    }
    public void SetGameEnd(bool state) {
        gameEnd = state;
    }

    public bool GetGameEnd() {
        return gameEnd;
    }

    public int GetScene() {
        return SceneManager.GetActiveScene().buildIndex;    
    }

    void OnGUI() {
        GUI.Label(new Rect(10, 10, 100, 30), "Scene: " + GetScene());
    }
}
