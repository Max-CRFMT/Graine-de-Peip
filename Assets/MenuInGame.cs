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

    public static MenuInGame instance;
    public MenuInGame() { }
    private void Awake()
    {
        instance = this;
    }
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
        
        TurnHandler.instance.ChangementClicableBoutonSelonTags(false, new List<string>(){"UIJoueur", "TimerDIscution" }, "UIJoueur");

        TurnHandler.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(true, new List<string>(){"MenuPause"}, "UIJoueur", 0);
        TurnHandler.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(true, new List<string>(){"MenuPause"}, "UIJoueur", 1);
        TurnHandler.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(true, new List<string>(){"MenuPause"}, "UIJoueur", 2);

    }

    public void EnleverMenuIngame()
    {
        Debug.Log("Desafficher activé");

        TurnHandler.instance.ChangementClicableBoutonSelonTags(true, new List<string>(){"UIJoueur", "TimerDIscution" }, "UIJoueur");

        TurnHandler.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(false, new List<string>() { "MenuPause" }, "UIJoueur", 0);
        TurnHandler.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(false, new List<string>() { "MenuPause" }, "UIJoueur", 1);
        TurnHandler.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(false, new List<string>() { "MenuPause" }, "UIJoueur", 2);
    }
}
