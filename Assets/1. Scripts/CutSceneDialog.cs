using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialog 
{
    public string text;
    public float startTime;
}

[CreateAssetMenu(fileName = "Dialog", menuName ="Dialog")]
public class CutSceneDialog : ScriptableObject
{
    public int gid;
    public Dialog[] dialogs;
}
