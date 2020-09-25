using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MainCodeObj : MonoBehaviour
{
    public InputField text_Input;
    public InputField text_Output;
    public InputField inputField_recursiveSteps_In;
    public InputField inputField_recursiveSteps_Out;
    [HideInInspector]    
    public string input_TextString;
    public Toggle toggle_Verbose;
    List<int> input_List = new List<int>();

    private void Start()
    {
        text_Input.text = "";
        inputField_recursiveSteps_Out.text = "8";
    }

    //called by button
    public void recursiveOutput()
    {
        //set limit to users
        int limit = 100;

        string txt = inputField_recursiveSteps_Out.text;
        //check recursive values
        int steps = int.Parse(txt);

        //check recursive steps, max output 100 
        if (txt == "" && steps > limit && steps % 2 != 0)
        {
            //number doesn't check
            addTextToOutput("recursive number empty, uneven or larger than limit = " + limit);
            return;
        }

        text_Input.text = "1,2";
        CalculateFoldIdentity(false);
        if (toggle_Verbose.isOn)
        {
            addTextToOutput(" ");
        }
        else
        {
            addTextToOutput(",", false);
        }

        if (steps > 2)
        {
            //textInput.text += ",";
        }
        int count = 2;
        //generate text for start values, starts at two and 
        for (int i = 2; i < steps; i += 2)
        {
            text_Input.text += ",";

            count += 1;
            text_Input.text += count.ToString() + ",";

            count += 1;

            Debug.Log("i: " + i + " steps: " + steps);

            if (i < steps - 1) { text_Input.text += count.ToString(); } else { text_Input.text += count.ToString() + ","; }

            CalculateFoldIdentity(false);

            if (toggle_Verbose.isOn) { addTextToOutput(" "); } else { addTextToOutput(",", false); }
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
        input_TextString = GetInputFieldText();

        if (toggle_Verbose.isOn)
        {
            addTextToOutput("Sequence: {" + input_TextString + "}");
        }

        string[] lines = input_TextString.Split(new string[] { "," }, StringSplitOptions.None);

        //emtpy list if items added previously
        input_List.Clear();
        //add input to List
        for (int i = 0; i < lines.Length; i++)
        {
            input_List.Add(int.Parse(lines[i]));
        }

        if (toggle_Verbose.isOn)
        {
            addTextToOutput("Burrito Matrix of n = " + input_List.Count);
        }

        List<int> tempInputList = new List<int>(input_List);
        List<int> idNumber_list = new List<int>();

        //input number is member of first row
        printOutListToOutput(input_List);

        for (int i = 0; i < input_List.Count - 1; i++)
        {
            idNumber_list.Add(tempInputList[1]);
            //recursive
            tempInputList = FoldListSequence(tempInputList);

            printOutListToOutput(tempInputList);
        }
        idNumber_list.Add(tempInputList[1]);

        //the identity Number
        if (toggle_Verbose.isOn)
        {
            text_Output.text += "Identity Number: {";
            printOutListToOutput(idNumber_list, false);
            text_Output.text += "}\n";
        }

        //the identity sum
        int sum = 0;
        for (int j = 0; j < idNumber_list.Count; j++)
        {
            sum += idNumber_list[j];
        }

        int travelValue = 0;
        if (idNumber_list.Count > 2)
        {
            for (int i = 0; i < idNumber_list.Count - 1; i++)
            {
                travelValue += Mathf.Abs(idNumber_list[i] - idNumber_list[i + 1]);
            }
        }

        if (toggle_Verbose.isOn)
        {
            //addTextToOutput("The sum of second column: " + sum);
            addTextToOutput("The abs sum of travel through second column: " + travelValue);
        }
        else
        {
            //addTextToOutput(sum.ToString(), false);
            addTextToOutput(travelValue.ToString(), false);
        }
    }

    public void printOutListToOutput(List<int> pList, bool noLinebreak = true)
    {
        if (!toggle_Verbose.isOn)
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

    public void copyToMemory() { GUIUtility.systemCopyBuffer = text_Output.text; }
    private string GetInputFieldText() { return (text_Input.text); }
    private void addTextToOutput(string s, bool linebreak = true)
    {
        if (linebreak)
        {

            text_Output.text += s + "\n";
        }
        else
        {
            text_Output.text += s;
        }
    }
    private void clearOutputText() { text_Output.text = ""; }
}
