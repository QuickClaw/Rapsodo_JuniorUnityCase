using UnityEngine;

public class StartAndFinish : MonoBehaviour
{
    [SerializeField] private Movement Movement;
    [SerializeField] private TPS TPS;

    [SerializeField] private GameObject finishPanel;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject txt_Congratz;

    [SerializeField] private ParticleSystem finishEffect;

    [SerializeField] private AudioSource finishAudioSource;
    [SerializeField] private AudioClip finishAudioClip;

    bool mazeCompleted;

    private void Start()
    {
        if (startPanel.activeInHierarchy) // Ba�lang��ta Start Panel aktifse fare imleci g�r�n�r, kullan�labilir olur, hareket ve kamera kodlar� devre d��� b�rak�l�r
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            Movement.enabled = false;
            TPS.enabled = false;

            Time.timeScale = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
            if (mazeCompleted == false) // Labirent tamamlanmam��sa
            {
                txt_Congratz.SetActive(true); // Oyuncu labirenti tamamlay�nca Congratulations mesaj� aktif olur

                finishEffect.Play(); // Kazanma efekti oynat�l�r

                finishAudioSource.PlayOneShot(finishAudioClip); // Kazanma sesi oynat�l�r

                Invoke(nameof(TriggerFinish), 3f); // Bitirme paneli a��l�r, Congratulations mesaj� inaktif olur, hareket ve kamera kodu devre d��� b�rak�l�r
            }

            mazeCompleted = true;
        }
    }

    private void Update()
    {
        if (finishPanel.activeInHierarchy) // Finish Panel hiyerar�ide aktif olursa fare imleci g�r�n�r olur, kilidi a��l�r
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (!startPanel.activeInHierarchy)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void TriggerFinish()
    {
        finishPanel.SetActive(true);
        txt_Congratz.SetActive(false);

        Movement.enabled = false;
        TPS.enabled = false;

        Time.timeScale = 0f;
    }

    public void ContinueExplore() // Oyuncu labirenti bitirip biraz daha gezmeye karar verirse butona basar ve bu method �al���r
    {
        finishPanel.SetActive(false);

        Movement.enabled = true;
        TPS.enabled = true;

        Time.timeScale = 1f;
    }

    public void StartGame() // Sahne ba��nda Start Paneli inaktif yap�p oyuna ba�lar
    {
        startPanel.SetActive(false);

        Movement.enabled = true;
        TPS.enabled = true;

        Time.timeScale = 1f;
    }
}