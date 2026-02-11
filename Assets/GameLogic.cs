using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using NUnit.Framework;
using Unity.VisualScripting;
using System.Collections.Generic;
public class GameLogic : MonoBehaviour
{

    public int nb_joueurs ;
    public string difficulte;
    public List<Player> Liste_Joueurs;
    public static GameLogic instance;

    public int ToursRestants;

    Dictionary<string, int> DicoTourEnFctDeDifficulte = new Dictionary<string, int>()
    {
        {"Facile", 15},
        {"Normal", 25},
        {"Difficile", 30}
    };

    public GameLogic() { }

    private void Awake()
    {
        instance = this;
        instance.nb_joueurs = 2;
        instance.difficulte = "Facile";
        instance.Liste_Joueurs = new List<Player>();
        instance.ToursRestants = DicoTourEnFctDeDifficulte[difficulte];
    }

    public void SetNbJoueurs(int nombre)
    {
        nb_joueurs = nombre;
        Debug.Log("Le nombre de joueur s�lectionn� est :" + nb_joueurs);
    }

    public void SetDifficulte(string difficult)
    {
        difficulte = difficult;
        Debug.Log("La difficult� s�lectionn�e est :" + difficulte);
    }

    public void SetListeJoueurs()
    {
        for (int i = 0; i < instance.nb_joueurs; i++)
        {
            string nom_a_trouver = "Joueur" + (i+1).ToString();
            GameObject[] couple_nom_map = GameObject.FindGameObjectsWithTag(nom_a_trouver);
            string nom_joueur = couple_nom_map[0].GetComponent<TMP_InputField>().text;
            string map_joueur = couple_nom_map[1].GetComponent<TMP_Dropdown>().options[couple_nom_map[1].GetComponent<TMP_Dropdown>().value].text;

            instance.Liste_Joueurs.Add(new Player(nom_joueur, 0,map_joueur));
        }
    }

    public void Jeu()
    {
        while (instance.ToursRestants != 0)
        {
            TurnHandler.Instance.RoundComplet();
        }
        FinDePartie();
    }

    public void FinDePartie()
    {
        //TODO - 
    }
}

