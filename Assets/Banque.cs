using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Banque //: MonoBehaviour
{
    public Queue<Cartes> FileDeCartes = new Queue<Cartes>();
    public void TestCode()
    {
        Cartes Fleur1 = new Cartes("1", true, "Vallée", 3);
        Cartes Fleur2 = new Cartes("2", true, "Montagne", 5);
        Cartes Fleur3 = new Cartes("3", true, "Plage", 1);
        Cartes Fleur4 = new Cartes("4", true, "Gravier", 46);

        //Debug.Log(Cartes.nombredecartes);

        if (FileDeCartes.Count <= 3)
        {
            FileDeCartes.Enqueue(Fleur1);
        }
        else
        {
            FileDeCartes.Dequeue();
        }
        Debug.Log(FileDeCartes.Count);
    }
    
}

public class Cartes
{
    public static int nombredecartes;
    public Cartes(string name, bool boolstockable, string typebiome, int effectiftotal)
    {
        string nom = name;
        bool stockable = boolstockable;
        string biome = typebiome;
        string PathImage = Application.persistentDataPath + "\assets ou un truc du genre" + nom + ".png";
        int effectif_total = effectiftotal;
        nombredecartes++;
    }
}