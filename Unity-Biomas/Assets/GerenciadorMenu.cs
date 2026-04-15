using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para carregar cenas

public class GerenciadorMenu : MonoBehaviour
{
    public void IniciarJogo()
    {
        SceneManager.LoadScene("Menu");
    }

        public void Jogar()
    {
        SceneManager.LoadScene("Frase");
    }

        public void Ranking()
    {
        SceneManager.LoadScene("Ranking");
    }

        public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }

        public void VoltarMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SairJogo()
    {
        Debug.Log("Saiu do jogo");

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}

