using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// System.Serializable is used to show this type of class in unity i.e. not inherited from monobehaviour.
[System.Serializable]
public class Instruction
{
    public string name;
    [TextArea(3, 10)]
    public string[] sentances;

    
}
