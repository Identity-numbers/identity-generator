﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandlerSave : MonoBehaviour
{

    public MainCodeObj mainCodeObj;

    public void onButtonPressed()
    {
        mainCodeObj.copyToMemory();
    }
}
