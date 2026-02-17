using UnityEngine;
using System.Collections.Generic;

public class TurnHandler : MonoBehaviour
{
    public static TurnHandler instance;
    public bool FinTour = false;
    public Dictionary<Player, List<int>> Dico_JoueurActions;
    public Player PlayerActuel;
    public TurnHandler() { }

    private void Awake()
    {
        instance = this;
        instance.Dico_JoueurActions = new Dictionary<Player, List<int>>();
    }

    public void RajouterAToutLesJoueursPiecesMissionEct()
    {
        foreach (Player player in GameLogic.instance.Liste_Joueurs)
        {
            player.MiseAJourDebutTourPieces();
        }
    }

    public void Evenement()
    {
        //TODO - Doit piocher une carte �v�nement et appliquer ce dernier
        //PiocherCarteEvenement();
        //AppliquerEvenement();
    }

    public void TempsDeDiscussion()
    {
        //TODO - Doit bloquer les commandes pendant 5min et afficher un tableau r�capitulatif des stats/missions des joueurs
        //Doit aussi proposer de mettre fin au temps de discussion pour passer � la suite de la partie
        //
    }

    public void ChangementTourJoueur(Player joueur_suivant)
    {
        //TODO - Doit s'occuper de tout ce qui est changement du tour d'un joueur � l'autre, n�c�ssitera beaucoup de fonction sous-jacentes (Faire apparaitre de l'UI et bouger la cam�ra)
    }
    
    public void FinDeTour()
    {
        //TODO - Doit s'occuper de tout ce qui pr�c�de le changement de tour, n�c�ssitera aussi des fonctions sous-jacentes (suppression de l'UI)
    }
    public void ChangeEtatTour()
    {
        // Se base sur un bouton "Fin de Tour" sur lequel il faudra appuyer pour activer cette fonction
        FinTour=true;
    }

    public void EffectuerActions()
    {
        Dictionary<Player, List<int>>.KeyCollection keys = Dico_JoueurActions.Keys; 
        for (int i = 1; i < 7; i++)
        {
            foreach (Player key in keys)
            {
                Debug.Log(key);
            }
        }
        //Une fois que les actions sont effectuées
        instance.Dico_JoueurActions = new Dictionary<Player, List<int>>();
    }

    public void AjouterActionDansDicoJoueursAction(int action)
    {
        //Faut que al fonction soit appellée par une autre fonction 
        //Si le nom du joueur est dans déjà dans les clées du dico, on rajoute l'action
        if (instance.Dico_JoueurActions.ContainsKey(PlayerActuel))
        {
            instance.Dico_JoueurActions[PlayerActuel].Add(action);
        }
        //Si le nom du joueur n'est pas déjà dans les clées du dico, on ajoute le joueur et l'action
        else
        {
            instance.Dico_JoueurActions.Add(PlayerActuel, new List<int>(){action});
        }
    }

    
    public void RoundComplet()
    {
        RajouterAToutLesJoueursPiecesMissionEct();
        Evenement();
        TempsDeDiscussion();

        foreach (Player joueur in GameLogic.instance.Liste_Joueurs)
        {
            Player PlayerActuel = joueur;
            ChangementTourJoueur(PlayerActuel);
            if (FinTour == true)
            {
                FinTour = false;
                FinDeTour();
            }
        }
        EffectuerActions();
    }
}
