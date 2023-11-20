using System.Collections.Generic;
using System.Linq;
using Fusion;
using Fusion.Sockets;
using HappyHourRts.Controllers;
using UnityEngine;

namespace HappyHourRts.Networks
{
    public class NetworkSpawner : MonoBehaviour, INetworkRunnerCallbacks
    {
        [Header("Player 1")]
        [SerializeField] Vector3 _position1;
        [SerializeField] Color _color1;
        
        [Header("Player 2")]
        [SerializeField] Vector3 _position2;
        [SerializeField] Color _color2;
        
        [Header("Network Prefab")]
        [SerializeField] PlayerController  _playerPrefab;

        readonly Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (runner.IsServer)
            {
                Debug.Log($"{nameof(OnPlayerJoined)} triggered on server side");
                bool condition = _spawnedCharacters.Count == 0;
                var newPlayer = runner.Spawn(_playerPrefab, condition ? _position1 : _position2, Quaternion.identity, player);

                newPlayer.Color = (condition ? _color1 : _color2);
                _spawnedCharacters.Add(player, newPlayer.Object);
            }
            else
            {
                Debug.Log($"{nameof(OnPlayerJoined)}");
            }
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
            {
                runner.Despawn(networkObject);
                _spawnedCharacters.Remove(player);
            }
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            if (PlayerController.Local == null) return;
            
            input.Set(new NetworkInputData
            {
                IsTouchDown =  PlayerController.Local.IInputReader.IsTouchDown
            });
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
        }

        public void OnDisconnectedFromServer(NetworkRunner runner)
        {
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        {
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, System.ArraySegment<byte> data)
        {
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
        }
    }    
}

