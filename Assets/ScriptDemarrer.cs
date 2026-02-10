using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScriptDemarrer : MonoBehaviour
{
    public void ActualiserListeJoueur()
    {
        GameLogic.instance.SetListeJoueurs();
    }

    public void ShuffleListeJoueur()
    {

    }
}
