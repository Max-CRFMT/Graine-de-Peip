using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Fonction_event : MonoBehaviour 
{
    public static Fonction_event instance;
    private void Awake()
    {
        instance = this;
    }
    public void Inondations()
    {
        foreach (Player joueur in GameLogic.instance.Liste_Joueurs)
        {
            List<Carte> liste_carte = joueur.continent.pileFaceCachee;
            print($"premiere carte avant suppression {liste_carte[0].ToString()}");
            liste_carte.RemoveAt(0);
            print($"premiere carte apres suppression {liste_carte[0].ToString()}");
        }
    }
    public void Avalanche()
    {
        System.Random random = new System.Random();
        Player joueur = GameLogic.instance.Liste_Joueurs[random.Next(GameLogic.instance.Liste_Joueurs.Count)];
        List<Carte> liste_carte = joueur.continent.pileFaceCachee;
        print($"premiere carte avant suppression {liste_carte[0].ToString()}");
        liste_carte.RemoveAt(0);
        print($"deuxieme carte avant suppression {liste_carte[0].ToString()}");
        liste_carte.RemoveAt(0);
        print($"troisieme carte avant suppression {liste_carte[0].ToString()}");
        liste_carte.RemoveAt(0);
        print($"premiere carte apres suppression {liste_carte[0].ToString()}");
    }
    public void Incendies()
    {
        foreach (Player joueur in GameLogic.instance.Liste_Joueurs)
        {
            List<Carte> liste_carte = joueur.continent.pileFaceCachee;
            print($"premiere carte avant suppression {liste_carte[0].ToString()}");
            liste_carte.RemoveAt(0);
            print($"deuxieme carte avant suppression {liste_carte[0].ToString()}");
            liste_carte.RemoveAt(0);
            print($"troisieme carte avant suppression {liste_carte[0].ToString()}");
            liste_carte.RemoveAt(0);
            print($"premiere carte apres suppression {liste_carte[0].ToString()}");
        }
    }
     public void Theorie_du_Complot()
    {
        foreach(Player joueur in GameLogic.instance.Liste_Joueurs)
        {
            int niveau_education = joueur.continent.EducationLevel;
            if (niveau_education != -1)  
            {
                niveau_education -= 1;
                print($"niveau d'education apres modif{niveau_education}");
            }
            else
            {
                niveau_education += 0;
            }
        }
    }
    public void Espece_invasive()
    {
        System.Random random = new System.Random();
        Player joueur = GameLogic.instance.Liste_Joueurs[random.Next(GameLogic.instance.Liste_Joueurs.Count)];
        List<Carte> liste_carte = joueur.continent.pileFaceCachee;
        foreach (Carte carte in liste_carte)
        {
            if (carte.vitesse == 2)
            {

            }
        }
    }
}