using UnityEngine;

public class NonDeterministicFSM : MonoBehaviour
{
    private EntityState currentState;
    private System.Random random = new System.Random();
    void Start()
    {
        // Inicia em um estado aleat�rio (excluindo o estado 0 - Andando)
        ChangeToRandomState();
    }
    void Update()
    {
        // L�gica de execu��o baseada no estado atual
        ExecuteState();
    }
    void ChangeToRandomState()
    {
        // Gera um valor aleat�rio entre 1 e 3 (Atirando, Pulando ou Fugindo)
        int newState = random.Next(1, 4);
        currentState = (EntityState)newState;
        Debug.Log("Mudando para estado: " + currentState);
    }
    void ExecuteState()
    {
        switch (currentState)
        {
            case EntityState.Andando:
                // Implemente a l�gica de andar aqui
                break;
            case EntityState.Atirando:
                // Implemente a l�gica de atirar aqui
                break;
            case EntityState.Pulando:
                // Implemente a l�gica de pular aqui
                break;
            case EntityState.Fugindo:
                // Implemente a l�gica de fugir aqui
                break;
        }
    }
    // M�todo p�blico para for�ar uma mudan�a de estado aleat�rio
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
