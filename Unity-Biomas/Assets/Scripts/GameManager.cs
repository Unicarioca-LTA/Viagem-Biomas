using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Button painelAmazonia;
    public Button painelCerrado;
    public Button painelCaatinga;
    public Button painelMataAtlantica;
    public Button painelPantanal;
    public Button painelPampa;

    // --- NOVAS VARIÁVEIS ---
    [Header("Conclusão de Etapas")]
    public Button botaoAvancar;       // Arraste o botão de continuar aqui
    public GameObject painelParabens;   // Arraste o painel de parabéns aqui

    private void Start()
    {
        // =========================
        // CENA DE SELEÇÃO DE BIOMA
        // =========================

        if (SceneManager.GetActiveScene().name == "SelecaoBioma")
        {
            // Garante que o botão e o painel começam invisíveis/desativados
            if (botaoAvancar != null) botaoAvancar.gameObject.SetActive(false);
            if (painelParabens != null) painelParabens.SetActive(false);

            // Verifica se os botões foram associados
            if (painelAmazonia != null && painelCerrado != null && painelCaatinga != null && painelMataAtlantica != null && painelPantanal != null && painelPampa != null)
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

                // Mata Atlantica
                if (GameUtility.MataAtlanticaLiberado == false)
                {
                    painelMataAtlantica.interactable = false;
                }

                // Pantanal
                if (GameUtility.PantanalLiberado == false)
                {
                    painelPantanal.interactable = false;
                }

                // Pampa
                if (GameUtility.PampaLiberado == false)
                {
                    painelPampa.interactable = false;
                }

                // Verifica logo no início caso o jogador já tenha concluído tudo antes
                VerificarConclusao();
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
            GameUtility.MataAtlanticaLiberado = true;
            GameUtility.PantanalLiberado = true;
            GameUtility.PampaLiberado = true;
        }
    }

    // --- NOVA FUNÇÃO DE VERIFICAÇÃO ---
    private void VerificarConclusao()
    {
        // Se TODOS os biomas estiverem como FALSE (desativados/concluídos)
        if (GameUtility.AmazoniaLiberada == false &&
            GameUtility.CerradoLiberado == false &&
            GameUtility.CaatingaLiberado == false &&
            GameUtility.MataAtlanticaLiberado == false &&
            GameUtility.PantanalLiberado == false &&
            GameUtility.PampaLiberado == false) 
        {
            // Ativa o botão de continuar na tela
            if (botaoAvancar != null)
            {
                botaoAvancar.gameObject.SetActive(true);
            }
        }
    }

    // --- NOVA FUNÇÃO PARA O CLIQUE DO BOTÃO CONTINUAR ---
    public void AbrirPainelParabens()
    {
        if (painelParabens != null)
        {
            painelParabens.SetActive(true);
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
        VerificarConclusao(); // <-- Verificação adicionada
    }

    public void DesativarCerrado()
    {
        GameUtility.CerradoLiberado = false;

        if (painelCerrado != null)
        {
            painelCerrado.interactable = false;
        }
        VerificarConclusao(); // <-- Verificação adicionada
    }

    public void DesativarCaatinga()
    {
        GameUtility.CaatingaLiberado = false;

        if (painelCaatinga != null)
        {
            painelCaatinga.interactable = false;
        }
        VerificarConclusao(); // <-- Verificação adicionada
    }

    public void DesativarMataAtlantica()
    {
        GameUtility.MataAtlanticaLiberado = false;

        if (painelMataAtlantica != null)
        {
            painelMataAtlantica.interactable = false;
        }
        VerificarConclusao(); // <-- Verificação adicionada
    }

    public void DesativarPantanal()
    {
        GameUtility.PantanalLiberado = false;

        if (painelPantanal != null)
        {
            painelPantanal.interactable = false;
        }
        VerificarConclusao(); // <-- Verificação adicionada
    }

    public void DesativarPampa()
    {
        GameUtility.PampaLiberado = false;

        if (painelPampa != null)
        {
            painelPampa.interactable = false;
        }
        VerificarConclusao(); // <-- Verificação adicionada
    }

    // ... (restante das suas rotas de cenas permanecem iguais)
    public void IniciarJogo() { SceneManager.LoadScene("Menu"); }
    public void Jogar() { SceneManager.LoadScene("Frase"); }
    public void Ranking() { SceneManager.LoadScene("Ranking"); }
    public void Creditos() { SceneManager.LoadScene("Creditos"); }
    public void VoltarMenu() { SceneManager.LoadScene("Menu"); }
    public void Avatar() { SceneManager.LoadScene("SelecaoAvatar"); }
    public void SelecaoBioma() { SceneManager.LoadScene("SelecaoBioma"); }

    // ==========================================
    // ROTAS DA AMAZÔNIA
    // ==========================================
    public void Amazonia() { SceneManager.LoadScene("Amazonia"); }
    public void AmazoniaMapa() { SceneManager.LoadScene("AmazoniaMapa"); }
    public void AmazoniaRegioes() { SceneManager.LoadScene("AmazoniaRegioes"); }
    public void AmazoniaClima() { SceneManager.LoadScene("AmazoniaClima"); }
    public void AmazoniaVegetacao() { SceneManager.LoadScene("AmazoniaVegetacao"); }
    public void AmazoniaAmeacas() { SceneManager.LoadScene("AmazoniaAmeacas"); }
    public void AmazoniaFauna() { SceneManager.LoadScene("AmazoniaFauna"); }
    public void AmazoniaQuiz() { SceneManager.LoadScene("AmazoniaQuiz"); }

    // ==========================================
    // ROTAS DA CAATINGA
    // ==========================================
    public void Caatinga() { SceneManager.LoadScene("Caatinga"); }
    public void CaatingaMapa() { SceneManager.LoadScene("CaatingaMapa"); }
    public void CaatingaRegioes() { SceneManager.LoadScene("CaatingaRegioes"); }
    public void CaatingaClima() { SceneManager.LoadScene("CaatingaClima"); }
    public void CaatingaVegetacao() { SceneManager.LoadScene("CaatingaVegetacao"); }
    public void CaatingaAmeacas() { SceneManager.LoadScene("CaatingaAmeacas"); }
    public void CaatingaFauna() { SceneManager.LoadScene("CaatingaFauna"); }
    public void CaatingaQuiz() { SceneManager.LoadScene("CaatingaQuiz"); }

    // ==========================================
    // ROTAS DO CERRADO
    // ==========================================
    public void Cerrado() { SceneManager.LoadScene("Cerrado"); }
    public void CerradoMapa() { SceneManager.LoadScene("CerradoMapa"); }
    public void CerradoRegioes() { SceneManager.LoadScene("CerradoRegioes"); }
    public void CerradoClima() { SceneManager.LoadScene("CerradoClima"); }
    public void CerradoVegetacao() { SceneManager.LoadScene("CerradoVegetacao"); }
    public void CerradoAmeacas() { SceneManager.LoadScene("CerradoAmeacas"); }
    public void CerradoFauna() { SceneManager.LoadScene("CerradoFauna"); }
    public void CerradoQuiz() { SceneManager.LoadScene("CerradoQuiz"); }

    // ==========================================
    // ROTAS DA MATA ATLÂNTICA
    // ==========================================
    public void MataAtlantica() { SceneManager.LoadScene("MataAtlantica"); }
    public void MataAtlanticaMapa() { SceneManager.LoadScene("MataAtlanticaMapa"); }
    public void MataAtlanticaRegioes() { SceneManager.LoadScene("MataAtlanticaRegioes"); }
    public void MataAtlanticaClima() { SceneManager.LoadScene("MataAtlanticaClima"); }
    public void MataAtlanticaVegetacao() { SceneManager.LoadScene("MataAtlanticaVegetacao"); }
    public void MataAtlanticaAmeacas() { SceneManager.LoadScene("MataAtlanticaAmeacas"); }
    public void MataAtlanticaFauna() { SceneManager.LoadScene("MataAtlanticaFauna"); }
    public void MataAtlanticaQuiz() { SceneManager.LoadScene("MataAtlanticaQuiz"); }

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