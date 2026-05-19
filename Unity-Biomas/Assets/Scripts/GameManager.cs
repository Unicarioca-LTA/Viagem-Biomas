using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour

    
{
    public Button painelAmazonia;
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "SelecaoBioma")
        {

            if (GameUtility.BotaoPainel == false)
            {
                painelAmazonia.interactable = false;
            }
        }
    }
    public void CarregarCena(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }

    public void DesativarAmazonia()
    {
        GameUtility.BotaoPainel = false;
    }

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
        public void AmazoniaMapa()
    {
        SceneManager.LoadScene("AmazoniaMapa");
    }       
        public void AmazoniaRegioes()
    {
        SceneManager.LoadScene("AmazoniaRegioes");
    }       
        public void AmazoniaClima()
    {
        SceneManager.LoadScene("AmazoniaClima");
    }       
        public void AmazoniaVegetacao()
    {
        SceneManager.LoadScene("AmazoniaVegetacao");
    }       
        public void AmazoniaAmeacas()
    {
        SceneManager.LoadScene("AmazoniaAmeacas");
    }       
        public void AmazoniaFauna()
    {
        SceneManager.LoadScene("AmazoniaFauna");
    }
    public void AmazoniaQuiz()
    {
        SceneManager.LoadScene("AmazoniaQuiz");
    }    


    public void Caatinga()
    {
        SceneManager.LoadScene("Caatinga");
    }        
        public void CaatingaMapa()
    {
        SceneManager.LoadScene("CaatingaMapa");
    }       
        public void CaatingaRegioes()
    {
        SceneManager.LoadScene("CaatingaRegioes");
    }       
        public void CaatingaClima()
    {
        SceneManager.LoadScene("CaatingaClima");
    }       
        public void CaatingaVegetacao()
    {
        SceneManager.LoadScene("CaatingaVegetacao");
    }       
        public void CaatingaAmeacas()
    {
        SceneManager.LoadScene("CaatingaAmeacas");
    }       
        public void CaatingaFauna()
    {
        SceneManager.LoadScene("CaatingaFauna");
    }
    public void CaatingaQuiz()
    {
        SceneManager.LoadScene("CaatingaQuiz");
    }    


    public void Cerrado()
    {
        SceneManager.LoadScene("Cerrado");
    }      

        public void CerradoMapa()
    {
        SceneManager.LoadScene("CerradoMapa");
    }       
        public void CerradoRegioes()
    {
        SceneManager.LoadScene("CerradoRegioes");
    }       
        public void CerradoClima()
    {
        SceneManager.LoadScene("CerradoClima");
    }       
        public void CerradoVegetacao()
    {
        SceneManager.LoadScene("CerradoVegetacao");
    }       
        public void CerradoAmeacas()
    {
        SceneManager.LoadScene("CerradoAmeacas");
    }       
        public void CerradoFauna()
    {
        SceneManager.LoadScene("CerradoFauna");
    }
    public void CerradoQuiz()
    {
        SceneManager.LoadScene("CerradoQuiz");
    }    


    public void MataAtlantica()
    {
        SceneManager.LoadScene("MataAtlantica");
    }      

        public void MataAtlanticaMapa()
    {
        SceneManager.LoadScene("MataAtlanticaMapa");
    }       
        public void MataAtlanticaRegioes()
    {
        SceneManager.LoadScene("MataAtlanticaRegioes");
    }       
        public void MataAtlanticaClima()
    {
        SceneManager.LoadScene("MataAtlanticaClima");
    }       
        public void MataAtlanticaVegetacao()
    {
        SceneManager.LoadScene("MataAtlanticaVegetacao");
    }       
        public void MataAtlanticaAmeacas()
    {
        SceneManager.LoadScene("MataAtlanticaAmeacas");
    }       
        public void MataAtlanticaFauna()
    {
        SceneManager.LoadScene("MataAtlanticaFauna");
    }
    public void MataAtlanticaQuiz()
    {
        SceneManager.LoadScene("MataAtlanticaQuiz");
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
