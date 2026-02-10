using UnityEngine;

public class Player
{
    public string pseudo;
    public int pieces;
    public string map_choisie;
    public Player(string name, int coins, string name_map)
    {
        pseudo = name;
        pieces = coins;
        map_choisie = name_map;
        Debug.Log("Pseudo du joueur : " + pseudo + "\nPieces du joueur : " + pieces +"\nMap que le joueur à choisi :" + name_map + "\n\n");
    }
}
