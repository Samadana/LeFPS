using Mirror;
using UnityEngine;

public class MenuNetworkControllerServer : NetworkManager
{
    public static MenuNetworkControllerServer instance { get; private set; }

    public TelepathyTransport telepathyTransport;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;

        telepathyTransport = GetComponent<TelepathyTransport>();
        StartServer();
    }
}
