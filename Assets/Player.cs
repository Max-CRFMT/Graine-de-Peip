using JetBrains.Annotations;
using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

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

    public List<int> Liste_prix_ouvrier = new List<int>(){12, 8, 6, 4};

    public Player(string name, int coins, string name_map)
    {
        pseudo = name;
        pieces = coins;
        map_choisie = name_map;
        Points_Action_Max = 3;
        
        Points_Action = Points_Action_Max;
        ThuneARecolterDebutTourProchain = 0;
        
        OuvrierAchete = false;
        //Liaison de classe
        continent = new Continent(map_choisie);
        Debug.Log("Pseudo du joueur : " + pseudo + "\nPieces du joueur : " + pieces + "\nMap que le joueur à choisi :" + continent.name);
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
        ChangementUITextJoueur.instance.ChangePointsActionJoueur();
    }

    public void RemplirPointAction()
    {
        Points_Action = Points_Action_Max;
        ChangementUITextJoueur.instance.ChangePointsActionJoueur();
    }

    public void RetirerPointAction(int nb)
    {
        Points_Action -= nb;
        ChangementUITextJoueur.instance.ChangePointsActionJoueur();
    }

    public void RajouterPointActionMax()
    {
        RetirerPieces(prix_ouvrier);
        Points_Action_Max++;
        ChangementUITextJoueur.instance.ChangePointsActionJoueur();
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

    public void Don(int montant_don, Player player_cible)
    {
        int prix_don = 1;
        if (VerifMontant(1))
        {
            Debug.Log("Function Don exec");
            //player_cible.RajouterPieces(montant_don);
        }
        else
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }
    }

    public void Restauration()
    {
        int prix_restauration = 2;
        //mm continent ou pas determiner
        if (VerifMontant(prix_restauration))
        {
            Debug.Log("Function restauration exec");
            //Bien galère
        }
        else
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }
    }

    public void Controle()
    {
        int prix_controle = 1;
        //mm continent ou pas determiner
        if (VerifMontant(prix_controle))
        {
            Debug.Log("Function controle exec");
            //ecrire la fonction
        }
        else
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }
    }

    public void Eduquer()
    {
        if (VerifMontant(3))
        {
            Debug.Log("Function eduquer exec");
            //continent.EducationLevel += 1
            //prix_ouvrier = Liste_prix_ouvrier[continent.EducationLevel];
        }
        else
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }

    }

    public void Recruter_Ouvrier()
    {
        if (VerifMontant(prix_ouvrier))
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
        int prix_recensement = 1;
        //Condition pour déterminer si c'est un autre continent
        if (VerifMontant(prix_recensement))
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
        int prix_recolte = 1;
        //Condition pour déterminer si c'est un autre continent
        if (VerifMontant(prix_recolte))
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
        int prix_jardin = 4;
        if (VerifMontant(prix_jardin))
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
