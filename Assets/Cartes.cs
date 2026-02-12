using UnityEngine;

public class Carte
{

    public Carte(string name, bool boolstockable, string typebiome, int effectiftotal)
    {
        string nom = name;
        bool stockable = boolstockable;
        string biome = typebiome;
        string PathImage = Application.persistentDataPath + "\assets ou un truc du genre" + nom + ".png";
        int effectif_total = effectiftotal;
    }
}
