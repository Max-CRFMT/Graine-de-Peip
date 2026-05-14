using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Banque
{
    public Queue<Carte> FileDeCartes = new Queue<Carte>();
    public int indice_carte = 0;
    public void Regulation_cartes(int indice_carte)
    {
        Carte NouvelleCarte = new Carte(indice_carte.ToString(), "Montagne", true, 0, 1, "jsp", "Continent");

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
    public void RemoveCard(Queue<Carte> queue, Carte carte_a_remove)
    {
        queue = new Queue<Carte>(queue.Where(x => x != carte_a_remove));
    }
}