using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script is used to load the instructio tab when the first level start
public class InstructionTrigger : MonoBehaviour
{
    public Instruction instruction;

    // for starting the instructions in start of the scene we trigger this function in start
    private void Start()
    {
        TriggerInstruction();
    }

    public void TriggerInstruction()
    {
        FindObjectOfType<InstructionWindow>().StartInstruction(instruction);
    }

    
}
