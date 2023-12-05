using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;

    [SerializeField] private Rigidbody rb;

    void Update()
    {
        float leftRight = Input.GetAxisRaw("Horizontal"); // Oyuncunun saða sola hareketi
        float forwardBackward = Input.GetAxisRaw("Vertical"); // Oyuncunun ileri geri hareketi

        Vector3 movement = speed * Time.deltaTime * new Vector3(leftRight, 0.0f, forwardBackward); // Oyuncunun klavye inputlarýna göre yeni bir Vector3 ortaya çýkar

        transform.Translate(movement, Space.Self); // Oyuncu hareket eder
    }
}