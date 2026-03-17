using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using System.Collections;
using System.Timers;
using TMPro;
using System.Linq;

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
        Canvas[] canvas_UI_liste = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
        Canvas canvas_UI = canvas_UI_liste[0];
        foreach (Canvas canvas in canvas_UI_liste)
        {
            if (canvas.tag == "UIJoueur")
            {
                canvas_UI = canvas;
            }
        }

        GameObject TexteCountDown = Instantiate(TxtCountdown, canvas_UI.transform);
        GameObject BoutonCountdown = Instantiate(ButtonCountdown, canvas_UI.transform);

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
        MasquerUIJoueur();
        yield return StartCoroutine(TempsDeDiscussion());
        instance.FinDiscution = false;
        ReafficherUI();

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
