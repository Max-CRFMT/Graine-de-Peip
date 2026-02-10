using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PseudoLengthHandler : MonoBehaviour
{
    [SerializeField] TMPro.TMP_InputField Champ_Input;
    public void OnChanged()
    {
        string text_input = Champ_Input.text;
        int longueur = text_input.Length;
        if (longueur > 12)
        {
            Debug.Log("Plus de 12 caractères atteint, on cuutttt");
            Champ_Input.text = text_input.Substring(0, 12);
        }
    }
    
}
