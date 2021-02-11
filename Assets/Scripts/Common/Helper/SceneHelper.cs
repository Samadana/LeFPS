namespace Assets.Scripts.Common.Helper
{
    public static class SceneHelper
    {
        public static void GoMenuLobby()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MenuLobby");
        }

        public static void GoGameLobby()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameLobby");
        }

        public static void GoCardMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }

        public static void GoLogin()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Login");
        }

        public static void GoTestLobby()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("TestLobby");
        }
    }
}
