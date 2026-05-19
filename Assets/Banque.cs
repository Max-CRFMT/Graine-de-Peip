using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Banque
{

    public GameObject Banque1;
    public GameObject Banque2;
    public GameObject Banque3;

    public List<List<Carte>> listeDeListes = new()
    {
        new List<Carte>(),
        new List<Carte>(),
        new List<Carte>(),
    };    

    public bool VerificationPresence(string CarteAVerifier, string carte)
    {
        return CarteAVerifier == carte;
    }

    public bool contient;

    public void AjouterCarteAuDebut(Carte CarteARajouter)
    {
        contient = false;
        foreach (var liste in listeDeListes)
        {
            foreach (var carte in liste)
            {
                if (VerificationPresence(carte, CarteARajouter))
                {
                    contient = true;
                }
            }
        }
        if (contient == false) 
        {
            listeDeListes[0].Add(CarteARajouter);
        }
    }

    public void Regulation_cartes()
    {
        listeDeListes[2].Clear();
        listeDeListes[2].AddRange(listeDeListes[1]);

        listeDeListes[1].Clear();      
        listeDeListes[1].AddRange(listeDeListes[0]);

        listeDeListes[0].Clear();     

        Debug.Log(listeDeListes[0].Count());
        Debug.Log(listeDeListes[1].Count());
        Debug.Log(listeDeListes[2].Count());

        Remontage1Carte(listeDeListes[1][0]);
        Debug.Log("MAcrton");
        UpdateUIJoueur();
    }

    public void UpdateUIJoueur()
    {
        Banque1 = GameObject.Find("Banque1");
        Banque2 = GameObject.Find("Banque2");
        Banque3 = GameObject.Find("Banque3");

        Debug.Log(listeDeListes[0].Count());
        Debug.Log(listeDeListes[1].Count());
        Debug.Log(listeDeListes[2].Count());

        if (listeDeListes[0].Count() != 0)
        {
            Debug.Log("Diff 0");
        } else {
            Debug.Log("0");
        }

        if (listeDeListes[1].Count() != 0)
        {
            Debug.Log("Diff 0");
        } else {
            Debug.Log("0");
        }

        if (listeDeListes[2].Count() != 0)
        {
            Debug.Log("Diff 0");
        } else {
            Debug.Log("0");
        }
    }

    public void Remontage1Carte(string CarteARemonter)
    {
        foreach (var liste in listeDeListes)
        {
            foreach (var carte in liste)
            {
                if (VerificationPresence(carte.nom, CarteARemonter))
                {
                    Debug.Log("ici");
                    listeDeListes[0].Add(carte);
                    Debug.Log("c passe");
                    liste.Remove(carte);
                    Debug.Log("Heee Heee");
                    return;
                }
            }
        }
    }
}