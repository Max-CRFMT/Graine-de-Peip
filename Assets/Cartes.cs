using System.Collections.Generic;
using UnityEngine;

public class Carte
{
    public string nom;
    public string biome;
    public string PathImage;
    public bool stockable;
    public int effectif_total;
    public Carte(string nom, string biome, bool stockable, int effectiftotal)
    {
        this.nom = nom;
        this.biome = biome;
        this.PathImage = "Assets/data/image_carte/" + nom + ".png";
        this.stockable = stockable;
        this.effectif_total = effectiftotal;
    }
    public override string ToString()
    {
        return $"(nom={nom}, biome={biome}, PathImage={PathImage}, stockable={stockable}, effectif total={effectif_total})";
    }
}
