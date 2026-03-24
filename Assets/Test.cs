using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public string biome;
    [ContextMenu("TEST")]
    public void Test2()
    {
        Player joueur = new Player("Jéremy", 729, "Asie");
        joueur.continent.jardin.Ajout_un_biome_au_jardin(biome);
    }
}
