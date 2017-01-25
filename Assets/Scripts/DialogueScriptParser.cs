using System.IO;
using UnityEngine;
using System;

public static class DialogueScriptParser {

    public static string filePath = "Assets/Text/";
    public static string fileName = "DialogueScript.txt";
    public static string colorLightblue = "#add8e6ff";
    public static string colorNavy = "#000080ff";
    public static string colorTeal = "#008080ff";

    static string charName, charDialogue;
    static char lineSeparator = '\\';
    static int currentLineNum;

    public static string Backlog(int sceneCurrentLine) {
        int maxBacklogLine = 20;
        int startBacklogLine = Mathf.Max(0, sceneCurrentLine - maxBacklogLine);
        string backLogText = "";

        using (StreamReader sReader = new StreamReader(filePath + fileName)) {

            //Try to read the file before continuing
            DialogueScriptCheck();

            //Read the file line by line
            string line;
            int currentBacklogLine = 0;
            while ((line = sReader.ReadLine()) != null) {
                string[] partLine = line.Split(lineSeparator);

                //Reformat the dialogue to fit backlog style using richtext format
                if (partLine.Length > 1 && currentBacklogLine >= startBacklogLine) { //Dialogue scene
                    if (partLine[0].Contains("Makoto"))
                        backLogText += RichTextBold(RichTextSize(RichTextColor(partLine[0], colorLightblue), 30));
                    else if (partLine[0].Contains("Kei"))
                        backLogText += RichTextBold(RichTextSize(RichTextColor(partLine[0], colorTeal), 30));
                    backLogText += "\n" + partLine[1] + "\n\n";
                } else if (currentBacklogLine > sceneCurrentLine) {
                    break;
                }
            }
            currentBacklogLine++;

            sReader.Close();
        }
        return backLogText;
    }

    static void DialogueScriptCheck() {
        using (StreamReader sReader = new StreamReader(filePath + fileName)) {
            try {
                int peekFile = sReader.Peek();
            } catch (Exception e) {
                Debug.Log("Cannot read " + fileName + " on " + filePath);
                Application.Quit();
            }
            sReader.Close();
        }
    }

    static string RichTextBold(string text) {
        return "<b>" + text + "</b>";
    }

    static string RichTextColor(string text, string hexCode) {
        return "<color=" + hexCode + ">" + text + "</color>";
    }

    static string RichTextSize(string text, int size) {
        return "<size=" + size + ">" + text + "</size>";
    }
}
