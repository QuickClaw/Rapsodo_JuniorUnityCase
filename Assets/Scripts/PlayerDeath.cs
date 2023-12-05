using UnityEngine;
using TMPro;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Movement Movement; // Hareket kodu
    [SerializeField] private CameraShake CameraShake; // Kamera Titreþmesi kodu

    [SerializeField] private ParticleSystem deathEffect;

    [SerializeField] private AudioSource deathAudioSource;
    [SerializeField] private AudioClip deathAudioClip;

    [SerializeField] private Collider playerCollider;

    [SerializeField] private TMP_Text txtDeaths; 

    public float shakeDuration;
    public float shakeMagnitude;  

    private int deathCount;

    private Vector3 respawnLocation;

    bool isDead; // Oyuncu ölü mü, deðil mi

    void Start()
    {
        isDead = false;

        Movement.enabled = true; // Sahne baþladýðýnda ne olursa olsun oyuncu hareket edebilmelidir

        respawnLocation = transform.position; // Oyuncunun baþlangýç noktasý kaydedilir

        deathCount = PlayerPrefs.GetInt("Deaths");
        txtDeaths.text = "<color=white>Deaths:</color> " + deathCount.ToString(); // Sahne baþladýðýnda kaydedilmiþ ölüm sayýsý UI da gözükür
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead == false) // Oyuncuya öldükten sonra doðmadan 1 saniye içinde tekrardan engel temas etmemesi için 'isDead' boolean kullanýlýr
        {
            if (other.tag is "Obstacle") // Oyuncu engel ile temas ettiði an çalýþýr
            {
                StartCoroutine(CameraShake.Shake(shakeDuration, shakeMagnitude)); // Kamera titreþmesi
                playerCollider.enabled = false; 

                deathCount += 1; // Ölüm sayýsý 1 arttýrýlýr
                PlayerPrefs.SetInt("Deaths", deathCount); // PlayerPrefs ile kaydedilir

                txtDeaths.text = "<color=white>Deaths:</color> " + deathCount.ToString();

                Movement.enabled = false; // Oyuncuyu hareket ettiren hareket kodu devre dýþý býrakýlýr
                isDead = true; // Oyuncu öldü

                deathEffect.Play(); // Temas etme efekti oynatýlýr
                deathAudioSource.PlayOneShot(deathAudioClip); // Temas etme sesi çalýnýr

                Invoke(nameof(Respawn), 1); // Oyuncu 1 saniye sonra baþlangýç noktasýna gider, hareket kodu aktif olur ve oyuncunun collider aktifleþir
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