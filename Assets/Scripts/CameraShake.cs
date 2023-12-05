using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude) // Oyuncu engellerden birine �arparsa kameran�n parent objesi sars�l�r
    {
        Vector3 originalPos = transform.localPosition; // Sars�lma ba�lad���nda objenin pozisyonu al�n�r
        float elapsed = 0.0f;

        while (elapsed < duration) 
        {
            float x = Random.Range(-1f, 1f) * magnitude; // x ekseni i�in random bir float �retilir, magnitude ile �arp�l�r
            float y = Random.Range(-1f, 1f) * magnitude; // y ekseni i�in random bir float �retilir, magnitude ile �arp�l�r

            transform.localPosition = new Vector3(x, y, originalPos.z); // x ve y ekseninde sars�l�r

            elapsed += Time.deltaTime; // durationa e�it olana kadar sars�l�r

            yield return null;
        }

        transform.localPosition = originalPos; // Sars�lma bitince obje ba�lang�� pozisyonunu al�r
    }
}