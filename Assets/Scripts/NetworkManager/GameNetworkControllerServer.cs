using Mirror;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.NetworkManager
{
    public class GameNetworkControllerServer : NetworkBehaviour
    {
        private Mirror.NetworkManager _networkManager;
        public static GameNetworkControllerServer instance { get; private set; }
        private float timeLeft = 3600;
        public TelepathyTransport telepathyTransport;

        void Awake()
        {
            _networkManager = GetComponent<Mirror.NetworkManager>();
        }

        // Start is called before the first frame update
        void Start()
        {
            if (instance != null)
                Destroy(instance);
            instance = this;

            telepathyTransport = GetComponent<TelepathyTransport>();
            telepathyTransport.port = ushort.Parse(ConfigManager.GameServerPort);           
            
            Debug.Log("Telepathy port : " + telepathyTransport.port);
            _networkManager = GetComponent<Mirror.NetworkManager>();

            StartCoroutine(CoroutineStartServer());
        }

        private void Update()
        {      
            timeLeft -= Time.deltaTime;    
            if(timeLeft < 0 )
            {
                _networkManager.StopHost();
                Application.Quit();
            }
        }

        public string GetParameter(string parameterName)
        {
            string[] args = System.Environment.GetCommandLineArgs();
            string input = "";
            for (int i = 0; i < args.Length; i++)
            {
                //Debug.Log("ARG " + i + ": " + args[i]);
                if (args[i] == parameterName)
                {
                    input = args[i + 1];
                }
            }
            return input;
        }

        //Check if port is valorised and start a server
        public IEnumerator CoroutineStartServer()
        {
            int _numberOfTry = 0;
            bool serverStarted = false;
            while (_numberOfTry < 30 && !serverStarted)
            {
                yield return new WaitForSeconds(1f);

                Debug.Log("TryStartServer : nb " + _numberOfTry);
                if (_networkManager != null && Convert.ToInt32(telepathyTransport.port) > 0)
                {
                    Debug.Log("Demarage du server");
                    _networkManager.StartServer();
                }

                if (_networkManager.isNetworkActive)
                {
                    serverStarted = true;
                }
                else
                {
                    _numberOfTry++;
                }
            }

            if (!serverStarted)
                Application.Quit();
        }

        public void PostCreateMatch()
        {
            DBmanager.instance.PostCreateMatch(telepathyTransport.port);
        }

    }
}
