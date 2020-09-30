using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text.RegularExpressions;

public class CleanInputTxt : MonoBehaviour
{

    public InputField inTxtField;
    public MainCodeObj mainCodeObj;

    //do this on value changed
    public void CheckString()
    {
        string txt = inTxtField.text;

        Regex regex = new Regex(@"[0-9,\\]");
        MatchCollection matches = regex.Matches(txt);
        string cleanTxtString = string.Join("", from Match match in matches select match.Value);
        inTxtField.text = cleanTxtString;
    }

    //validate inputstring
    public bool ValidateInputString()
    {
        string txt = inTxtField.text;

        //remove trailing comma, update txt
        txt = CleanLastComma();

        //check if txt empty
        if (string.IsNullOrEmpty(txt))
        {
            //inputstring empty
            mainCodeObj.addTextToOutput("Input sequence is empty");
            return false;
        }

        //check if group of digits is even
        if (CheckInputSeqEvenNumberDigits(txt) == false)
        {
            mainCodeObj.addTextToOutput("Sequence does not contain even number of digits");
            return false;
        }

        return true;
    }

    //do this once the input is needed
    public bool CheckInputSeqEvenNumberDigits(string txt)
    {
        string[] strArr = txt.Split(',');
        if (strArr.Length % 2 != 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //do this once the input is needed
    public string CleanLastComma()
    {
        string txt = inTxtField.text;
        string subString = txt.Substring(txt.Length - 1);
        if (subString == ",")
        {
            txt = txt.Remove(txt.Length - 1);
            inTxtField.text = txt;
            return txt;
        }else
        {
            return txt;
        }
    }
}
