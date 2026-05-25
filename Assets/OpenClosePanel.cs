using UnityEngine;
using System.Collections.Generic;

public class OpenClosePanel : MonoBehaviour 
{
    public GameObject panel;
    public static OpenClosePanel instance;

    private void Awake()
    {
        instance = this;
    }
    public void openPanel()
    {
        MenuInGame.instance.ChangementClicableBoutonSelonTags(false, new List<string>() { "BanqueList", "Jardins" }, "CanvasGUI");
        panel.SetActive(true);
    }
    public void closePanel()
    {
        MenuInGame.instance.ChangementClicableBoutonSelonTags(true, new List<string>() { "BanqueList", "Jardins" }, "CanvasGUI");
        TurnHandler.instance.PlayerActuel.continent.jardin.DisableButton();
        panel.SetActive(false);
    } 
    
    public void openPanelControl(GameObject panel,string nom_carte)
    {
        if (TurnHandler.instance.controle_en_cours)
        {
            MenuInGame.instance.ChangementClicableBoutonSelonTags(false, new List<string>() { "BanqueList", "Jardins" }, "CanvasGUI");
            panel.SetActive(true);
            MenuOptions.instance.ResearchCanvasSelonTag("TxtControle").gameObject.SetActive(false);
            SliderControl.instance.InitialisationSliderControle(nom_carte);
        }
    }
}
