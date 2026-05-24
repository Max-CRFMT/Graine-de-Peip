using UnityEngine;

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
        panel.SetActive(true);
    }
    public void closePanel()
    {
        panel.SetActive(false);
    }
    public void openPanelControl(GameObject panel,string nom_carte)
    {
        if (TurnHandler.instance.controle_en_cours)
        {
            panel.SetActive(true);
            MenuOptions.instance.ResearchCanvasSelonTag("TxtControle").gameObject.SetActive(false);
            SliderControl.instance.InitialisationSliderControle(nom_carte);
        }
    }
}
