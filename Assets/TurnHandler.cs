using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Timers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    public static TurnHandler instance;
    public bool FinTour;
    public bool FinDiscution;
    public Dictionary<Player, List<PlayerAction>> Dico_JoueurActions;
    public Player PlayerActuel;
    public GameObject TxtCountdown;
    public GameObject ButtonCountdown;
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
        instance.FinDiscution = false;
        instance.PlayerActuel = new Player("John", 0, "Fate");
    }
    public void Creationlisteevenement()
    {
        string filePath = Application.dataPath + "Assets/data/tableau_event.csv";
        using (StreamReader reader = new StreamReader(filePath))
        {
            reader.ReadLine();
        }
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

    public IEnumerator TempsDeDiscussion()
    {
        //TODO - Doit bloquer les commandes pendant 5min et afficher un tableau r�capitulatif des stats/missions des joueurs (faut que le tableau récapitulatif ait le tag TimerDiscution)
        Canvas canvas = FindAnyObjectByType<Canvas>();
        GameObject TexteCountDown = Instantiate(TxtCountdown, canvas.transform);
        GameObject BoutonCountdown = Instantiate(ButtonCountdown, canvas.transform);

        yield return new WaitUntil(() => instance.FinDiscution);
    }

    public void MasquerUIJoueur()
    {
        Canvas canvas = FindAnyObjectByType<Canvas>();
        UnityEngine.UI.Button[] gameobjects = canvas.GetComponentsInChildren<UnityEngine.UI.Button>(true);
        foreach (UnityEngine.UI.Button go in gameobjects)
        {
            if (go.gameObject.tag == "UIJoueur")
            {
                go.gameObject.SetActive(false);
            }

        }

        TextMeshProUGUI[] gameobjectsse = canvas.GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach (TextMeshProUGUI go in gameobjectsse)
        {
            if (go.gameObject.tag == "UIJoueur")
            {
                go.gameObject.SetActive(false);
            }

        }
    }

    public void ReafficherUI()
    {
        Canvas canvas = FindAnyObjectByType<Canvas>();
        UnityEngine.UI.Button[] gameobjects = canvas.GetComponentsInChildren<UnityEngine.UI.Button>(true);
        foreach (UnityEngine.UI.Button go in gameobjects)
        {
            if (go.gameObject.tag == "UIJoueur")
            {
                go.gameObject.SetActive(true);
            }
        }

        TextMeshProUGUI[] gameobjectsse = canvas.GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach (TextMeshProUGUI go in gameobjectsse)
        {
            if (go.gameObject.tag == "UIJoueur")
            {
                go.gameObject.SetActive(true);
            }

        }
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
        if (PresenceKeyJoueur(PlayerActuel))
        {
            instance.Dico_JoueurActions[PlayerActuel].Add(action);
        }
        //Si le nom du joueur n'est pas déjà dans les clées du dico, on ajoute le joueur et l'action
        else
        {
            instance.Dico_JoueurActions.Add(PlayerActuel, new List<PlayerAction>(){action});
        }
    }

    public bool PresenceKeyJoueur(Player player)
    {
        return instance.Dico_JoueurActions.ContainsKey(player);
    }

    public void AnnulerDerniereAction()
    {
        if (PresenceKeyJoueur(PlayerActuel) && (instance.Dico_JoueurActions[PlayerActuel].Count != 0))
        {
            Debug.Log("Action Removed");
            instance.PlayerActuel.Points_Action += 1 ;
            instance.Dico_JoueurActions[instance.PlayerActuel].RemoveAt(instance.Dico_JoueurActions[instance.PlayerActuel].Count() - 1);
        } 
        else
        {
            Debug.Log("Aucune action demandee, il n'y a rien à retirer");
        }
    }
    
    public void AfficherActionsJoueurActuel()
    {
        if ((instance.Dico_JoueurActions.ContainsKey(PlayerActuel)) && (instance.Dico_JoueurActions[PlayerActuel].Count != 0))
        {
            foreach (PlayerAction actions in instance.Dico_JoueurActions[PlayerActuel])
            {
                Debug.Log(actions);
            }
        }
        else
        {
            Debug.Log("Il n'y a pas d'actions a enlever, je joueur n'a aucune action enregistre");
        }
    }

    public IEnumerator TourJoueur()
    {
        //On veut que le tour se bloque tant que je joueur n'a pas appuyé sur le bouton qui passe son tour
        yield return new WaitUntil(() => instance.FinTour);
    }

    public void RemplirPtActionsChaqueJoueur()
    {
        foreach (Player player in GameLogic.instance.Liste_Joueurs)
        {
            player.RemplirPointAction();
        }
    }

    public void EvolutionPhaseVegetale()
    {
        //Faire évoluer la phase végétale : 
        //Si il y a plus de X cartes de même variété dans un biome ou environnement, ajouter % cartes graines dans cette zone.
        //Si il y a X cartes ou moins d’une même variété, l’espèce disparaît dans cet environnement.Retirer toutes les cartes présentes dans cette zone.
    }

    
    public IEnumerator RoundComplet()
    {
        //RajouterAToutLesJoueursPiecesMissionEct();
        //Evenement();
        MasquerUIJoueur();
        yield return StartCoroutine(TempsDeDiscussion());
        instance.FinDiscution = false;
        ReafficherUI();
        RemplirPtActionsChaqueJoueur();

        foreach (Player joueur in GameLogic.instance.Liste_Joueurs)
        {
            instance.FinTour = false;
            instance.PlayerActuel = joueur;
            ChangementUITextJoueur.instance.ChangerChangementJoueur();
            

            Debug.Log("Le joueur actuel est :" + PlayerActuel.pseudo);

            //ChangementTourJoueur(PlayerActuel);

            yield return StartCoroutine(TourJoueur());
        }
        EffectuerActions();
    }
}
