using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{

    public MainCodeObj mainCodeObj;

    public void onButtonPressed()
    {
        //Debug.Log("Hello");
        mainCodeObj.CalculateFoldIdentity();
    }
}
