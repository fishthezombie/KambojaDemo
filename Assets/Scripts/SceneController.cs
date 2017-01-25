using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

    public RawImage sceneBg;
    public RawImage dialogueBox;
    public GameObject backlogScreen;

	public void SetSceneBG(Texture sceneImage) {
        sceneBg.texture = sceneImage;
    }

    public void HideDialogueBox(bool trigger) {
        dialogueBox.gameObject.SetActive(!trigger);
    }

    public Texture GetSceneBG() {
        return sceneBg.texture;
    }

    public void SetDialogueBox(Texture dialogueBoxImage) {
        dialogueBox.enabled = true;
        dialogueBox.texture = dialogueBoxImage;
    }

    public Texture GetDialogueBox() {
        return dialogueBox.texture;
    }

    public void ShowBacklog(bool show, int lineNumber) {
        backlogScreen.SetActive(show);
        if (show) {
            Text backlogText = backlogScreen.GetComponentInChildren<Text>();
            backlogText.text = DialogueScriptParser.Backlog(3);
        }
    }
}
