using System;
using System.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HappyHourRts.Networks
{
    public class PhotonNetworkRunnerHandler : MonoBehaviour
    {
        [SerializeField] NetworkRunner _prefab;
        
        NetworkRunner _networkRunner;

        async void Start()
        {
            _networkRunner = Instantiate(_prefab);

            await InitializedNetworkRunnerAsync(_networkRunner, GameMode.AutoHostOrClient, NetAddress.Any(),
                SceneManager.GetActiveScene().buildIndex + 1, (networkRunner) =>
                {
                    Debug.Log(networkRunner.State);
                });
        }

        Task InitializedNetworkRunnerAsync(NetworkRunner networkRunner, GameMode gameMode,
            NetAddress netAddress, SceneRef sceneRef, Action<NetworkRunner> onInitializedSuccessCallback)
        {
            INetworkSceneManager sceneManager;
            
            if (networkRunner.TryGetComponent(out INetworkSceneManager networkSceneManager))
            {
                sceneManager = networkSceneManager;
            }
            else
            {
                sceneManager = networkRunner.AddComponent<NetworkSceneManagerDefault>();
            }

            _networkRunner.ProvideInput = true;

            return _networkRunner.StartGame(new StartGameArgs()
            {
                GameMode = gameMode,
                Address = netAddress,
                Scene = sceneRef,
                SessionName = "TestRoom",
                Initialized = onInitializedSuccessCallback,
                SceneManager = sceneManager
            });
        }
    }    
}