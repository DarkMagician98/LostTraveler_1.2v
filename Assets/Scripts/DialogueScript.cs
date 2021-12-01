using UnityEngine;

[System.Serializable]
public class DialogueScript
{
    public string key;

    [TextArea(3, 10)]
    public string[] sentences;
}
