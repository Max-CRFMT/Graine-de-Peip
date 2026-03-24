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

    public Continent(string nom)
    {
        name = nom;
        int EducationLevel = 0;

        List<Carte> pileFaceCachee = new List<Carte>();
        
        //List<SpeciesStack> SocleSpeciesStacks = new List<SpeciesStack>(); //Socle contenant plusieurs piles d'espèces.

        //liason de classe
        banque = new Banque();
        jardin = new Jardin(name);
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
