using UnityEngine;
using TMPro;

public class Recup_joueurdonc : MonoBehaviour
{
    public void Onclicked()
    {
        var bouton = GetComponentInParent<UnityEngine.UI.Button>();
        var texte = bouton.GetComponentInChildren<TMP_Text>();

        string nom_joueur = texte.text;
        Debug.Log("On est passés");
        SliderControl.instance.JoueurDonCliqued(nom_joueur);
    }
}
