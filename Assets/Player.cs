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
        RetirerPieces(Liste_prix_ouvrier[continent.EducationLevel]);
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
        int montantSubvention = 1;
        if (VerifMontant(montantSubvention))
        {
            Debug.Log("Function demandeSubventions exec");
            pieces += 2;
        }
        else
        {
            Debug.Log("Pas assez de thune rip bozo");
        }
    }

    public void Don(int montant_don, Player player_source, Player player_cible)
    {
        int prix_don = 1;
        if (VerifMontant(prix_don))
        {
            Debug.Log("Function Don exec");
            player_cible.RajouterPieces(montant_don);
            player_source.RetirerPieces(montant_don + prix_don);
            TurnHandler.instance.indice_player_cible_don++;
            TurnHandler.instance.indice_liste_montant_don++;
        }
        else
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }
    }

    public void RestaurationBanque(Carte carte_selectionnee)
    {
        Debug.Log("Function restauration Banque exec");
        foreach (Player player in GameLogic.instance.Liste_Joueurs)
        {
            if (player.continent.name == carte_selectionnee.continent_name)
            {
                //Ajouter dans la pile face pas cachée la carte
                foreach (Carte carte in continent.banque.FileDeCartes)
                {
                    if (carte_selectionnee.nom == carte.nom)
                    {
                        continent.banque.RemoveCard(continent.banque.FileDeCartes, carte_selectionnee);
                    }
                }
            }
        }

    }
    public void Restauration(char name, Carte carte_selected)
    {
        int prix_restauration = 2;
        if (continent.name != carte_selected.continent_name)
        {
            prix_restauration = 3;
        }
        if (VerifMontant(prix_restauration))
        {
            if (name == 'B')
            {
                RestaurationBanque(carte_selected);
            }
            else if (name == 'J')
            {
                RestaurationJardin(carte_selected);
            }
            TurnHandler.instance.indice_JB_restauration++;
            TurnHandler.instance.indice_carte_selected_restauration++;
        }
        else
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }

    }
    public void RestaurationJardin(Carte carte_selectionnee)
    {
        Debug.Log("Function restauration Jardin exec");
        //Compost.retirer(Carte)
        foreach (Player player in GameLogic.instance.Liste_Joueurs)
        {
            if (player.continent.name == carte_selectionnee.continent_name)
            {
                player.continent.pileFaceCachee.Add(carte_selectionnee);
                GameLogic.instance.ShuffleListeCartes(player.continent.pileFaceCachee);
            }
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
        if (VerifMontant(Liste_prix_ouvrier[continent.EducationLevel]))
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

    public void RecencerGraines(Player cible_recensement)
    {
        int prix_recensement = 1;
        if (cible_recensement.continent.name != continent.name)
        {
            prix_recensement = 2;
        }
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
        TurnHandler.instance.indice_liste_player_cible_recensement++;
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
