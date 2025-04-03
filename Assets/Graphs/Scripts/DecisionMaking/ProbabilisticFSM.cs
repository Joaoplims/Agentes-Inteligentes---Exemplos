using UnityEngine;

public class ProbabilisticFSM : MonoBehaviour
{
    public enum CharacterState
    {
        Walking = 0,
        Shooting = 1,
        Jumping = 2,
        Fleeing = 3
    }
    private CharacterState currentState;
    private System.Random random = new System.Random();
    void Start()
    {
        DetermineNextState();
    }
    void Update()
    {
        ExecuteCurrentState();
    }
    void DetermineNextState()
    {
        int randomNumber = random.Next(100); // Generates 0-99

        if (randomNumber < 60) // 0-59 (60% chance)
        {
            currentState = CharacterState.Shooting;
        }
        else if (randomNumber < 85) // 60-84 (25% chance)
        {
            currentState = CharacterState.Jumping;
        }
        else // 85-99 (15% chance)
        {
            currentState = CharacterState.Fleeing;
        }
        Debug.Log("New State: " + currentState + " | Rolled: " + randomNumber);
    }
    void ExecuteCurrentState()
    {
        switch (currentState)
        {
            case CharacterState.Walking:
                // Implement walking logic
                break;

            case CharacterState.Shooting:
                // Implement shooting logic
                break;

            case CharacterState.Jumping:
                // Implement jumping logic
                break;

            case CharacterState.Fleeing:
                // Implement fleeing logic
                break;
        }
    }
}
