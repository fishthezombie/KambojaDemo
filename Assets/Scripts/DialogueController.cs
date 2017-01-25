using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {

    public List<RawImage> avatarBox;
    public Text dialogueBox;
    public Text avatarNameBox;
    float autotypeInterval = 0.02f;

    List<RawImage> characterAvatarBoxes;
    bool keyPressed = false;
    bool dialogueDone = false;
    Color nonActiveCharColor;
    Characters emptyChar;

    void Start() {
        //Initial value
        characterAvatarBoxes = new List<RawImage>();
        nonActiveCharColor = new Color(0.25f, 0.25f, 0.25f);

        //Get all avatar slots and disable Raw Image component
        GameObject[] findChars = GameObject.FindGameObjectsWithTag("Characters");
        foreach (GameObject character in findChars) {
            characterAvatarBoxes.Add(character.GetComponent<RawImage>());
            //character.GetComponent<RawImage>().enabled = false; //Put on Awake() so it'll be executed before SetAvatar()
        }
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) || (Input.mouseScrollDelta.y < 0f))
            keyPressed = true;
    }

    public void SetAvatar(Texture avatar, HashSet<Characters> activeCharacters) {
        //Tell the code that the box is filled on this scene
        //bool filled = false;

        //Fill the center box if there's only one character
        if (activeCharacters.Count.Equals(1)) {
            InsertAvatar(0, avatar);
        } else {
            //Move the center box character to the active box[1]
            if (avatarBox[0].texture != null && avatarBox[0].texture != EmptyChar().GetAvatar()) {
                InsertAvatar(1, avatarBox[0].texture);
                avatarBox[0].texture = EmptyChar().GetAvatar();
                avatarBox[0].enabled = false;
                //filled = true;
            }

            //Fill one of four avatar box with the avatar only when it's not exist in any box yet
            for (int i = 1; i < avatarBox.Count; ++i) {
                if (avatarBox[i].texture.Equals(null) || avatarBox[i].texture.Equals(avatar)) {
                    InsertAvatar(i, avatar);
                    SetActiveCharacter(avatarBox[i]);
                    //filled = true;
                    break;
                }
            }
        }

        //if all avatar slot has filled but still has unassigned character, throw an error
        //if (!filled)
        //    Debug.Log("Cannot add new avatar image, all available slot is not empty");
    }

    //Dim the color of non-speaker character in current scene
    void SetActiveCharacter (RawImage activeCharacter) {
        foreach (RawImage characterInScene in characterAvatarBoxes) {
            if (!characterInScene.Equals(activeCharacter)) {
                characterInScene.color = nonActiveCharColor;
            } else {
                characterInScene.color = Color.white;
            }
        }
    }

    public Texture GetAvatar(int index) {
        return avatarBox[index].texture;
    }

    void InsertAvatar(int index, Texture avatar) {
        avatarBox[index].enabled = true;
        avatarBox[index].texture = avatar;
    }

    //Autotype animation
    public IEnumerator SetDialogue(string dialogueLine) {
        SetDialogueState(false);
        dialogueBox.text = "";
        keyPressed = false;

        //Add letter by letter to the dialogue box until MLB is pressed
        foreach (char letter in dialogueLine) {
            if (keyPressed)
                break;
            dialogueBox.text += letter;
            yield return new WaitForSeconds(autotypeInterval);
        }

        dialogueBox.text = dialogueLine;
        SetDialogueState(true);
    }

    public void SetDialogueState(bool state) {
        dialogueDone = state;
    }

    public bool GetDialogueState() {
        return dialogueDone;
    }

    public string GetDialogue() {
        return dialogueBox.text;
    }
    public void SetAvatarName(string name) {
        avatarNameBox.text = name;
    }

    public string GetAvatarName() {
        return avatarNameBox.text;
    }

    public void SetEmptyChar (Characters chara) {
        emptyChar = chara;
    }

    public Characters EmptyChar() {
        return emptyChar;
    }

}
