using System.Linq;
using UnityEngine;

public class GestionRemontageBanque : MonoBehaviour
{
    public int nom;

    public void SurClique()
    {
        GestionPostRecolte.instance.PiocheOuBanque = 'B';
        GestionPostRecolte.instance.carte_cible_recolte = TurnHandler.instance.PlayerActuel.continent.banque.listeDeListes[GestionPostRecolte.instance.banque2ou3][nom];
        MenuOptions.instance.ResearchCanvasSelonTag("BanqueList").gameObject.SetActive(false);
    }
}
