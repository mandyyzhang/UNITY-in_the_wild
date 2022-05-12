using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    //public string name; // character we are talking to 

    [TextArea(3, 10)]
    public string[] sentences;
}
