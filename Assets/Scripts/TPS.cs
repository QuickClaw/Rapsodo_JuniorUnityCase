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
        mouseY = Mathf.Clamp(mouseY, downLimit, upLimit); // Kamera yukarý aþaðý hareket ederken sýnýrlandýrýlýr

        transform.LookAt(target); // Camera Player objesinin içindeki Target parent objesine doðru bakar

        target.rotation = Quaternion.Euler(mouseY, mouseX, 0); // Target parent objesi kullancýnýn mouse hareketlerine göre döner
        player.rotation = Quaternion.Euler(0, mouseX, 0); // Player sadece kullancýnýn x ekseninde dönüþ yapar
    }

    public void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0) // Kameranýn görüþ açýsýný arttýrmak için mouse tekerleði kullanýlýr
        {
            mainCamera.fieldOfView += 2.5f;
            mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, 45, 120); // Kamera görüþ açýsýný iki float arasýnda sýnýrlamak için Mathf.Clamp kullanýldý
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // // Kameranýn görüþ açýsýný azaltmak için mouse tekerleði kullanýlýr
        {
            mainCamera.fieldOfView -= 2.5f;
            this.interpolatedTime += Time.deltaTime;
            mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, 45, 120); // Kamera görüþ açýsýný iki float arasýnda sýnýrlamak için Mathf.Clamp kullanýldý
        }
    }
}