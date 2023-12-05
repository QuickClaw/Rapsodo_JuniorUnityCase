using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform mainCam;

    void LateUpdate()
    {
        transform.LookAt(transform.position + mainCam.forward); // Obje her zaman kameraya dönük olur
    }
}