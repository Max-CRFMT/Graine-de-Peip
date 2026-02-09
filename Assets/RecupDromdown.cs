using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RecupDromdown : MonoBehaviour
{

    [SerializeField] TMP_Dropdown dropdown_nb_joueur;
    [SerializeField] TMP_Dropdown dropdown_difficulte;

    Dictionary<int, string> dict_difficulte = new Dictionary<int, string>()
    {
        {0, "Difficile" },
        {1, "Normal" },
        {2, "Difficile" }
    };

    public void RecupNbJoueurs()
    {
        int jsp = dropdown_nb_joueur.value;
        jsp += 2;
        GameLogic.instance.SetNbJoueurs(jsp);
    }

    public void RecupDifficulte()
    {
        GameLogic.instance.SetDifficulte(dict_difficulte[dropdown_difficulte.value]);
    }

}
