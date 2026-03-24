using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Banque
{
    public Queue<Cartes> FileDeCartes = new Queue<Cartes>();
    public int indice_carte = 0;
    public void Regulation_cartes(int indice_carte)
    {
        Cartes NouvelleCarte = new Cartes(indice_carte.ToString(), true, "Montagne", 5);

        if (FileDeCartes.Count < 3)
        {
            FileDeCartes.Enqueue(NouvelleCarte);
        }
        else if (FileDeCartes.Count == 3)
        {
            FileDeCartes.Enqueue(NouvelleCarte);
            FileDeCartes.Dequeue();
        }
        else
        {
            FileDeCartes.Dequeue();
        }
        Debug.Log("Nombre de Cartes dans la banque :" + FileDeCartes.Count);
    }
    public void plus_une_carte()
    {
        Regulation_cartes(indice_carte);
        indice_carte++;
    }
    public void moins_une_carte()
    {
        FileDeCartes.Dequeue();
        Debug.Log("Nombre de Cartes dans la banque :" + FileDeCartes.Count);
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