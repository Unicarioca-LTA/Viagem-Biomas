using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {
    public static GameHandler instance;

    public AvatarData avatarSelecionado;
    public string nomeJogador;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject); // Impede que o objeto suma ao mudar de cena
        } else {
            Destroy(gameObject);
        }
    }
}

