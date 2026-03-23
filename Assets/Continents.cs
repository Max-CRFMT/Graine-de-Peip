using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Continent
{
    //public List<SpeciesStack> SocleSpeciesStacks; //Socle contenant plusieurs piles d'espèces.
    public List<string> ListSpeciesName; //Liste de string ayant le nom des espèces présentes dans le continent. Importé du ficheir excel.
    public Jardin jardin;
    public Banque banque;
    public int EducationLevel;
    public string name;

    Dictionary<string, List<string>> Dict_Continent_Biomes = new Dictionary<string, List<string>>()
    {
        {"Europe", new List<string>(){"Bretagne", "Paris"} },
        {"Afrique", new List<string>(){"Mali", "Maroc"} },
        {"Asie", new List<string>(){"Japon", "Cor�e"} },
        {"Oc�anie", new List<string>(){"Iles", "Australie" } },
        {"Am�rique du Sud", new List<string>(){"Bresil", "Argentine"} },
        {"Am�rique du Nord", new List<string>(){"Canada", "US" } },
    };
    public Continent(string nom)
    {
        name = nom;
        List<string> biomes = Dict_Continent_Biomes[nom];
        EducationLevel = 0;
        List<Carte> PileFaceCachee = PileCacheeEnFonctionDuContinent();
        //List<SpeciesStack> SocleSpeciesStacks = new List<SpeciesStack>(); //Socle contenant plusieurs piles d'espèces.

        //liason de classe
        banque = new Banque();
        jardin = new Jardin(name);
    }

    public List<Carte> PileCacheeEnFonctionDuContinent()
    {
        //TODO - Ouverture d'un fichier JSON/Exel en fonction du nom du continent et cr�ant la pile de cartes. Pour cela, il faut simplement avoir besoin du nom de la carte
        //Cette fonction permettra aussi d'instancier des cartes
        return null;
    }
    public void SocleMaker()
    {
        //Cette fonction a pour but de creer l'ensemble de liste de pile de graines en fonction du continent.
        //foreach (string SpeciesName in ListSpeciesName)
        //{
        //    SocleSpeciesStacks.Add(new SpeciesStack(SpeciesName));
        //}
    }
}
