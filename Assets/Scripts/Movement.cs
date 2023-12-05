using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;

    [SerializeField] private Rigidbody rb;

    void Update()
    {
        float leftRight = Input.GetAxisRaw("Horizontal"); // Oyuncunun sa�a sola hareketi
        float forwardBackward = Input.GetAxisRaw("Vertical"); // Oyuncunun ileri geri hareketi

        Vector3 movement = speed * Time.deltaTime * new Vector3(leftRight, 0.0f, forwardBackward); // Oyuncunun klavye inputlar�na g�re yeni bir Vector3 ortaya ��kar

        transform.Translate(movement, Space.Self); // Oyuncu hareket eder
    }
}