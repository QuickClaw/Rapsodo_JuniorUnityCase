using UnityEngine;
using TMPro;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Movement Movement; // Hareket kodu
    [SerializeField] private CameraShake CameraShake; // Kamera Titre�mesi kodu

    [SerializeField] private ParticleSystem deathEffect;

    [SerializeField] private AudioSource deathAudioSource;
    [SerializeField] private AudioClip deathAudioClip;

    [SerializeField] private Collider playerCollider;

    [SerializeField] private TMP_Text txtDeaths; 

    public float shakeDuration;
    public float shakeMagnitude;  

    private int deathCount;

    private Vector3 respawnLocation;

    bool isDead; // Oyuncu �l� m�, de�il mi

    void Start()
    {
        isDead = false;

        Movement.enabled = true; // Sahne ba�lad���nda ne olursa olsun oyuncu hareket edebilmelidir

        respawnLocation = transform.position; // Oyuncunun ba�lang�� noktas� kaydedilir

        deathCount = PlayerPrefs.GetInt("Deaths");
        txtDeaths.text = "<color=white>Deaths:</color> " + deathCount.ToString(); // Sahne ba�lad���nda kaydedilmi� �l�m say�s� UI da g�z�k�r
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead == false) // Oyuncuya �ld�kten sonra do�madan 1 saniye i�inde tekrardan engel temas etmemesi i�in 'isDead' boolean kullan�l�r
        {
            if (other.tag is "Obstacle") // Oyuncu engel ile temas etti�i an �al���r
            {
                StartCoroutine(CameraShake.Shake(shakeDuration, shakeMagnitude)); // Kamera titre�mesi
                playerCollider.enabled = false; 

                deathCount += 1; // �l�m say�s� 1 artt�r�l�r
                PlayerPrefs.SetInt("Deaths", deathCount); // PlayerPrefs ile kaydedilir

                txtDeaths.text = "<color=white>Deaths:</color> " + deathCount.ToString();

                Movement.enabled = false; // Oyuncuyu hareket ettiren hareket kodu devre d��� b�rak�l�r
                isDead = true; // Oyuncu �ld�

                deathEffect.Play(); // Temas etme efekti oynat�l�r
                deathAudioSource.PlayOneShot(deathAudioClip); // Temas etme sesi �al�n�r

                Invoke(nameof(Respawn), 1); // Oyuncu 1 saniye sonra ba�lang�� noktas�na gider, hareket kodu aktif olur ve oyuncunun collider aktifle�ir
            }
        }
    }

    void Respawn()
    {
        transform.position = respawnLocation;

        isDead = false;

        Movement.enabled = true;
        Movement.speed = 4f;

        playerCollider.enabled = true;
    }
}