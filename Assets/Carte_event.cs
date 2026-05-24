using UnityEngine;

public class Carte_event
{
    public string nom;
    public string Type;
    public string Effet;
    public string Difficulte;
    public string Gain;
    public string Cas_echec;
    public int Reproduction;
    public Carte_event(string nom, string Type, string Effet, string Difficulte, string Gain, string Cas_echec, int Reproduction)
    {
        this.nom = nom;
        this.Type = Type;
        this.Effet = Effet;
        this.Difficulte = Difficulte;
        this.Gain = Gain;
        this.Cas_echec = Cas_echec;
        this.Reproduction = Reproduction;

    }
    public override string ToString()
    {
        return $"(nom={nom}, Type={Type}, Effet={Effet}, Difficulte={Difficulte}, Gain={Gain}, Cas_echec={Cas_echec}, Reproduction={Reproduction})";
    }
}
