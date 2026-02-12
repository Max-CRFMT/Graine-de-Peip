using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Continent
{
    Dictionary<string, List<string>> Dict_Continent_Biomes = new Dictionary<string, List<string>>()
    {
        {"Europe", new List<string>(){"Bretagne", "Paris"} },
        {"Afrique", new List<string>(){"Mali", "Maroc"} },
        {"Asie", new List<string>(){"Japon", "Corée"} },
        {"Océanie", new List<string>(){"Iles", "Australie" } },
        {"Amérique du Sud", new List<string>(){"Bresil", "Argentine"} },
        {"Amérique du Nord", new List<string>(){"Canada", "US" } },
    };
    public Continent(string nom)
    {
        string name = nom;
        List<string> biomes = Dict_Continent_Biomes[nom];
        int EducationLevel = 0;
        List<Carte> PileFaceCachee = PileCacheeEnFonctionDuContinent();
    }

    public List<Carte> PileCacheeEnFonctionDuContinent()
    {
        //TODO - Ouverture d'un fichier JSON/Exel en fonction du nom du continent et créant la pile de cartes. Pour cela, il faut simplement avoir besoin du nom de la carte
        //Cette fonction permettra aussi d'instancier des cartes
        return null;
    }
}
