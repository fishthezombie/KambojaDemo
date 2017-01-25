using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour {

    public static GameStateController gameState;
    public static string saveFileName = "save.kam";

    bool gameEnd = false;
    int scriptLine;

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

    public void SetScriptLine(int line) {
        scriptLine = line;
    }

    public int GetScriptLine() {
        return scriptLine;
    }

    public int GetScene() {
        return SceneManager.GetActiveScene().buildIndex;    
    }

    public string GetSceneName() {
        return SceneManager.GetActiveScene().name;
    }

    void OnGUI() {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 20;
        GUI.Label(new Rect(10, 10, 100, 30), "Scene: " + GetScene() + " - " + GetSceneName(), style);
        GUI.Label(new Rect(10, 40, 100, 30), "Saved Line: " + GetScriptLine(), style);
    }

    public void Save(int currentLine, bool gameState) {
        BinaryFormatter bFormat = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + saveFileName);

        GameData data = new GameData();
        data.scriptLineNumber = scriptLine;
        data.gameState = gameEnd;

        bFormat.Serialize(file, data);
        file.Close();
    }

    public void Load() {
        BinaryFormatter bFormat = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/" + saveFileName, FileMode.Open);

        GameData data = (GameData)bFormat.Deserialize(file);
        scriptLine = data.scriptLineNumber;
        gameEnd = data.gameState;

        file.Close();
    }
}

[Serializable]
class GameData {
    public int scriptLineNumber;
    public bool gameState;
}