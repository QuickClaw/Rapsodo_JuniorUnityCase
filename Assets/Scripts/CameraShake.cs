using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude) // Oyuncu engellerden birine çarparsa kameranýn parent objesi sarsýlýr
    {
        Vector3 originalPos = transform.localPosition; // Sarsýlma baþladýðýnda objenin pozisyonu alýnýr
        float elapsed = 0.0f;

        while (elapsed < duration) 
        {
            float x = Random.Range(-1f, 1f) * magnitude; // x ekseni için random bir float üretilir, magnitude ile çarpýlýr
            float y = Random.Range(-1f, 1f) * magnitude; // y ekseni için random bir float üretilir, magnitude ile çarpýlýr

            transform.localPosition = new Vector3(x, y, originalPos.z); // x ve y ekseninde sarsýlýr

            elapsed += Time.deltaTime; // durationa eþit olana kadar sarsýlýr

            yield return null;
        }

        transform.localPosition = originalPos; // Sarsýlma bitince obje baþlangýç pozisyonunu alýr
    }
}