using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;

public class MenuInGame : MonoBehaviour
{
    public List<string> Liste_Tags;
    public bool MenuEnCours = false;
    InputAction touche_echap;
    void Start()
    {
        touche_echap = InputSystem.actions.FindAction("Echap");
    }

    void Update()
    {
        if (touche_echap.WasPerformedThisFrame())
        {
            if (MenuEnCours == false)
            {
                MenuEnCours = true;
                AfficherMenuInGame();
            }
            else
            {
                MenuEnCours = false;
                EnleverMenuIngame();
            }  
        }
    }
    public void AfficherMenuInGame()
    {
        Debug.Log("Afficher activé");
        Liste_Tags = new List<string>(){"UIJoueur"};
        TurnHandler.instance.ChangementClicableButonSelonTags(false, Liste_Tags); 

        Canvas[] canvas_UI_liste = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
        Canvas canvas_UI = canvas_UI_liste[0];
        foreach (Canvas canvas in canvas_UI_liste)
        {
            if (canvas.tag == "UIJoueur")
            {
                canvas_UI = canvas;
            }
        }
           
    }

    public void EnleverMenuIngame()
    {
        Debug.Log("Desafficher activé");
        Liste_Tags = new List<string>(){"UIJoueur"};
        TurnHandler.instance.ChangementClicableButonSelonTags(true, Liste_Tags); 

        Canvas[] canvas_UI_liste = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
        Canvas canvas_UI = canvas_UI_liste[0];
        foreach (Canvas canvas in canvas_UI_liste)
        {
            if (canvas.tag == "UIJoueur")
            {
                canvas_UI = canvas;
            }
        }      
    }
}
