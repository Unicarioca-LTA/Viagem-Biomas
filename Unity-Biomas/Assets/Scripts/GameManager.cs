using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
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
    
        public void Avatar()
    {
        SceneManager.LoadScene("SelecaoAvatar");
    }    
        public void SelecaoBioma()
    {
        SceneManager.LoadScene("SelecaoBioma");
    }

        
    public void Amazonia()
    {
        SceneManager.LoadScene("Amazonia");
    }        
    public void Cerrado()
    {
        SceneManager.LoadScene("Cerrado");
    }        
    public void Caatinga()
    {
        SceneManager.LoadScene("Caatinga");
    }        
    public void MataAtlantica()
    {
        SceneManager.LoadScene("MataAtlantica");
    }        
    public void Pantanal()
    {
        SceneManager.LoadScene("Pantanal");
    }        
    public void Pampa()
    {
        SceneManager.LoadScene("Pampa");
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
