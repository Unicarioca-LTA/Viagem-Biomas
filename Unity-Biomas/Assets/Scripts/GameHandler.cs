using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;

    public AvatarData avatarSelecionado;
    public string nomeJogador = "Jogador"; // valor padrão

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // mantém entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 👇 método pra definir o nome com segurança
    public void DefinirNome(string nome)
    {
        if (string.IsNullOrEmpty(nome))
        {
            nomeJogador = "Jogador";
        }
        else
        {
            nomeJogador = nome;
        }
    }

    // 👇 método opcional pra definir avatar
    public void DefinirAvatar(AvatarData avatar)
    {
        avatarSelecionado = avatar;
    }
}