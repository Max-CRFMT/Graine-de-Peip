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
    public Player PlayerActuel;
    public GameObject TxtCountdown;
    public GameObject ButtonCountdown;

    public bool resencement_en_cours;
    public bool recolte_en_cours;
    public bool recolte_pioche_en_cours;


    public TurnHandler() { }

    private void Awake()
    {
        instance = this;


        instance.resencement_en_cours = false;
        instance.recolte_en_cours = false;
        instance.recolte_pioche_en_cours = false;
    }
    
    public void Creationlisteevenement()
    {
        string filePath = Application.streamingAssetsPath + "Assets/data/tableau_event.csv";
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
        MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(false, new List<string>() { "BoutonUIJoueur", "boutonFinTour" }, "UIJoueur", 0);
        MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(false, new List<string>() { "BoutonUIJoueur", "boutonFinTour" }, "CanvasGUI", 0);
        MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(false, new List<string>() { "UIJoueur" }, "UIJoueur", 2);
        MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(false, new List<string>() { "UIJoueur" }, "CanvasGUI", 2);
    }


    public void ReafficherUI()
    {
        MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(true, new List<string>() { "BoutonUIJoueur", "boutonFinTour" }, "UIJoueur", 0);
        MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(true, new List<string>() { "BoutonUIJoueur", "boutonFinTour" }, "CanvasGUI", 0);
        MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(true, new List<string>() { "UIJoueur" }, "UIJoueur", 2);
        MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(true, new List<string>() { "UIJoueur" }, "CanvasGUI", 2);
    }

    public void ChangementTourJoueur(Player joueur_suivant)
    {
        //TODO - Doit s'occuper de tout ce qui est changement du tour d'un joueur � l'autre, n�c�ssitera beaucoup de fonction sous-jacentes (Faire apparaitre de l'UI et bouger la cam�ra)
    }
    
    public void FinDeTour()
    {
        //TODO - Doit s'occuper de tout ce qui precede le changement de tour, necessitera aussi des fonctions sous-jacentes (suppression de l'UI)
        instance.FinTour = true;
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
    
    public IEnumerator RoundComplet()
    {
        RajouterAToutLesJoueursPiecesMissionEct();
        //Evenement();
        MasquerUIJoueur();
        yield return StartCoroutine(TempsDeDiscussion());
        ReafficherUI();
        RemplirPtActionsChaqueJoueur();

        foreach (Player joueur in GameLogic.instance.Liste_Joueurs)
        {
            instance.FinTour = false;
            instance.PlayerActuel = joueur;

            instance.PlayerActuel.continent.banque.Regulation_cartes();
            ChangementUITextJoueur.instance.ChangerChangementJoueur();
            PlayerActuel.continent.jardin.UpdateSpriteJardin();
            

            Debug.Log("Le joueur actuel est :" + PlayerActuel.pseudo);

            //ChangementTourJoueur(PlayerActuel);

            yield return StartCoroutine(TourJoueur());
        }
        instance.FinDiscution = false;
    }
}
