using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class MyManager :  NetworkManager{

    public GameObject[] playerPrefabs;
    public static int playSelectNo;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log("OnServerAddPlayer");
        Transform trans = GetStartPosition();

        int rand = Random.Range(0, playerPrefabs.Length);
        
        GameObject prefab = playerPrefabs[rand];
        Debug.Log("No;" + rand + "/" + trans);
        GameObject player = Instantiate(prefab, trans.position, trans.rotation) as GameObject;
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        
        /*
        if (extraMessageReader != null)
        {
            var s = extraMessageReader.ReadMessage<StringMessage>();
            Debug.Log("my name is " + s.value);
        }
        base.OnServerAddPlayer(conn, playerControllerId, extraMessageReader);
        */
    }
}
