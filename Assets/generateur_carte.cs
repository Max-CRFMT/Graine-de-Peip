using JetBrains.Annotations;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class generateur_carte : MonoBehaviour
{
    void Start()
    {
        List<List<string>> tableau_Oceanie = new();
        tableau_Oceanie = TurnHandler.instance.Traduction_csv("Assets/data/tableau/tableau_oceanie.csv",9,tableau_Oceanie);
        Creation_carte_plante(tableau_Oceanie);       
    }
    public List<Carte> Creation_carte_plante(List<List<string>> liste_de_caracteristique)
    {
        List<Carte> liste_instance = new();

        for (int i = 0; i < liste_de_caracteristique.Count; i++)
        {
            liste_de_caracteristique[i].RemoveAt(4);
            liste_de_caracteristique[i].RemoveAt(3);
            liste_de_caracteristique[i].RemoveAt(1);
            Carte une_carte = new Carte(liste_de_caracteristique[i][0], liste_de_caracteristique[i][1], true, 10);
            liste_instance.Add(une_carte);
            Debug.Log(liste_instance[i].ToString());
        }
        return liste_instance;
    }
}
