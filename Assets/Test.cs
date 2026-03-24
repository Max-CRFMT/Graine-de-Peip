using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [ContextMenu("TEST")]
    public void Test2()
    {
        Player joueur = new Player("Jéremy", 729, "Asie");
        joueur.continent.jardin.JSP();
    }
}
