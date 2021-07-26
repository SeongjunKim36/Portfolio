using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManger : Photon.PunBehaviour {
    int[] list = new int[2];
    public Transform[] trnPlayerStarted;
    public static int myIndexNumber;
    GameObject playerBody;
    private bool check_ID;


    PhotonView photonView ;

    // Start is called before the first frame update
    void Start () {
        photonView = GetComponent<PhotonView> ();

        //photonView.RPC("wow", PhotonTargets.All, list);
        if (PhotonNetwork.isMasterClient) {
            addPlayer (PhotonNetwork.player.ID);
            createPlayerBody ();
        }

    }
    void addPlayer (int _id) {
        int index = Array.IndexOf (list, 0);

        list[index] = _id;

        photonView.RPC ("UpdatePlayerList", PhotonTargets.Others, list, index);
    }
    void removePlayer (int _id) {
        int index = Array.IndexOf (list, _id);

        list[index] = 0;

        //photonView.RPC("UpdatePlayerList", PhotonTargets.Others, list, index);
    }


    void createPlayerBody () {
        
        
            playerBody = PhotonNetwork.Instantiate ("SpawnPlayer", trnPlayerStarted[myIndexNumber].position, trnPlayerStarted[myIndexNumber].rotation, 0);
            check_ID = !check_ID;
        
    }

    public override void OnPhotonPlayerConnected (PhotonPlayer newPlayer) {
        if (!PhotonNetwork.isMasterClient) return;
        addPlayer (newPlayer.ID);
    }

    public override void OnPhotonPlayerDisconnected (PhotonPlayer otherPlayer) {
        if (!PhotonNetwork.isMasterClient) return;
        removePlayer (otherPlayer.ID);
    }

    [PunRPC]
    void UpdatePlayerList (int[] _list, int _index) {
        list = _list;

        if (playerBody == null) {
            myIndexNumber = _index;
            createPlayerBody ();
        }
    }

    [PunRPC]
    void wow (int[] _list) {
        Debug.Log (_list.Length);
    }

}
