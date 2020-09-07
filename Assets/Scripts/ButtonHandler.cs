using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{

    public MainCodeObj mainCodeObj;

    public void onButtonPressed()
    {
        mainCodeObj.CalculateFoldIdentity();
    }
}
