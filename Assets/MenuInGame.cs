using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


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
        if (touche_echap.WasPerformedThisFrame() && !MenuOptions.instance.MenuOptionsEnCours) //Rajouter condition nomScene = "Game"
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
        else if (touche_echap.WasPerformedThisFrame() && MenuOptions.instance.MenuOptionsEnCours)
        {
            MenuOptions.instance.MasquerMenuOptions();
        }
    }
    public void AfficherMenuInGame()
    {
        Debug.Log("Afficher activé");
        
        ChangementClicableBoutonSelonTags(false, new List<string>(){"UIJoueur", "TimerDIscution" }, "CanvasGUI");

        ChangementActiveBoutonRawImageOuTexteSelonTags(true, new List<string>(){"MenuPause"}, "UIJoueur", 0);
        ChangementActiveBoutonRawImageOuTexteSelonTags(true, new List<string>(){"MenuPause"}, "UIJoueur", 1);
        ChangementActiveBoutonRawImageOuTexteSelonTags(true, new List<string>(){"MenuPause"}, "UIJoueur", 2);

    }

    public void EnleverMenuIngame()
    {
        Debug.Log("Desafficher activé");

        ChangementClicableBoutonSelonTags(true, new List<string>(){"UIJoueur", "TimerDIscution" }, "CanvasGUI");

        ChangementActiveBoutonRawImageOuTexteSelonTags(false, new List<string>() { "MenuPause" }, "UIJoueur", 0);
        ChangementActiveBoutonRawImageOuTexteSelonTags(false, new List<string>() { "MenuPause" }, "UIJoueur", 1);
        ChangementActiveBoutonRawImageOuTexteSelonTags(false, new List<string>() { "MenuPause" }, "UIJoueur", 2);
    }

    public void ChangementActiveBoutonRawImageOuTexteSelonTags(bool vraioufaux, List<string> ListeTags, string tagCanvas, int numerotkt)
    {
        //Detection et définition du canvas dans lequel on va rechercher l'object
        Canvas[] canvas_UI_liste = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
        Canvas canvas_UI = canvas_UI_liste[0];
        foreach (Canvas canvas in canvas_UI_liste)
        {
            if (canvas.tag == tagCanvas)
            {
                canvas_UI = canvas;
            }
        }
        if (numerotkt == 0)
        {
            UnityEngine.UI.Button[] gameobjects = canvas_UI.GetComponentsInChildren<UnityEngine.UI.Button>(true);
            foreach (string tag in ListeTags)
            {
                foreach (UnityEngine.UI.Button go in gameobjects)
                {
                    if (go.gameObject.tag == tag)
                    {
                        go.gameObject.SetActive(vraioufaux);
                    }
                }
            }
        }
        else if (numerotkt == 1)
        {
            UnityEngine.UI.RawImage[] gameobjects = canvas_UI.GetComponentsInChildren<UnityEngine.UI.RawImage>(true);
            foreach (string tag in ListeTags)
            {
                foreach (UnityEngine.UI.RawImage go in gameobjects)
                {
                    if (go.gameObject.tag == tag)
                    {
                        go.gameObject.SetActive(vraioufaux);
                    }
                }
            }
        }
        else if (numerotkt == 2)
        {
            TMPro.TextMeshProUGUI[] gameobjects = canvas_UI.GetComponentsInChildren<TMPro.TextMeshProUGUI>(true);
            foreach (string tag in ListeTags)
            {
                foreach (TMPro.TextMeshProUGUI go in gameobjects)
                {
                    if (go.gameObject.tag == tag)
                    {
                        go.gameObject.SetActive(vraioufaux);
                    }
                }
            }
        }
    }
    public void ChangementClicableBoutonSelonTags(bool vraioufaux, List<string> ListeTags, string tagCanvas)
    {
        if (SceneManager.GetActiveScene().name == "Game" && !TurnHandler.instance.FinDiscution)
        {
            ListeTags.Remove("UIJoueur");
        }
        //Detection et définition du canvas dans lequel on va rechercher l'object
        Canvas[] canvas_UI_liste = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
        Canvas canvas_UI = canvas_UI_liste[0];
        foreach (Canvas canvas in canvas_UI_liste)
        {
            if (canvas.tag == tagCanvas)
            {
                canvas_UI = canvas;
            }
        }

        UnityEngine.UI.Button[] gameobjects = canvas_UI.GetComponentsInChildren<UnityEngine.UI.Button>(true);
        foreach (string tag in ListeTags)
        {
            foreach (UnityEngine.UI.Button go in gameobjects)
            {
                if (go.gameObject.tag == tag)
                {
                    go.gameObject.GetComponent<UnityEngine.UI.Button>().enabled = vraioufaux;
                }
            }
        }
    }
}
