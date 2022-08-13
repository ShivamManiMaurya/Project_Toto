using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionWindow : MonoBehaviour
{
    private Queue<string> _sentences;

    private void Start()
    {
        _sentences = new Queue<string>();
    }

    public void StartInstruction(Instruction instruction)
    {
        Debug.Log("Starting Instruction with " + instruction);

        _sentences.Clear();

        foreach (string sentence in instruction.sentances)
        {
            _sentences.Enqueue(sentence);
        }

        DisplayNextScentence();
    }

    public void DisplayNextScentence()
    {
        if (_sentences.Count == 0)
        {
            EndInstruction();
            return;
        }

        string scentence = _sentences.Dequeue();
        Debug.Log(scentence);
    }

    void EndInstruction()
    {
        Debug.Log("End of Instructions");
    }

}
