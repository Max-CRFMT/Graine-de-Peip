using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GestionInteractionBanque : MonoBehaviour
{
    public void Banque1OnClick()
    {
        if (TurnHandler.instance.recolte_pioche_en_cours)
        {
            if (GestionPostRecolte.instance.carte_cible_recolte.conservable)
            {
                GestionPostRecolte.instance.PiocheToJardinOuPiocheToBanque = 'B';
                TurnHandler.instance.PlayerActuel.continent.banque.Banque1.transform.GetChild(0).gameObject.SetActive(false);
                GestionPostRecolte.instance.AjoutConstantesEctAuTurnHandlerEtCloture();
            } else 
            {
                TurnHandler.instance.PlayerActuel.MessageErreur("Cette carte n'est pas conservable en banque.");
            }
        } 
        else if (TurnHandler.instance.recolte_en_cours && GestionPostRecolte.instance.PiocheOuBanque == 'B')
        {
            GestionPostRecolte.instance.BanqueReloadOuBanqueToJardin = 'R';
            GestionPostRecolte.instance.AjoutConstantesEctAuTurnHandlerEtCloture();
        } 
        else if (TurnHandler.instance.PlayerActuel.restaurationEnCours)
        {
            TurnHandler.instance.PlayerActuel.curBank = 0;
            AfficherCartesDansBanqueRemontee(0);
        }      
        else
        {
            AfficherCartesDansBanqueConsultation(0);
        }
    }
    public void Banque2OnClick()
    {
        if (TurnHandler.instance.recolte_en_cours)
        {
            AfficherCartesDansBanqueRemontee(1);
        }
        else if (TurnHandler.instance.PlayerActuel.restaurationEnCours)
        {
            TurnHandler.instance.PlayerActuel.curBank = 1;
            AfficherCartesDansBanqueRemontee(1);
        }  
        else
        {
            AfficherCartesDansBanqueConsultation(1);
        }
    }
    public void Banque3OnClick()
    {
        if (TurnHandler.instance.recolte_en_cours)
        {
            AfficherCartesDansBanqueRemontee(2);
        } 
        else if (TurnHandler.instance.PlayerActuel.restaurationEnCours)
        {
            TurnHandler.instance.PlayerActuel.curBank = 2;
            AfficherCartesDansBanqueRemontee(2);
        }  
        else
        {
            AfficherCartesDansBanqueConsultation(2);
        }
    }
    public void AfficherCartesDansBanqueConsultation(int num_banque)
    {
        Canvas canvas_banque = MenuOptions.instance.ResearchCanvasSelonTag("BanqueList");
        canvas_banque.gameObject.SetActive(true);

        for (int i = 0; i != TurnHandler.instance.PlayerActuel.continent.banque.listeDeListes[num_banque].Count(); i++)
        {
            GameObject go = GameObject.Find(i.ToString());

            Sprite new_sprite = Resources.Load<Sprite>(TurnHandler.instance.PlayerActuel.continent.banque.listeDeListes[num_banque][i].PetitPathImage);
            Image image = go.GetComponent<Image>();
            image.sprite = new_sprite;
            image.color = TurnHandler.instance.PlayerActuel.continent.banque.Visible;
            go.GetComponent<Button>().enabled = false;
        }

        for (int i = 8; i != TurnHandler.instance.PlayerActuel.continent.banque.listeDeListes[num_banque].Count() - 1; i--)
        {
            GameObject go = GameObject.Find(i.ToString());
            Image image = go.GetComponent<Image>();
            image.color = TurnHandler.instance.PlayerActuel.continent.banque.Invisible;
            go.GetComponent<Button>().enabled = false;
        }
    }

    public void AfficherCartesDansBanqueRemontee(int num_banque)
    {
        Canvas canvas_banque = MenuOptions.instance.ResearchCanvasSelonTag("BanqueList");
        canvas_banque.gameObject.SetActive(true);
        GestionPostRecolte.instance.banque2ou3 = num_banque;

        for (int i = 0; i != TurnHandler.instance.PlayerActuel.continent.banque.listeDeListes[num_banque].Count(); i++)
        {
            GameObject go = GameObject.Find(i.ToString());

            Sprite new_sprite = Resources.Load<Sprite>(TurnHandler.instance.PlayerActuel.continent.banque.listeDeListes[num_banque][i].PetitPathImage);
            Image image = go.GetComponent<Image>();
            image.sprite = new_sprite;
            image.color = TurnHandler.instance.PlayerActuel.continent.banque.Visible;
            go.GetComponent<Button>().enabled = true;
        }

        for (int i = 8; i != TurnHandler.instance.PlayerActuel.continent.banque.listeDeListes[num_banque].Count() - 1; i--)
        {
            GameObject go = GameObject.Find(i.ToString());
            Image image = go.GetComponent<Image>();
            image.color = TurnHandler.instance.PlayerActuel.continent.banque.Invisible;
            go.GetComponent<Button>().enabled = false;
        }
    }
}
