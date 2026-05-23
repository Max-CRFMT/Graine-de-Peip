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

    // Déclaration du champ privé pour le dictionnaire
    private Dictionary<string, Action> dico_Nom_event;

    public void Awake()
    {
        instance = this;
        dico_Nom_event = new Dictionary<string, Action>()
        {
            {"Inondations", Inondations },
            {"Avalanche", Avalanche },
            {"Incendies", Incendies },
            {"Théorie du Complot", Theorie_du_Complot },
            {"Espèce invasive", Espece_invasive },
            {"Déforestation", Deforestation },
            {"Désinformation", Desinformation },
            {"Grand banditisme", Grand_banditisme },
        };
    }

    public Dictionary<string, Action> Dico_Nom_event{get => dico_Nom_event;set => dico_Nom_event = value;}

    public void Lancement_event(string nom_event)
    {
        dico_Nom_event[nom_event].Invoke();
    }
    public void Inondations()
    {
        foreach (Player joueur in GameLogic.instance.Liste_Joueurs)
        {
            List<Carte> liste_carte = joueur.continent.pileFaceCachee;
            print($"premiere carte avant suppression {liste_carte[0].nom}");
            liste_carte.RemoveAt(0);
            print($"premiere carte apres suppression {liste_carte[0].nom}");
        }
        GameLogic.instance.Reproduction();
    }
    public void Avalanche()
    {
        System.Random random = new System.Random();
        Player joueur = GameLogic.instance.Liste_Joueurs[random.Next(GameLogic.instance.Liste_Joueurs.Count)];
        List<Carte> liste_carte = joueur.continent.pileFaceCachee;
        print($"premiere carte avant suppression {liste_carte[0].nom}");
        liste_carte.RemoveAt(0);
        print($"deuxieme carte avant suppression {liste_carte[0].nom}");
        liste_carte.RemoveAt(0);
        print($"troisieme carte avant suppression {liste_carte[0].nom}");
        liste_carte.RemoveAt(0);
        print($"premiere carte apres suppression {liste_carte[0].nom}");
        GameLogic.instance.Reproduction();
    }
    public void Incendies()
    {
        foreach (Player joueur in GameLogic.instance.Liste_Joueurs)
        {
            List<Carte> liste_carte = joueur.continent.pileFaceCachee;
            print($"premiere carte avant suppression {liste_carte[0].nom}");
            liste_carte.RemoveAt(0);
            print($"deuxieme carte avant suppression {liste_carte[0].nom}");
            liste_carte.RemoveAt(0);
            print($"troisieme carte avant suppression {liste_carte[0].nom}");
            liste_carte.RemoveAt(0);
            print($"premiere carte apres suppression {liste_carte[0].nom}");
        }
        GameLogic.instance.Reproduction();
    }
    public void Theorie_du_Complot()
    {
        foreach (Player joueur in GameLogic.instance.Liste_Joueurs)
        {
            if (joueur.continent.EducationLevel != -1)
            {
                joueur.continent.EducationLevel -= 1;
                print($"niveau d'education apres modif{joueur.continent.EducationLevel}");
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
            List<Carte> list_carte_reproduit = new List<Carte>();
            if (carte.vitesse == 2 && list_carte_reproduit.Exists(card => card.nom == carte.nom) == false)
            {
                list_carte_reproduit.Add(carte);
                liste_carte = GameLogic.instance.Ajout_carte(carte,joueur.continent.pileFaceCachee,joueur.continent.defausse);
            }
        }
        GameLogic.instance.Reproduction();
    }
    public void Deforestation()
    {
        foreach(Player joueur in GameLogic.instance.Liste_Joueurs)
        {
            int nombre_de_boucle = joueur.continent.pileFaceCachee.Count;
            for (int i = 0; i < nombre_de_boucle; i++)
            {
                joueur.continent.defausse.Add(joueur.continent.pileFaceCachee[i]);
                Debug.Log(joueur.continent.defausse.Count);
            }
            joueur.continent.pileFaceCachee.Clear();
        }
        GameLogic.instance.Reproduction();
    }
    public void Desinformation()
    {
        foreach (Player joueur in GameLogic.instance.Liste_Joueurs)
        {
            for (int i = 0; i < 2; i++)
            {
                if (joueur.continent.EducationLevel != -1)
                {
                    joueur.continent.EducationLevel -= 1;
                }
            }
            print($"niveau d'education apres modif{joueur.continent.EducationLevel}");
        }
        GameLogic.instance.Reproduction();
    }
    public void Grand_banditisme()
    {
        foreach (GameObject racine in GameObject.FindGameObjectsWithTag("SpeciesStack"))
        {
            List<Carte> list_carte_recensee = racine.transform.GetComponent<SpeciesStackScript>().Liste_Carte_Recensee;
            for (int i = 0; i < list_carte_recensee.Count; i++)
            {
                Carte carte_actuel = list_carte_recensee[i];
                if (carte_actuel.vitesse == 0)
                {
                    foreach (Player joueur in GameLogic.instance.Liste_Joueurs)
                    {
                        if (joueur.map_choisie == carte_actuel.continent_name)
                        {
                            joueur.continent.defausse.Add(carte_actuel);
                            list_carte_recensee.Remove(carte_actuel);
                        }
                    }
                }
            }
        }
        GameLogic.instance.Reproduction();
    }
}