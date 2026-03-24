using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScreenMoverScript : MonoBehaviour
{
    //coordonées ajustables pour le déplacement de la caméra
    public int yCoord;
    public bool isButtonPressed = false;
    public float velocity;

    public GameObject MainCamera;
    public GameObject boutonFinTour;

    public GameObject gameObjectWithButtonToBeDisabled;
    public Button buttonToBeDisabledDuringMovement;

    // Pour les deux fonctions on appelle la fonction MoveCamera de GameLogic en lui passant les coordonnées correspondantes pour faire bouger la caméra vers le haut ou vers le bas

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
        boutonFinTour.gameObject.SetActive(false); //On désactive le bouton de fin de tour pour éviter les bugs d'interaction avec les boutons de déplacement de la caméra
        MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, new Vector3(0, yCoord, -10), velocity * Time.deltaTime);
        boutonFinTour.gameObject.SetActive(true); //On réactive le bouton de fin de tour une fois que la caméra a fini de se déplacer
    }

    private void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        boutonFinTour = GameObject.FindGameObjectWithTag("boutonFinTour");
    }

    void Update()
    {
        if (isButtonPressed)
        {
            buttonToBeDisabledDuringMovement = gameObjectWithButtonToBeDisabled.GetComponentInParent<Button>();
            buttonToBeDisabledDuringMovement.enabled = false;
            MoveCamera(yCoord, velocity);
            if (Mathf.Abs(MainCamera.transform.position.y - yCoord) <= 0.01) //Arrête la caméra une foi strès proche de la coordonée voulue mais arrondie pour pouvoir ajuster la valeur proprement et non rester vers une limite jamais atteinte.
            {
                MainCamera.transform.position = new Vector3(0, yCoord, -10); //Assure que la caméra est exactement à la coordonnée voulue une fois le mouvement terminé
                isButtonPressed = false;
                buttonToBeDisabledDuringMovement.enabled = true;
            }
        }
    }
}
