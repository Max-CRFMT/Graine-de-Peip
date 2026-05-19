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
    public Color Invisible = new Color(1f, 1f, 1f, 0f);
    public Color Visible = new Color(1f, 1f, 1f, 1f);


    public List<List<Carte>> listeDeListes = new()
    {
        new List<Carte>(),
        new List<Carte>(),
        new List<Carte>(),
    };    

    public bool contient;

    public bool VerificationPresence(string CarteAVerifier, string carte)
    {
        return CarteAVerifier == carte;
    }

    public void AjouterCarteAuDebut(Carte CarteARajouter)
    {
        contient = false;
        foreach (var liste in listeDeListes)
        {
            foreach (var carte in liste)
            {
                if (VerificationPresence(carte.nom, CarteARajouter.nom))
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

        UpdateUIJoueur();
    }

    public void UpdateUIJoueur()
    {
        Banque1 = GameObject.Find("Banque1");
        Banque2 = GameObject.Find("Banque2");
        Banque3 = GameObject.Find("Banque3");

        if (listeDeListes[0].Count() != 0)
        {
            Sprite new_sprite = Resources.Load<Sprite>(listeDeListes[0][0].PetitPathImage);
            Image image = Banque1.GetComponent<Image>();
            image.sprite = new_sprite;
            image.color = Visible;

        } else
        {
            Banque1.GetComponent<Image>().color = Invisible;
        }

        if (listeDeListes[1].Count() != 0)
        {
            Sprite new_sprite = Resources.Load<Sprite>(listeDeListes[1][0].PetitPathImage);
            Image image = Banque2.GetComponent<Image>();
            image.sprite = new_sprite;
            image.color = Visible;
        } else
        {
            Banque2.GetComponent<Image>().color = Invisible;
        }

        if (listeDeListes[2].Count() != 0)
        {
            Sprite new_sprite = Resources.Load<Sprite>(listeDeListes[2][0].PetitPathImage);
            Image image = Banque3.GetComponent<Image>();
            image.sprite = new_sprite;
            image.color = Visible;
        } else
        {
            Banque3.GetComponent<Image>().color = Invisible;
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