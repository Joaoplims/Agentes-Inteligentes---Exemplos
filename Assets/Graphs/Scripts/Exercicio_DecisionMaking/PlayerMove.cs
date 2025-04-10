using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;


    // Update is called once per frame
    void Update()
    {
        Vector3 inDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) *speed;
    }
}
