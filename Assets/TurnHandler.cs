using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using System.Collections;

public class TurnHandler : MonoBehaviour
{
    public static TurnHandler instance;
    public bool FinTour;
    public Dictionary<Player, List<PlayerAction>> Dico_JoueurActions;
    public Player PlayerActuel;
    public TurnHandler() { }

    public enum PlayerAction
    {
        Subventions,
        Eduquer,
        Recruter,
        Recenser,
        Recolter,
        Ameliorer
    }

    public Dictionary<PlayerAction, Action<Player>> Dico_Actions = new()
    {
        {PlayerAction.Subventions, player => player.DemandeSubventions()},
        {PlayerAction.Eduquer, player => player.Eduquer()},
        {PlayerAction.Recruter, player => player.Recruter_Ouvrier()},
        {PlayerAction.Recenser, player => player.RecencerGraines()},
        {PlayerAction.Recolter, player => player.RecolterGraines()},
        {PlayerAction.Ameliorer, player => player.AmeliorerJardin()}
    };

    private void Awake()
    {
        instance = this;
        instance.Dico_JoueurActions = new Dictionary<Player, List<PlayerAction>>();
        instance.FinTour = false;
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
        instance.FinTour = true;
    }

    public void EffectuerActions()
    {
        Dictionary<Player, List<PlayerAction>>.KeyCollection keys = Dico_JoueurActions.Keys; 
        for (int i = 1; i < GameLogic.instance.Liste_Joueurs.Count + 1; i++)
        {
            foreach (Player key in keys)
            {
                foreach (PlayerAction Action in Dico_JoueurActions[key])
                {
                    Dico_Actions[Action](key);
                }
            }
        }
        //Une fois que les actions sont effectuées on supprime la liste pour en créer une nouvelle
        instance.Dico_JoueurActions = new Dictionary<Player, List<PlayerAction>>();
    }

    public void AjouterActionDansDicoJoueursAction(PlayerAction action)
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
            instance.Dico_JoueurActions.Add(PlayerActuel, new List<PlayerAction>(){action});
        }
    }
    
    public IEnumerator TourJoueur()
    {
        //On veut que le tour se bloque tant que je joueur n'a pas appuyé sur le bouton qui passe son tour
        yield return new WaitUntil(() => instance.FinTour);

    }
    
    public IEnumerator RoundComplet()
    {
        //RajouterAToutLesJoueursPiecesMissionEct();
        //Evenement();
        //TempsDeDiscussion();

        foreach (Player joueur in GameLogic.instance.Liste_Joueurs)
        {
            instance.FinTour = false;
            PlayerActuel = joueur;

            Debug.Log("Le joueur actuel est :" + PlayerActuel.pseudo);

            //ChangementTourJoueur(PlayerActuel);

            yield return StartCoroutine(TourJoueur());
        }
        //EffectuerActions();
    }
}
