using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectRoquesAndBuiBui;
using System.Threading;

public class ServerManager : MonoBehaviour {

    ServerBdd server;
    Player p;

	// Use this for initialization
	void Start () {
        p = new Player("miniafrica");
        server = new ServerBdd(p);

        server.OnMatchFound += (() =>
        {
            FindObjectOfType<MatchMakingBox>().PlayerFound(server.Game.PlayerFound);
            Thread.Sleep(3000);
            //FindObjectOfType<Manager>().StartGame();
        });
	}
	
	// Update is called once per frame
	void Update () {
        GameObject temp = GameObject.Find("Game");
        if (temp != null)
        {
            //Debug.Log("gameready");
        }
    }

    public bool Connect()
    {
        bool temp = server.Connect();
        Debug.Log(temp);
        return temp;
    }

    public void JoinMatchMaking()
    {
        server.JoinMatchMaking();
    }

    public void Close()
    {
        server.Close();
    }
}
