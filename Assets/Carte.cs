using System.Collections.Generic;
using UnityEngine;

public class Carte
{
    public string nom;
    public string biome;
    public string PathImage;
    public bool conservable;
    public int effectif;
    public int vitesse;
    public string rarete;
    public string continent_name;
    public string PetitPathImage;
    public Carte(string nom, string biome, bool conservable, int effectif, int vitesse, string rarete, string continent_name)
    {
        this.nom = nom;
        this.biome = biome;
        this.PathImage = "Assets/data/image_carte/" + nom + ".png";
        this.conservable = conservable;
        this.vitesse = vitesse;
        this.effectif = effectif;
        this.rarete = rarete;
        this.continent_name = continent_name;
        this.PetitPathImage = "Cards/" + GameLogic.instance.Dico_traduction_useless[continent_name] + "/" + GameLogic.instance.Dico_NomCarte_Attache[nom];
    }
    public override string ToString()
    {
        return $"(nom={nom}, biome={biome}, PathImage={PathImage}, conservable={conservable}, " +
            $"vitesse={vitesse},effectif_total={effectif},rarete={rarete},continent={continent_name})";
    }
}
