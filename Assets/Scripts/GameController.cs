using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class GameController : MonoBehaviour {

    public DialogueController dialogue;
    public ImageAssets imageAssets;
    public SceneController scene;

    Texture sceneBG;
    string filePath;
    string fileName;
    char dialogueSeparator;
    bool mouseClick = false;
    HashSet<Characters> charSet;
    HashSet<Characters> sceneCharacters;

    void Start() {
        //Misc
        dialogueSeparator = '\\';

        //Define file path and file name
        filePath = "Assets/Text/";
        fileName = "DialogueScript.txt";

        //Create characters of the game, then put them into a GAME CHARACTER HASH SET to be pulled later
        charSet = new HashSet<Characters>();
        MakeCharacter("Makoto Kowata", 1, 0);
        MakeCharacter("Kei Kawaguchi", 4, 1);

        //Empty Character for dialogue controller use
        Characters newChar = new Characters();
        newChar.SetName("");
        newChar.SetAvatar(imageAssets.charAvatar[0]);
        dialogue.SetEmptyChar(newChar);

        //Create a Hash Set for the CHARACTERS IN ONE SCENE
        sceneCharacters = new HashSet<Characters>();

        //Set the background of this scene
        scene.SetSceneBG(imageAssets.bgImage[0]);

        //Start the scene
        StartCoroutine(DialogueEvent());

    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && dialogue.GetDialogueState())
            mouseClick = true;
    }

    IEnumerator DialogueEvent() {
        //Open the dialogue script text file
        using (StreamReader theReader = new StreamReader(filePath + fileName)) {

            //Try to open the file before continuing
            try {
                int readTest = theReader.Peek();
            } catch (Exception e) {
                Debug.Log("Cannot read " + fileName + " on " + filePath);
                Application.Quit();
            }

            //Read the dialogue script text
            //Queue the dialogue, show the next line only on mouse click
            string textLine;
            while ((textLine = theReader.ReadLine()) != null) {
                mouseClick = false;
                string[] line = textLine.Split(dialogueSeparator);

                //If the line is a dialogue
                if (line.Length.Equals(2)) {
                    Characters activeChar = null;

                    //Add new character to the Scene Character set
                    foreach (Characters charSetIteration in charSet) {
                        if (charSetIteration.GetName().Contains(line[0])) {
                            activeChar = charSetIteration;
                            sceneCharacters.Add(charSetIteration);
                        }
                    }

                    //Throw an error if the character is not found
                    if (activeChar.Equals(null))
                        Debug.Log("Cannot find character " + line[0] + " from file " + filePath + fileName + " in the game.");

                    //Set the scene
                    dialogue.SetAvatarName(activeChar.GetName());
                    scene.SetDialogueBox(activeChar.GetBoxImage());
                    dialogue.SetAvatar(activeChar.GetAvatar(), sceneCharacters);
                    //dialogue.SetDialogue(line[1]);
                    StartCoroutine(dialogue.RunDialogue(line[1]));
                    yield return new WaitUntil(() =>  mouseClick);

                } else if (line.Length.Equals(1)) { //If the line indicates a scene change
                    break;
                }
            }
            theReader.Close();
        }

        Application.Quit();
    }

    void MakeCharacter(string name, int avatarIndex, int boxIndex) {
        Characters newChar = new Characters();
        newChar.SetName(name);
        newChar.SetAvatar(imageAssets.charAvatar[avatarIndex]);
        newChar.SetBoxImage(imageAssets.boxImage[boxIndex]);
        charSet.Add(newChar);
    }
}
