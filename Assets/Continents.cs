using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Continent
{
    public List<string> ListSpeciesName; //Liste de string ayant le nom des espèces présentes dans le continent. Importé du ficheir excel.
    public Jardin jardin;
    public Banque banque;
    public int EducationLevel;
    public string name;
    public List<Carte> pileFaceCachee;
    public List<Carte> defausse;
    public Continent(string nom)
    {
        name = nom;
        EducationLevel = 0;

        pileFaceCachee = new List<Carte>();
        
        //List<SpeciesStack> SocleSpeciesStacks = new List<SpeciesStack>(); //Socle contenant plusieurs piles d'espèces.

        //liason de classe
        banque = new Banque();
        jardin = new Jardin(name);
    }
}
