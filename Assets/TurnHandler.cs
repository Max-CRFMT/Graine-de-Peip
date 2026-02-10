using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    public static TurnHandler Instance;
    public bool FinTour = false;

    private void Awake()
    {
        Instance = this;
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
        //TODO - Doit piocher une carte évènement et appliquer ce dernier
        //PiocherCarteEvenement();
        //AppliquerEvenement();
    }

    public void TempsDeDiscussion()
    {
        //TODO - Doit bloquer les commandes pendant 5min et afficher un tableau récapitulatif des stats/missions des joueurs
        //Doit aussi proposer de mettre fin au temps de discussion pour passer à la suite de la partie
        //
    }

    public void ChangementTourJoueur(Player joueur_suivant)
    {
        //TODO - Doit s'occuper de tout ce qui est changement du tour d'un joueur à l'autre, nécéssitera beaucoup de fonction sous-jacentes (Faire apparaitre de l'UI et bouger la caméra)
    }
    
    public void FinDeTour()
    {
        //TODO - Doit s'occuper de tout ce qui précède le changement de tour, nécéssitera aussi des fonctions sous-jacentes (suppression de l'UI)
    }
    public void ChangeEtatTour()
    {
        // Se base sur un bouton "Fin de Tour" sur lequel il faudra appuyer pour activer cette fonction
        FinTour=true;
    }

    

    public void RoundComplet()
    {
        RajouterAToutLesJoueursPiecesMissionEct();
        Evenement();
        TempsDeDiscussion();

        foreach (Player joueur in GameLogic.instance.Liste_Joueurs)
        {
            ChangementTourJoueur(joueur);
            if (FinTour == true)
            {
                FinTour = false;
                FinDeTour();
            }
        }
    }
}
