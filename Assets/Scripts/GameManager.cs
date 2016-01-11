using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public List<Player> players;

    public static GameManager instance;

    void Awake()
    {
        if (!instance)
            instance = this;
        if (instance != this)
            Destroy(gameObject);
        StartUp();
    }

    void StartUp()
    {
        players = new List<Player>();
    }

    public void GameOver(Player loser)
    {
        int i = players.IndexOf(loser);
        Player winner;
        if (i == 1)
            winner = players[0];
        else if (i == 0)
            winner = players[1];
        else winner = players[0];
        winner.Win();
    }
}
