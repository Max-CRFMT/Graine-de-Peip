using System.Collections.Generic;
using UnityEngine;

public class GestionControl : MonoBehaviour
{
    public static GestionControl instance;

    public void Awake()
    {
        instance = this;
    }

    public void ReactivationPostControl()
    {
        MenuOptions.instance.ResearchCanvasSelonTag("TxtControle").gameObject.SetActive(false);
        TurnHandler.instance.controle_en_cours = false;
        MenuInGame.instance.ChangementClicableBoutonSelonTags(true, new List<string>() { "topButtonToPlayerBoard" }, "CanvasGUI");
    }
}
