using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/*
TODO: Value imagedata toogle get working
TODO: Add recursive functionality
TODO: Add "use special seq"
TODO: Get "copy to clipboard or save to file working"
TODO: fix open close special seq window
*/

public class MainCodeObj : MonoBehaviour
{
    //INPUT TEXTFIELD SEQUENCE ========================================
    public InputField text_Input;
    public CleanInputTxt cleanInputTxt;

    //OUTPUT TEXTFIELD SEQUENCE =======================================
    public InputField text_Output;

    //SINGLE  =========================================================
    public Toggle toggle_VelocityMap_Image;
    public Toggle toggle_Mma_ImageData;

    //SINGLE RECURSIVE ================================================
    public InputField inputField_singlerecursive_column;
    public InputField inputField_singlerecursive_loop;
    public Toggle toggle_addFirstDigit_removelast;

    //GROWING RECURSIVE ===============================================
    public InputField inputField_recursiveSteps_In;
    public InputField inputField_recursiveSteps_Out;

    //SPECIAL SEQ SETTINGS  ===========================================

    public InputField inputField_specialseq;
    public GameObject GoSpecialseq;

    //GLOBAL SETTINGS   ===============================================
    public Toggle toggle_Verbose;

    //                  ===============================================

    [HideInInspector]
    public string input_TextString;
    List<int> input_List = new List<int>();

    private void Start()
    {
        text_Input.text = "1,2,3,4";
        inputField_recursiveSteps_In.text = "2";
        inputField_recursiveSteps_Out.text = "8";
    }

    //called by button
    public void recursiveOutput()
    {
        clearOutputText();

        //set limit to users
        int limit = 100;

        string txtIn = inputField_recursiveSteps_In.text;
        string txtOut = inputField_recursiveSteps_Out.text;

        //check recursive values
        if (string.IsNullOrEmpty(txtIn) || string.IsNullOrEmpty(txtOut))
        {
            addTextToOutput("recursive number in field In or Out EMPTY");
            return;
        }

        int stepsIn = int.Parse(txtIn);
        int stepsOut = int.Parse(txtOut);

        if (stepsIn % 2 != 0 || stepsOut % 2 != 0)
        {
            addTextToOutput("recursive number in field In or Out not EVEN");
            return;
        }
        else if (stepsIn == 0)
        {
            stepsIn = 2;
            inputField_recursiveSteps_In.text = stepsIn.ToString();
        }
        else if (stepsIn > stepsOut)
        {
            stepsOut = stepsIn;
            inputField_recursiveSteps_Out.text = stepsOut.ToString();
        }
        else if (stepsIn > limit || stepsOut > limit)
        {
            addTextToOutput("recursive number In or Out larger than LIMIT = " + limit);
            return;
        }

        //add padding to this
        string paddingString = "";
        for (int k = 0; k < stepsIn; k++)
        {
            paddingString += (k + 1).ToString();
            if (k < stepsIn - 1)
            {
                paddingString += ",";
            }
        }
        text_Input.text = paddingString;

        CalculateFoldIdentity(false);
        if (toggle_Verbose.isOn)
        {
            addTextToOutput(" ");
        }
        else
        {
            addTextToOutput(",", false);
        }

        int count = stepsIn;
        //generate text for start values, starts at two and 
        for (int i = stepsIn; i < stepsOut; i += 2)
        {
            text_Input.text += ",";

            count += 1;
            text_Input.text += count.ToString() + ",";

            count += 1;

            if (i < stepsOut - 1) { text_Input.text += count.ToString(); } else { text_Input.text += count.ToString() + ","; }

            CalculateFoldIdentity(false);

            if (toggle_Verbose.isOn) { addTextToOutput(" "); } else { addTextToOutput(",", false); }
        }
    }

    //called by button, and by recursive steps button
    public void CalculateFoldIdentity(bool clearOutput = true)
    {
        //for recursive to bypass
        if (clearOutput)
        {
            clearOutputText();
        }

        if (cleanInputTxt.ValidateInputString() == false)
        {
            return;
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
            addTextToOutput("The sum of second column: " + sum);
            addTextToOutput("The abs sum of travel through second column: " + travelValue);
        }
        else
        {
            //addTextToOutput(sum.ToString(), false);
            //addTextToOutput(travelValue.ToString(), false);
        }
    }

    public void printOutListToOutput(List<int> pList, bool noLinebreak = true)
    {
        if (!toggle_Verbose.isOn)
        {
            //return;
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
    private string GetInputFieldText()
    {
        cleanInputTxt.CleanLastComma();
        return (text_Input.text);
    }

    public void addTextToOutput(string s, bool linebreak = true)
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
