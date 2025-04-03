using UnityEngine;

public class NonDeterministicFSM : MonoBehaviour
{
    private EntityState currentState;
    private System.Random random = new System.Random();
    void Start()
    {
        // Inicia em um estado aleatório (excluindo o estado 0 - Andando)
        ChangeToRandomState();
    }
    void Update()
    {
        // Lógica de execução baseada no estado atual
        ExecuteState();
    }
    void ChangeToRandomState()
    {
        // Gera um valor aleatório entre 1 e 3 (Atirando, Pulando ou Fugindo)
        int newState = random.Next(1, 4);
        currentState = (EntityState)newState;
        Debug.Log("Mudando para estado: " + currentState);
    }
    void ExecuteState()
    {
        switch (currentState)
        {
            case EntityState.Andando:
                // Implemente a lógica de andar aqui
                break;
            case EntityState.Atirando:
                // Implemente a lógica de atirar aqui
                break;
            case EntityState.Pulando:
                // Implemente a lógica de pular aqui
                break;
            case EntityState.Fugindo:
                // Implemente a lógica de fugir aqui
                break;
        }
    }
    // Método público para forçar uma mudança de estado aleatório
    public void ChangeState()
    {
        ChangeToRandomState();
    }
    public enum EntityState
    {
        Andando = 0,
        Atirando = 1,
        Pulando = 2,
        Fugindo = 3
    }
}
