using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MainCodeObj : MonoBehaviour
{
    public InputField textInput;
    public InputField textOutput;
    public string inputText;
    public Toggle toggle;
    public string currentIdentity = "not set yet";
    public GameObject infoscreen;

    //new vars
    List<int> inputList = new List<int>();

    private void Start()
    {
        textInput.text = "1,2,3,4";
    }

    //called when button pressed
    public void CalculateFoldIdentity()
    {
        clearOutputText();

        //get text first time
        inputText = GetInputFieldText();

        addTextToOutput("Sequence: {" + inputText + "}");

        string[] lines = inputText.Split(new string[] { "," }, StringSplitOptions.None);

        //emtpy list if items added previously
        inputList.Clear();
        //add input to List
        for (int i = 0; i < lines.Length; i++)
        {
            inputList.Add(int.Parse(lines[i]));
        }

        addTextToOutput("Burrito Matrix of n = " + inputList.Count);
        
        List<int> tempInputList = new List<int>(inputList);
        List<int> idNumber_list = new List<int>();

        //input number is member of first row
        printOutListToOutput(inputList);

        for (int i = 0; i < inputList.Count-1; i++)
        {
            idNumber_list.Add(tempInputList[1]);
            //recursive
            tempInputList = FoldListSequence(tempInputList);

            printOutListToOutput(tempInputList);
        }
        idNumber_list.Add(inputList[1]);

        //the identity Number
        textOutput.text += "Identity Number: {";
        printOutListToOutput(idNumber_list);
        textOutput.text += "}";

        //the identity sum
        int sum = 0;
        for (int j = 0; j < idNumber_list.Count; j++)
        {
            sum +=idNumber_list[j];
        }
        addTextToOutput("The sum of second column: " + sum);
    }

    public void printOutListToOutput(List<int> pList)
    {
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
        addTextToOutput(pString);
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

    public void copyToMemory()
    {
        GUIUtility.systemCopyBuffer = textOutput.text;
    }

    private string GetInputFieldText()
    {
        return (textInput.text);
    }

    private void addTextToOutput(string s)
    {
        textOutput.text += s + "\n";
    }

    private void clearOutputText()
    {
        textOutput.text = "";
    }
}
