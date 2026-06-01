using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Button painelAmazonia;
    public Button painelCerrado;
    public Button painelCaatinga;

    private void Start()
    {
        // =========================
        // CENA DE SELEÇÃO DE BIOMA
        // =========================

        if (SceneManager.GetActiveScene().name == "SelecaoBioma")
        {
            // Verifica se os botões foram associados
            if (painelAmazonia != null && painelCerrado  != null && painelCaatinga != null)
            {
                // AMAZÔNIA
                if (GameUtility.AmazoniaLiberada == false)
                {
                    painelAmazonia.interactable = false;
                }

                // CERRADO
                if (GameUtility.CerradoLiberado == false)
                {
                    painelCerrado.interactable = false;
                }

                // CAATINGA
                if (GameUtility.CaatingaLiberado == false)
                {
                    painelCaatinga.interactable = false;
                }
            }
            else
            {
                Debug.LogWarning("Associe os botões no Inspector.");
            }
        }

        // =========================
        // CENA DE SELEÇÃO DE AVATAR
        // =========================

        if (SceneManager.GetActiveScene().name == "SelecaoAvatar")
        {
            GameUtility.AmazoniaLiberada = true;
            GameUtility.CerradoLiberado = true;
            GameUtility.CaatingaLiberado = true;
        }
    }


    public void CarregarCena(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }

        public void DesativarAmazonia()
    {
        GameUtility.AmazoniaLiberada = false;

        if (painelAmazonia != null)
        {
            painelAmazonia.interactable = false;
        }
    }

    public void DesativarCerrado()
    {
    GameUtility.CerradoLiberado = false;

    if (painelCerrado != null)
    {
        painelCerrado.interactable = false;
    }
    }   
    
    public void DesativarCaatinga()
    {
    GameUtility.CaatingaLiberado = false;

    if (painelCaatinga != null)
    {
        painelCaatinga.interactable = false;
    }
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

    // ==========================================
    // ROTAS DA AMAZÔNIA
    // ==========================================
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

    // ==========================================
    // ROTAS DA CAATINGA
    // ==========================================
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

    // ==========================================
    // ROTAS DO CERRADO
    // ==========================================
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

    // ==========================================
    // ROTAS DA MATA ATLÂNTICA
    // ==========================================
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