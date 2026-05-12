using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    public Slider amountSlider;
    public TMP_Text amountText;

    int thunesdujoueur = TurnHandler.instance.PlayerActuel.pieces;
    void Start()
    {
        amountSlider.minValue = 1;
        amountSlider.maxValue = thunesdujoueur;
        amountSlider.value = 1;
    }

    public void OnSliderChange(float value)
    {
        amountText.text = ((int)value).ToString();
    }

    public void OnValidateClicked()
    {
        int montantSelectionne = (int)amountSlider.value;
        Debug.Log("Montant : " + montantSelectionne);
    }
}
