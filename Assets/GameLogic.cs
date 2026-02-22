using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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

    public List<string> SelectionMaps = new List<string>() { "Europe", "Afrique", "Asie", "Océanie", "Amérique du Nord", "Amérique du Sud" };
    public void SetListeJoueurs()
    {
        System.Random random = new System.Random();
        List<(string, string)> ListeJoueursAttente = new List<(string, string)>();

        for (int i = 0; i < instance.nb_joueurs; i++)
        {
            string nom_a_trouver = "Joueur" + (i+1).ToString();
            GameObject[] couple_nom_map = GameObject.FindGameObjectsWithTag(nom_a_trouver);
            string nom_joueur = couple_nom_map[0].GetComponent<TMP_InputField>().text;
            string map_joueur = couple_nom_map[1].GetComponent<TMP_Dropdown>().options[couple_nom_map[1].GetComponent<TMP_Dropdown>().value].text;

            if (map_joueur == "Aléatoire")
            {
                ListeJoueursAttente.Add((nom_joueur, map_joueur));
            }
            else
            {
                instance.Liste_Joueurs.Add(new Player(nom_joueur, 0, map_joueur));
                SelectionMaps.Remove(map_joueur);
            }
        }

        foreach ((string, string) couple in ListeJoueursAttente)
        {
            string map_joueur = SelectionMaps[random.Next(SelectionMaps.Count)];
            SelectionMaps.Remove(map_joueur);
            instance.Liste_Joueurs.Add(new Player(couple.Item1, 0, map_joueur));
        }
    }

    public bool VerifUniteMap(TMP_Dropdown DropdownSource)
    {
        TMP_Dropdown[] listeDeroulantesMaps = GameObject.FindObjectsByType<TMP_Dropdown>(FindObjectsSortMode.None);
        foreach (TMP_Dropdown maps in listeDeroulantesMaps)
        {
            if ((DropdownSource.value != 0) && (maps.value == DropdownSource.value) && (maps.tag != DropdownSource.tag))
            {
                DropdownSource.value = 0;
                Debug.Log("Vous ne pouvez pas avoir deux continents identiques, map aléatoire");
                return false;
            }
        }
        return true;
    }

    public void Jeu()
    {
        SceneManager.LoadScene("Game");
        while (instance.ToursRestants != 0)
        {
            Debug.Log(instance.ToursRestants);
            TurnHandler.instance.ChangeEtatTour();
            instance.ToursRestants--;
        }
        FinDePartie();
    }

    public void FinDePartie()
    {
        //TODO - 
        Debug.Log("Fonction FinDePartie() executée");
    }
}

