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

    public Player(string name, int coins, string name_map)
    {
        pseudo = name;
        pieces = coins;
        map_choisie = name_map;
        Points_Action_Max = 3;
        prix_ouvrier = 1000;
        ThuneARecolterDebutTourProchain = 0;
        continent = new Continent(name_map);
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
        if (VerifPointAction(1))
        {
            ThuneARecolterDebutTourProchain += 1;
        }
    }

    public void Eduquer()
    {
        if (VerifMontant(50) && VerifPointAction(1))
        {
            //Joueur.Continent.CompteurEducation += 1
            RetirerPieces(50);
        }
    }

    public void Recruter_Ouvrier()
    {
        if (VerifMontant(100) && VerifPointAction(1))
        {
            RajouterPointActionMax();
            RetirerPieces(100);
            //Bool qui dit que le joueur a déjà utilisé ça et donc ne peut plus l'utiliser
        }
    }

    public void RecencerGraines()
    {
        if (VerifMontant(10) && VerifPointAction(1))
        {
            RetirerPieces(10);
            
            // TODO
        }
    }

    public void RecolterGraines()
    {
        if (VerifMontant(20) && VerifPointAction(1))
        {
            RetirerPieces(20);

            //TODO
        }
    }

    public void AmeliorerJardin()
    {
        if (VerifMontant(200) && VerifPointAction(1))
        {
            RetirerPieces(200);
            //TODO
        }
    }

    public void MiseAJourDebutTourPieces()
    {
        pieces += ThuneARecolterDebutTourProchain;
        ThuneARecolterDebutTourProchain = 0;
    }
}
