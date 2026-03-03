using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Player
{
    public string pseudo;
    public int pieces;
    public string map_choisie;

    public int prix_ouvrier;
    public int Points_Action_Max;
    public int Points_Action;

    public int ThuneARecolterDebutTourProchain;

    public Continent continent;

    public bool OuvrierAchete;

    public Player(string name, int coins, string name_map)
    {
        pseudo = name;
        pieces = coins;
        map_choisie = name_map;
        Points_Action_Max = 3;
        prix_ouvrier = 1000;
        Points_Action = Points_Action_Max;
        ThuneARecolterDebutTourProchain = 0;
        OuvrierAchete = false;
        Debug.Log("Pseudo du joueur : " + pseudo + "\nPieces du joueur : " + pieces + "\nMap que le joueur à choisi :" + name_map + "\n\n");
    }

    public void SetContinent()
    {
        //TODO - Modifier cette méthode pour attribuer un élément de la classe continent (et si il a des spécificités ça peut être pire)
        continent = continent;
    }

    public bool VerifMontant(int nb)
    {
        return pieces >= nb;
    }

    public bool VerifPointAction(int nb)
    {
        return Points_Action >= nb;
    }

    public void RajouterPointAction(int nb)
    {
        Points_Action += nb;
    }

    public void RemplirPointAction()
    {
        Points_Action = Points_Action_Max;
    }

    public void RetirerPointAction(int nb)
    {
        Points_Action -= nb;
    }

    public void RajouterPointActionMax()
    {
        RetirerPieces(prix_ouvrier);
        Points_Action_Max++;
    }

    public void RajouterPieces(int nb)
    {
        pieces += nb;
    }

    public void RetirerPieces(int nb)
    {
        pieces -= nb;
    }

    public void DemandeSubventions()
    {
        Debug.Log("Function demandeSubventions exec");
        ThuneARecolterDebutTourProchain += 1;
    }

    public void Eduquer()
    {
        if (VerifMontant(0))
        {
            Debug.Log("Function eduquer exec");
            //continent.CompteurEducation += 1
        }
        else
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }

    }

    public void Recruter_Ouvrier()
    {
        if (VerifMontant(0))
        {
            Debug.Log("Function recruter ouvrier exec");
            RajouterPointActionMax();
            OuvrierAchete = true;
            Debug.Log("Maintenant, pt actions max = " + Points_Action_Max);
        }
        else
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }

    }

    public void RecencerGraines()
    {
        if (VerifMontant(0))
        {
            Debug.Log("Function recenser exec");
            //TODO
        }
        else
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }
    }

    public void RecolterGraines()
    {
        if (VerifMontant(0))
        {
            Debug.Log("Function recolter exec");
            //TODO
        }
        else
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }
    }

    public void AmeliorerJardin()
    {
        if (VerifMontant(0))
        {
            Debug.Log("Function ameliorer exec");
            //TODO
        }
        else
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }
    }

    public void MiseAJourDebutTourPieces()
    {
        pieces += ThuneARecolterDebutTourProchain;
        ThuneARecolterDebutTourProchain = 0;
    }
}
