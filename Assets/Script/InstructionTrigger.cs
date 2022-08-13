using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionTrigger : MonoBehaviour
{
    public Instruction instruction;
    bool infoGiven = false;

    public void TriggerInstruction()
    {
        FindObjectOfType<InstructionWindow>().StartInstruction(instruction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !infoGiven)
        {
            TriggerInstruction();
            infoGiven = true;
        }
    }
}
