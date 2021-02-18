using Mirror;
using UnityEngine;


public class MenuNetworkControllerClient : NetworkManager
{
    public static MenuNetworkControllerClient instance { get; private set; }

    public TelepathyTransport telepathyTransport;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;

        telepathyTransport = GetComponent<TelepathyTransport>();
        networkAddress = ConfigManager.serverIP;
        StartClient();
    }
}

