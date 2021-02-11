using System.Collections;
using UnityEngine;


namespace Mirror
{
    [RequireComponent(typeof(NetworkManager))]
    public class GameNetworkControllerClient : MonoBehaviour
    {
        public NetworkManager _networkManager;
        public TelepathyTransport telepathyTransport;
        public static GameNetworkControllerClient instance { get; private set; }
        public bool clientStarted = false;

        // Start is called before the first frame update
        void Start()
        {
            if (instance != null)
                Destroy(instance);
            instance = this;
        }

        void Awake()
        {
            telepathyTransport = GetComponent<TelepathyTransport>();
            telepathyTransport.port = ushort.Parse(ConfigManager.GameServerPort);

            _networkManager = GetComponent<NetworkManager>();
            _networkManager.networkAddress = ConfigManager.serverIP;
            StartCoroutine(CoroutineConnectWhenClientReady());
        }

        public IEnumerator CoroutineConnectWhenClientReady()
        {
            while (!clientStarted)
            {
                yield return new WaitForSeconds(5.0f);
                if (!NetworkClient.isConnected && !NetworkServer.active)
                {
                    if (!NetworkClient.active)
                    {
                        clientStarted = true;
                        _networkManager.StartClient();
                    }
                }
            }
        }
    }
}
