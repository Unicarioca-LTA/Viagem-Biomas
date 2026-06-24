using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PainelManager : MonoBehaviour
{
    [Header("PAINEL: Painel 1")]
    public GameObject painelUm;    // Arraste o painel de campo vazio aqui

    [Header("PAINEL: Painel 2")]
    public GameObject painelDois;    // Arraste o painel de campo vazio aqui

    [Header("PAINEL: Painel 3")]
    public GameObject painelTres;    // Arraste o painel de campo vazio aqui

    [Header("PAINEL: Painel 4")]
    public GameObject painelQuatro;    // Arraste o painel de campo vazio aqui


    public void AbrirPainelUm() {
        if (painelUm != null) {
            painelUm.SetActive(true);
        } else {
        }
    }
    public void FecharPainelUm() {
        if (painelUm != null) {
            painelUm.SetActive(false);
        }
    }
    public void AbrirPainelDois() {
        if (painelDois != null) {
            painelDois.SetActive(true);
        } else {
        }
    }
    public void FecharPainelDois() {
        if (painelDois != null) {
            painelDois.SetActive(false);
        }
    }
    public void AbrirPainelTres() {
        if (painelTres != null) {
            painelTres.SetActive(true);
        } else {
        }
    }
    public void FecharPainelTres() {
        if (painelTres != null) {
            painelTres.SetActive(false);
        }
    }
    public void AbrirPainelQuatro() {
        if (painelQuatro != null) {
            painelQuatro.SetActive(true);
        } else {
        }
    }
    public void FecharPainelQuatro() {
        if (painelQuatro != null) {
            painelQuatro.SetActive(false);
        }
    }
}
