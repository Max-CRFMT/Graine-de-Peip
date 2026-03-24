using JetBrains.Annotations;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class generateur_carte : MonoBehaviour
{
    
    void Start()
    {
        List<List<string>> tableau_Oceanie = new();
        tableau_Oceanie = GameLogic.instance.Traduction_csv("Assets/data/tableau/tableau_oceanie.csv",10,tableau_Oceanie);
        Creation_carte_plante(tableau_Oceanie);
    }
    public List<Carte> Creation_carte_plante(List<List<string>> liste_de_caracteristique)
    {
        List<Carte> liste_instance = new();
        bool conservable;
        for (int i = 0; i < liste_de_caracteristique.Count; i++)
        {
            if (liste_de_caracteristique[i][6] == "Orthodoxe (Oui)")
            {
                conservable=true;
            }
            else 
            {
                conservable=false;
            }
            for (int j = 0; j < int.Parse(liste_de_caracteristique[i][7]); j++)
            {
                Carte une_carte = new Carte(liste_de_caracteristique[i][0], liste_de_caracteristique[i][2], conservable, int.Parse(liste_de_caracteristique[i][8]));
                liste_instance.Add(une_carte);
                
            } 
            
        }
        return liste_instance;
    }
}
