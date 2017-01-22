using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

    public RawImage sceneBg;
    public RawImage dialogueBox;

	public void SetSceneBG(Texture sceneImage) {
        sceneBg.texture = sceneImage;
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
}
