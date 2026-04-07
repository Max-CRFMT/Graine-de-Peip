using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Jardin
{
    public string name;
    public int limite_max_jardin = 8;
    public static int niveau_jardin = 1;
    public static int fibo1 = 0;
    public static int fibo2 = 1;
    public static List<string> liste_biome_jardin;

    public List<Carte> Liste_Carte;

    Dictionary<string, List<string>> Dict_Continent_Biomes = new Dictionary<string, List<string>>()
    {
        {"Europe", new List<string>(){"Bretagne", "Paris"} },
        {"Afrique", new List<string>(){"Mali", "Maroc"} },
        {"Asie", new List<string>(){"Japon", "Coree"} },
        {"Oceanie", new List<string>(){"Iles", "Australie" } },
        {"Amerique du Sud", new List<string>(){"Bresil", "Argentine"} },
        {"Amerique du Nord", new List<string>(){"Canada", "US" } },
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

    public void Amelioration_du_jardin()
    {
        if (niveau_jardin < limite_max_jardin)
        {
            niveau_jardin = fibo1 + fibo2;
            fibo1 = fibo2;
            fibo2 = niveau_jardin;
            Debug.Log(niveau_jardin);
        }
        else
        {
            Debug.Log("Niveau maximal atteint");
        }
    }

    public void Ajout_un_biome_au_jardin(string nom_biome)
    {
        liste_biome_jardin.Add(nom_biome);
        //string message = string.Join(",", liste_biome_jardin);
        //Debug.Log(message);
    }
}
