using UnityEngine;

public class PositionNode : MonoBehaviour, INodeGraph
{
    [SerializeField] private int index = -1;


    public int GetIndice()
    {
        return index;
    }

    public void SetIndice(int indice)
    {
        if (indice > -1)
        {
            index = indice;
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.25f);
    }


}
