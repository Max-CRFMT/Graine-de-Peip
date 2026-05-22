using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Jardin 
{
    public string name;
    public int limite_max_jardin = 8;
    public static int niveau_jardin = 1;
    public static int fibo1 = 1;
    public static int fibo2 = 1;
    public List<string> liste_biome_jardin;

    public List<Carte> Liste_Carte;

    Dictionary<string, List<string>> Dict_Continent_Biomes = new Dictionary<string, List<string>>()
    {
        {"Europe", new List<string>(){"Forêt tempérée"} },
        {"Afrique", new List<string>(){"Brousse", "Désert"} },
        {"Asie", new List<string>(){"Forêt pluvieuse"} },
        {"Oceanie", new List<string>(){"Désert", "Prairies"} },
        {"Amerique du Sud", new List<string>(){"Forêt pluvieuse"} },
        {"Amerique du Nord", new List<string>(){"Forêt de conifères"} },
    };

    public Jardin(string nom_continent)
    {
        name = nom_continent;
        if (liste_biome_jardin == null)  //Pour pas r�initialiser la liste_biome_jardin
        {
            liste_biome_jardin = new List<string>(Dict_Continent_Biomes[nom_continent]);
        }
        Liste_Carte = new List<Carte>();
    }

    public void Amelioration_niveau_jardin()
    {
        if (niveau_jardin < limite_max_jardin)
            {
                niveau_jardin = fibo1 + fibo2;
                fibo1 = fibo2;
                fibo2 = niveau_jardin;
                Debug.Log("Amélioration effectuée, le jardin peut maitenant accueillir " + niveau_jardin + " plantes.");
            }
            else
            {
                Debug.Log("Niveau maximal atteint");
            }
    }

    public void Ajout_biome_jardin(string biome)
    {
        Debug.Log("Le biome " + biome + " a été ajouté à la liste des biomes du jardin.");
        liste_biome_jardin.Add(biome);
    }
}
