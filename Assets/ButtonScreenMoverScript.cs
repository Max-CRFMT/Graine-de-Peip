using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ButtonScreenMoverScript : MonoBehaviour
{
    //coordon�es ajustables pour le d�placement de la cam�ra
    public int yCoord;
    public bool isButtonPressed = false;
    public float velocity;

    public GameObject MainCamera;

    public GameObject gameObjectWithButtonToBeDisabled;
    public Button buttonToBeDisabledDuringMovement;

    public static ButtonScreenMoverScript instance;

    public void Awake()
    {
        instance = this;
    }

    // Pour les deux fonctions on appelle la fonction MoveCamera de GameLogic en lui passant les coordonn�es correspondantes pour faire bouger la cam�ra vers le haut ou vers le bas

    public void ScreenMoverTopButtonpressed()
    {
        gameObjectWithButtonToBeDisabled = GameObject.FindGameObjectWithTag("bottomButtonToGameAtlas");
        isButtonPressed = true;
    }

    public void ScreenMoverBottomButtonPressed()
    {
        gameObjectWithButtonToBeDisabled = GameObject.FindGameObjectWithTag("topButtonToPlayerBoard");
        isButtonPressed = true;
    }



    public void MoveCamera(int yCoord, float velocity) 
    {
        MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, new Vector3(0, yCoord, -10), velocity * Time.deltaTime);
    }

    private void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if (isButtonPressed)
        {
            buttonToBeDisabledDuringMovement = gameObjectWithButtonToBeDisabled.GetComponentInParent<Button>();
            buttonToBeDisabledDuringMovement.enabled = false;
            MenuInGame.instance.ChangementClicableBoutonSelonTags(false, new List<string>(){"UIJoueur"}, "CanvasGUI");
            MoveCamera(yCoord, velocity);
            if (Mathf.Abs(MainCamera.transform.position.y - yCoord) <= 0.01) //Arr�te la cam�ra une foi str�s proche de la coordon�e voulue mais arrondie pour pouvoir ajuster la valeur proprement et non rester vers une limite jamais atteinte.
            {
                MainCamera.transform.position = new Vector3(0, yCoord, -10); //Assure que la cam�ra est exactement � la coordonn�e voulue une fois le mouvement termin�
                isButtonPressed = false;
                MenuInGame.instance.ChangementClicableBoutonSelonTags(true, new List<string>(){"UIJoueur"}, "CanvasGUI");
                if (!TurnHandler.instance.resencement_en_cours)
                {
                    buttonToBeDisabledDuringMovement.enabled = true;
                }
            }
        }
    }
}
