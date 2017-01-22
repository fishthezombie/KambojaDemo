using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {

    public List<RawImage> avatarBox;
    public Text dialogueBox;
    public Text avatarNameBox;
    float autotypeInterval = 0.02f;

    List<string> dialogueLineList;
    List<RawImage> CharacterAvatars;
    bool keyPressed = false;
    bool dialogueDone = false;


    void Awake () {
        dialogueLineList = new List<string>();
	}
    
    void Start() {
        //Initial value
        CharacterAvatars = new List<RawImage>();

        //Get all avatar slots and disable Raw Image component
        GameObject[] findChars = GameObject.FindGameObjectsWithTag("Characters");
        foreach (GameObject character in findChars) {
            CharacterAvatars.Add(character.GetComponent<RawImage>());
            //character.GetComponent<RawImage>().enabled = false; //Put on Awake() so it'll be executed before SetAvatar()
        }
    }

    void Update() {
        if (Input.GetMouseButtonDown(0))
            keyPressed = true;
    }

    public void SetAvatar(Texture avatar) {
        //Tell the code that the box is filled on this scene
        bool filled = false;

        //Fill the avatar box with the avatar only when it's not exist in any box yet
        for (int i = 0; i < avatarBox.Count; ++i) {
            if (avatarBox[i].texture.Equals(null) || avatarBox[i].texture.Equals(avatar)) {
                avatarBox[i].enabled = true;
                avatarBox[i].texture = avatar;
                filled = true;
                break;
            }
        }

        //if all avatar slot has filled but still has unassigned character, throw an error
        if (!filled)
            Debug.Log("Cannot add new avatar image, all available slot is not empty");
    }

    public Texture GetAvatar(int index) {
        return avatarBox[index].texture;
    }


    public void AddDialogue(string text) {
        dialogueLineList.Add(text);
    }

    public void SetDialogue (string text) {
        dialogueBox.text = text;
    }

    public IEnumerator RunDialogue() {
        SetDialogueDone(false);
        foreach (string line in dialogueLineList) {
            keyPressed = false;
            dialogueBox.text = line;
            yield return new WaitUntil(() => keyPressed);
        }
        dialogueLineList.Clear();
        SetDialogueDone(true);            
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

    void SetDialogueDone (bool dialogueState) {
        dialogueDone = dialogueState;
    }

    public bool GetDialogueDone() {
        return dialogueDone;
    }


}
