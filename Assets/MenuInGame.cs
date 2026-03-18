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
        
        TurnHandler.instance.ChangementClicableBoutonSelonTags(false, new List<string>(){"UIJoueur"}, "UIJoueur");

        TurnHandler.instance.ChangementActiveBoutonSelonTags(true, new List<string>(){"MenuPause"}, "UIJoueur");
        TurnHandler.instance.ChangementActiveRawImageSelonTags(true, new List<string>(){"MenuPause"}, "UIJoueur");
        TurnHandler.instance.ChangementActiveTexteSelonTags(true, new List<string>(){"MenuPause"}, "UIJoueur");

    }

    public void EnleverMenuIngame()
    {
        Debug.Log("Desafficher activé");

        TurnHandler.instance.ChangementClicableBoutonSelonTags(true, new List<string>(){"UIJoueur"}, "UIJoueur");

        TurnHandler.instance.ChangementActiveBoutonSelonTags(false, new List<string>(){"MenuPause"}, "UIJoueur");
        TurnHandler.instance.ChangementActiveRawImageSelonTags(false, new List<string>(){"MenuPause"}, "UIJoueur");
        TurnHandler.instance.ChangementActiveTexteSelonTags(false, new List<string>(){"MenuPause"}, "UIJoueur");
    }
}
