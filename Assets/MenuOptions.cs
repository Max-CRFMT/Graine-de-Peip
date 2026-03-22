using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    public bool MenuOptionsEnCours = false;
    public static MenuOptions instance;

    private void Awake()
    {
        instance = this;
    }

    public Canvas ResearchCanvasSelonTag(string tagCanvas)
    {
        //Detection et définition du canvas dans lequel on va rechercher l'object
        Canvas[] canvas_UI_liste = FindObjectsByType<Canvas>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        Canvas canvas_UI = canvas_UI_liste[0];
        foreach (Canvas canvas in canvas_UI_liste)
        {
            if (canvas.tag == tagCanvas)
            {
                canvas_UI = canvas;
            }
        }
        return canvas_UI;
    }

    public void AfficherMenuOptions()
    {
        MenuOptionsEnCours = true;
        ResearchCanvasSelonTag("Options").gameObject.SetActive(true);
        
        
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(false, new List<string>(){"Lobby"}, "Lobby", 0);
            MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(false, new List<string>(){"Lobby"}, "Lobby", 2);            
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(false, new List<string>(){"UIJoueur", "MenuPause", "TexteTimer", "TimerDIscution"}, "UIJoueur", 0);
            MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(false, new List<string>(){"UIJoueur", "MenuPause", "TexteTimer"}, "UIJoueur", 1);
            MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(false, new List<string>(){"UIJoueur", "MenuPause", "TexteTimer"}, "UIJoueur", 2);
        }
    }

    public void MasquerMenuOptions()
    {
        MenuOptionsEnCours = false;
        ResearchCanvasSelonTag("Options").gameObject.SetActive(false);
    
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(true, new List<string>(){"Lobby"}, "Lobby", 0);
            MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(true, new List<string>(){"Lobby"}, "Lobby", 2); 
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            List<string> ListeTags = new List<string>(){"MenuPause", "TexteTimer", "TimerDIscution"};
            if (TurnHandler.instance.FinDiscution == true)
            {
                ListeTags.Add("UIJoueur");
            }
            MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(true, ListeTags, "UIJoueur", 0);
            MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(true, ListeTags, "UIJoueur", 1);
            MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(true, ListeTags, "UIJoueur", 2);
        }
    }
}
