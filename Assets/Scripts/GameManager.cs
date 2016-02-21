using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public List<Player> players;
    public Transform[] faces;
    public GameObject[] spawnPoints;
    public int player1Face, player2Face;

    public static GameManager instance;

    void Awake()
    {
        if (!instance)
            instance = this;
        if (instance != this)
            Destroy(gameObject);
        StartUp();
    }

    void OnLevelWasLoaded()
    {
        StartUp();
    }

    void StartUp()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Arena":
                spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
                players = new List<Player>();
                Instantiate(faces[player1Face], spawnPoints[1].transform.position,
                    spawnPoints[1].transform.rotation);
                Instantiate(faces[player2Face], spawnPoints[0].transform.position,
                    spawnPoints[0].transform.rotation);
                break;

        }
    }

    public void SetControls()
    {
        if(players.Count > 1)
            players[1].useSecondaryControls = true;
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
