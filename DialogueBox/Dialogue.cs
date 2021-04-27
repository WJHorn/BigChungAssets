using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//defines what constitutes basic dialogue and adds editable fields to the Unity GUI
[System.Serializable]
public class Dialogue {
    
    public string name;
    [TextArea(3, 10)]
    public string[] sentences;
}
