using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class GameLogic : MonoBehaviour
{

    public int nb_joueurs ;
    public string difficulte;
    public static GameLogic instance;

    public GameLogic() { }

    private void Awake()
    {
        instance = this;
        instance.nb_joueurs = 2;
        instance.difficulte = "Facile";
    }

    public void SetNbJoueurs(int nombre)
    {
        nb_joueurs = nombre;
        Debug.Log("Le nombre de joueur sélectionné est :" + nb_joueurs);
    }

    public void SetDifficulte(string difficult)
    {
        difficulte = difficult;
        Debug.Log("La difficulté sélectionnée est :" + difficulte);
    }
}

