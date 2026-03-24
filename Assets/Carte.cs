using System.Collections.Generic;
using UnityEngine;

public class Carte
{
    public string nom;
    public string biome;
    public string PathImage;
    public bool conservable;
    public int vitesse;
    public Carte(string nom, string biome, bool conservable, int vitesse)
    {
        this.nom = nom;
        this.biome = biome;
        this.PathImage = "Assets/data/image_carte/" + nom + ".png";
        this.conservable = conservable;
        this.vitesse = vitesse;
    }
    public override string ToString()
    {
        return $"(nom={nom}, biome={biome}, PathImage={PathImage}, conservable={conservable}, vitesse={vitesse})";
    }
}
