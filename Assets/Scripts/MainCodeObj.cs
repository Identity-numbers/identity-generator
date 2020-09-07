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
    public Toggle toggle_removeLastDigit;
    public Toggle toggle_addFirstDidgit;

    /* TODO 
    Remove last digit in identity number
    check if copy to memory works in WebGL?
    checkbox readd first digit to identitynumber
    */

    private void Start()
    {
        textInput.text = "1234";
    }

    //called when button pressed
    public void CalculateFoldIdentity()
    {
        //get text
        inputText = GetInputFieldText();

        //text ok, clear output
        clearOutputText();

        //cut up text in linebreaks
        string[] lines = inputText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        for (int j = 0; j < lines.Length; j++)
        {
            //cut up string in two halfs and fold
            string s = lines[j];
            char firstDigit = s[0];
            Debug.Log(firstDigit);
            //validate string
            s = validateText(s);

            string foldedIdentity = "";
            int strLength = s.Length; //need length twice
            for (int i = 0; i < strLength; i++)
            {
                if (i == 0)
                {
                    addTextToOutput("\n" + "this is input: " + s);
                }

                if (toggle.isOn)
                {
                    addTextToOutput(s);
                }

                if (strLength >= 0)
                {
                    foldedIdentity += s.ToString()[1];
                }

                s = FoldSequence(s, strLength);
            }

            if (toggle_removeLastDigit.isOn)
            {
                foldedIdentity = foldedIdentity.Remove(foldedIdentity.Length - 1);
            }

            if (toggle_addFirstDidgit.isOn)
            {
                foldedIdentity = firstDigit + foldedIdentity;
            }

            addTextToOutput("Folded identity: " + foldedIdentity);
        }
    }

    private string FoldSequence(string s, int l)
    {
        //string length
        int strLength = l;

        //mixed string
        string mixedString = "";
        char[] mixedCharArray = new char[strLength];

        //take string and divide in two substrings
        string firstPart = s.Substring(0, strLength / 2);
        string secondPart = s.Substring(strLength / 2, strLength / 2);

        //reverse second part
        secondPart = Reverse(secondPart);

        char[] firstPartCharArr = firstPart.ToCharArray();
        char[] secondPartCharArr = secondPart.ToCharArray();

        //Mix arrays into new string
        for (int i = 0; i < strLength / 2; i++)
        {
            mixedString += firstPartCharArr[i];
            mixedString += secondPartCharArr[i];
        }
        return mixedString;
    }

    public void copyToMemory()
    {
        textOutput.text.CopyToClipboard();
    }

    private string GetInputFieldText()
    {
        //Debug.Log(textInput.text);
        return (textInput.text);
    }

    private string validateText(string s)
    {
        if (s.Length % 2 == 0)
        {
            //do nothing
        }
        else if (s.Length == 0)
        {
            //string is empty do something...
            s = "::";
        }
        else
        {
            addTextToOutput("Size not even, trimming end from: " + s);
            s = s.Remove(s.Length - 1);
            addTextToOutput("->                            to: " + s);
        }
        return (s);
    }

    private void addTextToOutput(string s)
    {
        textOutput.text += s + "\n";
    }

    private void clearOutputText()
    {
        textOutput.text = "";
    }

    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}
