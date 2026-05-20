using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class MenuOptions : MonoBehaviour
{
    public bool MenuOptionsEnCours = false;
    public static MenuOptions instance;

    private Slider brightnessSlider;

    private Volume volume;
    private ColorAdjustments ca;
    private float brightness;
    private Array obj;
    private Canvas canvasOptions;
    private Array sliders;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        obj = scene.GetRootGameObjects();
        foreach (GameObject o in obj)
        {
            if (o.name == "Brightness")
            {
                volume = o.GetComponent<Volume>();
            }
            if (o.name == "CanvasOptions")
            {
                canvasOptions = o.GetComponent<Canvas>();
                sliders = canvasOptions.GetComponentsInChildren<Slider>();
                foreach (Slider s in sliders)
                {
                    if (s.name == "Slider:Luminosite")
                    {
                        brightnessSlider = s;
                    }
                }
            }
        }
        brightness = PlayerPrefs.GetFloat("brightness");
        if (volume.profile.TryGet<ColorAdjustments>(out ca))
        {
            ca.postExposure.value = brightness;
        }
        if (brightnessSlider != null)
        {
            brightnessSlider.value = brightness;    
        }
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
            Canvas[] canvas_UI_liste = FindObjectsByType<Canvas>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            Canvas canvas_UI = canvas_UI_liste[0];
            foreach (Canvas canvas in canvas_UI_liste)
            {
                if ((canvas.tag == "CanvasGUI") || (canvas.tag == "UIJoueur"))
                {
                    canvas.gameObject.SetActive(false);
                }
            }

        }
    }

    public void ChangerTexteDansCanvas(Canvas canvas, string nouveau_texte, string tag_texte)
    {
        TMPro.TextMeshProUGUI[] gameobjects = canvas.GetComponentsInChildren<TMPro.TextMeshProUGUI>(true);
        foreach (TMPro.TextMeshProUGUI go in gameobjects)
        {
            if (go.gameObject.tag == tag_texte)
            {
                go.text = nouveau_texte;
            }
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
            Canvas[] canvas_UI_liste = FindObjectsByType<Canvas>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            Canvas canvas_UI = canvas_UI_liste[0];
            foreach (Canvas canvas in canvas_UI_liste)
            {
                if ((canvas.tag == "CanvasGUI") || (canvas.tag == "UIJoueur"))
                {
                    canvas.gameObject.SetActive(true);
                }
            }

        }
    }

    public void sliderCallBack()
    {
        if (volume != null)
        {
            if (volume.profile.TryGet<ColorAdjustments>(out ca))
            {
                if (brightnessSlider != null)
                {
                    ca.postExposure.value = brightnessSlider.value;
                    PlayerPrefs.SetFloat("brightness", brightnessSlider.value);
                }
            }
        }
        
    }
}
