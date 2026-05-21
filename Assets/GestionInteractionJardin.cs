using UnityEngine;
using UnityEngine.UI;

public class GestionInteractionJardin : MonoBehaviour
{
    public Carte carte_contenue;
    public void JardinCliqued()
    {
        GestionPostRecolte.instance.PiocheToJardinOuPiocheToBanque = 'J';
        GestionPostRecolte.instance.jardin_cible = gameObject;
        GestionPostRecolte.instance.AjoutConstantesEctAuTurnHandlerEtCloture();
    }

    public void MiseAJourUI()
    {
        if (carte_contenue != null)
        {
            var go = gameObject;
            Sprite new_sprite = Resources.Load<Sprite>(carte_contenue.PetitPathImage);
            Image image = go.GetComponent<Image>();
            image.sprite = new_sprite;
            image.color = TurnHandler.instance.PlayerActuel.continent.banque.Visible;
        } else if (carte_contenue == null)
        {
            var go = gameObject;
            Image image = go.GetComponent<Image>();
            image.color = TurnHandler.instance.PlayerActuel.continent.banque.Invisible;
        }
    }
}
