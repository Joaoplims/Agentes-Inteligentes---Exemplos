using UnityEngine;

public class Exercicio : MonoBehaviour
{
    public Transform player;
    public float hp = 100;
    public bool inimigoPerto;
    public enum Estado { Patrulhar, Atacar, Fugir, UsarBomba, BuscarItem }
    private Estado estadoAtual;
    void PensarFSM()
    {
        switch (estadoAtual)
        {
            case Estado.Patrulhar:
                if (Vector3.Distance(transform.position, player.position) < 5f)
                    estadoAtual = Estado.Atacar;
                break;
            case Estado.Atacar:
                if (hp < 30f)
                    estadoAtual = Estado.Fugir;
                break;
        }
    }

    void OnDetectPlayer()
    {
        int escolha = Random.Range(0, 3);
        switch (escolha)
        {
            case 0: estadoAtual = Estado.Atacar; break;
            case 1: estadoAtual = Estado.Fugir; break;
            case 2: estadoAtual = Estado.UsarBomba; break;
        }
    }

    void DecidirAcao()
    {
        float rand = Random.Range(0f, 1f);
        if (rand < 0.1f && inimigoPerto) estadoAtual = Estado.Atacar;
        else if (rand < 0.1f) estadoAtual = Estado.Fugir;
        else estadoAtual = Estado.BuscarItem;
    }
}
