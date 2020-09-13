using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MainCodeObj : MonoBehaviour
{
    public InputField textInput;
    public InputField textOutput;
    public InputField inputField_recursiveSteps;
    public string inputText;
    public Toggle toggle_verbose;
    public string currentIdentity = "not set yet";
    public GameObject infoscreen;
    List<int> inputList = new List<int>();

    private void Start()
    {
        textInput.text = "";
        inputField_recursiveSteps.text = "8";
    }

    //called by button
    public void recursiveOutput()
    {
        //set limit to users
        int limit = 100;
        //check recursive values
        int steps = int.Parse(inputField_recursiveSteps.text);

        //check recursive steps, max output 100 
        if (inputField_recursiveSteps.text == "" && steps > limit && steps % 2 != 0)
        {
            addTextToOutput("recursive number empty, uneven or larger than limit = " + limit);
            return;
        }

        textInput.text = "1,2";
        CalculateFoldIdentity(false);
        if (toggle_verbose.isOn)
        {
            addTextToOutput(" ");
        }
        else
        {
            addTextToOutput(",",false);
        }

        if (steps > 2)
        {
            //textInput.text += ",";
        }
        int count = 2;
        //generate text for start values, starts at two and 
        for (int i = 2; i < steps; i += 2)
        {
            textInput.text += ",";

            count += 1;
            textInput.text += count.ToString() + ",";

            count += 1;

            Debug.Log("i: " + i + " steps: " + steps);
            if (i < steps - 1)
            {
                textInput.text += count.ToString();
            }
            else
            {
                textInput.text += count.ToString() + ",";
            }
            Debug.Log("Calc fold");
            CalculateFoldIdentity(false);

            if (toggle_verbose.isOn)
            {
                addTextToOutput(" ");
            }
            else
            {
                addTextToOutput(",", false);
            }
        }
    }

    //called by button, and by recursive steps
    public void CalculateFoldIdentity(bool clearOutput = true)
    {
        //for recursive to bypass
        if (clearOutput)
        {
            clearOutputText();
        }

        //get text first time
        inputText = GetInputFieldText();

        if (toggle_verbose.isOn)
        {
            addTextToOutput("Sequence: {" + inputText + "}");
        }

        string[] lines = inputText.Split(new string[] { "," }, StringSplitOptions.None);

        //emtpy list if items added previously
        inputList.Clear();
        //add input to List
        for (int i = 0; i < lines.Length; i++)
        {
            inputList.Add(int.Parse(lines[i]));
        }

        if (toggle_verbose.isOn)
        {
            addTextToOutput("Burrito Matrix of n = " + inputList.Count);
        }

        List<int> tempInputList = new List<int>(inputList);
        List<int> idNumber_list = new List<int>();

        //input number is member of first row
        printOutListToOutput(inputList);

        for (int i = 0; i < inputList.Count - 1; i++)
        {
            idNumber_list.Add(tempInputList[1]);
            //recursive
            tempInputList = FoldListSequence(tempInputList);

            printOutListToOutput(tempInputList);
        }
        idNumber_list.Add(inputList[1]);

        //the identity Number
        if (toggle_verbose.isOn)
        {
            textOutput.text += "Identity Number: {";
            printOutListToOutput(idNumber_list, false);
            textOutput.text += "}\n";
        }

        //the identity sum
        int sum = 0;
        for (int j = 0; j < idNumber_list.Count; j++)
        {
            sum += idNumber_list[j];
        }

        if (toggle_verbose.isOn)
        {
            addTextToOutput("The sum of second column: " + sum);
        }
        else
        {
            addTextToOutput(sum.ToString(), false);
        }
    }

    public void printOutListToOutput(List<int> pList, bool noLinebreak = true)
    {
        if (!toggle_verbose.isOn)
        {
            return;
        }
        string pString = "";
        for (int i = 0; i < pList.Count; i++)
        {
            if (i != pList.Count - 1)
            {
                pString += pList[i].ToString() + ",";
            }
            else
            {
                pString += pList[i].ToString();
            }
        }
        if (noLinebreak)
        {
            addTextToOutput(pString);
        }
        else
        {
            addTextToOutput(pString, false);
        }

    }

    private List<int> FoldListSequence(List<int> seqList)
    {
        List<int> tempList = new List<int>(seqList);

        //fold templist
        List<int> firstHalf = tempList.GetRange(0, (tempList.Count / 2));
        List<int> secondHalf = tempList.GetRange((tempList.Count / 2), tempList.Count / 2);

        //reverse second Half
        secondHalf.Reverse();

        List<int> returnList = new List<int>();

        int countfirst = 0;
        int countsecond = 0;
        for (int i = 0; i < seqList.Count; i++)
        {
            if (i % 2 == 0)
            {
                returnList.Add(firstHalf[0]);
                firstHalf.RemoveAt(0);
            }
            else
            {
                returnList.Add(secondHalf[0]);
                secondHalf.RemoveAt(0);
            }
        }
        return returnList;
    }

    public void copyToMemory() { GUIUtility.systemCopyBuffer = textOutput.text; }
    private string GetInputFieldText() { return (textInput.text); }
    private void addTextToOutput(string s, bool linebreak = true)
    {
        if (linebreak)
        {

            textOutput.text += s + "\n";
        }
        else
        {
            textOutput.text += s;
        }
    }
    private void clearOutputText() { textOutput.text = ""; }
}
