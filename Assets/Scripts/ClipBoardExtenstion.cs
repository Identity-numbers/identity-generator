using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ClipBoardExtenstion
{
    public static void CopyToClipboard(this string str)
    {
        GUIUtility.systemCopyBuffer = str;
    }
}
