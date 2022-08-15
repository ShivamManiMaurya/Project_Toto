using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionWindow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;


    private Queue<string> _sentences;


    private void Awake()
    {
        _sentences = new Queue<string>();
    }

    public void StartInstruction(Instruction instruction)
    {
        PauseMenu.GameIsPaused = true;

        _sentences.Clear();

        //for (int i = 0; i < instruction.sentances.Length; i++)
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

        string sentence = _sentences.Dequeue();

        // StopAllCoroutines() function is used because if our sentence letters are comming one by one
        // so no other coroutine will run.
        StopAllCoroutines();
        StartCoroutine(TypeScentence(sentence));

    }

    // type the sentence like they are comming one by one letters
    IEnumerator TypeScentence(string sentence)
    {
        textMeshPro.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            textMeshPro.text += letter;
            yield return null;
        }
    }

    void EndInstruction()
    {
        Debug.Log("End of Instructions");
    }

}
