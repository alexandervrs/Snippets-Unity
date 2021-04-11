
/**
 * ThirdParty.PUN.cs
 * Photon Networking related snippets for Unity
 *
 * https://www.photonengine.com/pun
 */

/* using */
using Photon.Pun;
using Photon.Realtime;


/* -----------------------------------------
   Setup an Account
----------------------------------------- */

/*
		  
	1. Create an account and login to https://www.photonengine.com/
    2. From your Dashboard click at "Create New App" button, choose as Photon Type: "Photon PUN" and name it
    3. After creation you'll be able to see your APP ID. This is important for your Unity project

    4. Create a new Unity project and install the PUN plugin from https://assetstore.unity.com/packages/tools/network/pun-2-free-119922
    5. Search inside the "Photon" folder and find the "PhotonServerSettings" asset file
    6. Click on it and input your APP ID under "Server/Cloud Settings"

    7. Next you'll minimally need a Scene for the Lobby/Find Game screen & a Scene for the actual game

*/


/* ----------------------------------------------
   Lobby
---------------------------------------------- */

/*
    Create a script to handle Lobby connections and finding a room, attach it once to a GameObject in the Scene:
*/

/// Class Body:
private bool isConnecting = false; // variable to check if the game is connecting

private int maxPlayersPerRoom = 3; // how many players to have max in each game room

/// Awake():
// will signify that all clients should load the same scene as the master client
PhotonNetwork.AutomaticallySyncScene = true;

/// FindRoom():
// this should be triggered usually by a "Find Game" button

isConnecting = true;

if (PhotonNetwork.IsConnected) {

    Debug.Log("Joining Room...");
    PhotonNetwork.JoinRandomRoom();

} else {

    PhotonNetwork.GameVersion = "1.0.0"; // all clients will have to have the same version to connect correctly
    PhotonNetwork.ConnectUsingSettings(); // connect with the settings from PhotonServerSettings file

}

// if there's no room to join at all, we can create one
public override void OnJoinRandomFailed(short returnCode, string message)
{
    Debug.Log("Join Random Room Failed, Creating Room");
    PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    PhotonNetwork.CurrentRoom.IsVisible = true; // if this is set to false, then the room will need to be accessed directly (good for private rooms)
}

// handle player joining room
public override void OnJoinedRoom()
{
    Debug.Log("Joined Room Success!");

    int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

    // set the player's name, could be set to anything you want like actual account name etc.
    PhotonNetwork.NickName = "Player"+playerCount;

    if (playerCount != maxPlayersPerRoom) {
        // players have not completed the quota yet
        Debug.Log("Waiting for Players! "+playerCount+" of "+maxPlayersPerRoom);
    } else {
        // all required players have joined
        Debug.Log("Session is ready to start!");
    }

}

// handle secondary client connection to master client, very first client must have created a room and waiting for players
// a master client is the considered the very first client that creates the room and waits for other players
public override void OnConnectedToMaster()
{

    Debug.Log("Connected!");

    if (isConnecting) {
        PhotonNetwork.JoinRandomRoom();
    }

}

// when player joins room, add to the player count and if player quota is reached, start the game
public override void OnPlayerEnteredRoom(Player newPlayer)
{

    if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayersPerRoom) {

        // close the room availability, player quota has been reached
        PhotonNetwork.CurrentRoom.IsOpen = false;

        Debug.Log("Max Players Reached, Starting Game");

        if (PhotonNetwork.IsMasterClient) {
            // load the level Scene for every joined player
            PhotonNetwork.LoadLevel("SessionScene");
        }

    }

}
    
// handle connection errors
public override void OnDisconnected(DisconnectCause cause)
{
    isConnecting = false;
    Debug.Log("Disconnected... "+cause);
}


/* ----------------------------------------------
   In-Game
---------------------------------------------- */

/*
		  
	Create a GameObject for the Player:
    1. Add the "Photon Transform View" Component with "Position" and "Rotation" options enabled
    2. Add the "Photon View" Component to it with the following settings:
        a. Ownership Transfer: Fixed
        b. Synchronization: Reliable Delta Compressed
        c. Observable Search: Manual
        d. Observed Components: add the player Prefab (so it shows Player (Photon Transform View))

*/

/*
    Create a script to handle connections and attach it once to a GameObject in the Scene:
*/

/// Class Body:
public GameObject playerPrefab; // This will be each prefab representing a Player. Player prefab will be to be in a "Resources" folder

/// Start():
// create the connected players in game, a bit randomly placed from each other
Debug.Log("Players Connected: "+PhotonNetwork.CurrentRoom.PlayerCount.ToString());
PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0+UnityEngine.Random.Range(-2, 2), 0+UnityEngine.Random.Range(-2, 2), 0), Quaternion.identity, 0);

// you can use this string in a TextMeshPro field or anything to identify the player
Debug.Log("Player Name is: "+photonView.Owner.NickName);


/// LeaveSession():
// this should be triggered usually by a "Leave Game" button
Debug.Log("Players Connected: "+PhotonNetwork.CurrentRoom.PlayerCount.ToString());
PhotonNetwork.LeaveRoom();


// handle disconnect
public override void OnLeftRoom()
{
    // load back the Lobby Scene if player leaves, will only do it for the player leaving
    PhotonNetwork.LoadLevel("LobbyScene");
}