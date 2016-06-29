using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class MyLobby : NetworkLobbyManager
{
    public GameObject[] playerPrefabs;
  
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log("OnServerAddPlayer");
        Transform trans = GetStartPosition();

        int rand = Random.Range(0, playerPrefabs.Length);

        GameObject prefab = playerPrefabs[rand];
        Debug.Log("No;" + rand + "/" + trans);
        GameObject player = Instantiate(prefab, trans.position, trans.rotation) as GameObject;
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}
