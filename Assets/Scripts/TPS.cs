using UnityEngine;

public class TPS : MonoBehaviour
{
    public float upLimit;
    public float downLimit;
    public float interpolatedTime = 0;

    private float mouseX, mouseY;

    [SerializeField] private Transform target;
    [SerializeField] private Transform player;

    [SerializeField] private Camera mainCamera;

    void Start()
    {
        mainCamera.fieldOfView = 120;
    }

    void LateUpdate()
    {
        CameraControl();
    }

    private void Update()
    {
        Zoom();
    }

    public void CameraControl()
    {
        mouseX += Input.GetAxis("Mouse X") * 1.5f;
        mouseY += Input.GetAxis("Mouse Y") * -1.5f;
        mouseY = Mathf.Clamp(mouseY, downLimit, upLimit); // Kamera yukar� a�a�� hareket ederken s�n�rland�r�l�r

        transform.LookAt(target); // Camera Player objesinin i�indeki Target parent objesine do�ru bakar

        target.rotation = Quaternion.Euler(mouseY, mouseX, 0); // Target parent objesi kullanc�n�n mouse hareketlerine g�re d�ner
        player.rotation = Quaternion.Euler(0, mouseX, 0); // Player sadece kullanc�n�n x ekseninde d�n�� yapar
    }

    public void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0) // Kameran�n g�r�� a��s�n� artt�rmak i�in mouse tekerle�i kullan�l�r
        {
            mainCamera.fieldOfView += 2.5f;
            mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, 45, 120); // Kamera g�r�� a��s�n� iki float aras�nda s�n�rlamak i�in Mathf.Clamp kullan�ld�
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // // Kameran�n g�r�� a��s�n� azaltmak i�in mouse tekerle�i kullan�l�r
        {
            mainCamera.fieldOfView -= 2.5f;
            this.interpolatedTime += Time.deltaTime;
            mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, 45, 120); // Kamera g�r�� a��s�n� iki float aras�nda s�n�rlamak i�in Mathf.Clamp kullan�ld�
        }
    }
}