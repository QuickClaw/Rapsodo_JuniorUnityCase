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
        if (startPanel.activeInHierarchy) // Baþlangýçta Start Panel aktifse fare imleci görünür, kullanýlabilir olur, hareket ve kamera kodlarý devre dýþý býrakýlýr
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
            if (mazeCompleted == false) // Labirent tamamlanmamýþsa
            {
                txt_Congratz.SetActive(true); // Oyuncu labirenti tamamlayýnca Congratulations mesajý aktif olur

                finishEffect.Play(); // Kazanma efekti oynatýlýr

                finishAudioSource.PlayOneShot(finishAudioClip); // Kazanma sesi oynatýlýr

                Invoke(nameof(TriggerFinish), 3f); // Bitirme paneli açýlýr, Congratulations mesajý inaktif olur, hareket ve kamera kodu devre dýþý býrakýlýr
            }

            mazeCompleted = true;
        }
    }

    private void Update()
    {
        if (finishPanel.activeInHierarchy) // Finish Panel hiyerarþide aktif olursa fare imleci görünür olur, kilidi açýlýr
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

    public void ContinueExplore() // Oyuncu labirenti bitirip biraz daha gezmeye karar verirse butona basar ve bu method çalýþýr
    {
        finishPanel.SetActive(false);

        Movement.enabled = true;
        TPS.enabled = true;

        Time.timeScale = 1f;
    }

    public void StartGame() // Sahne baþýnda Start Paneli inaktif yapýp oyuna baþlar
    {
        startPanel.SetActive(false);

        Movement.enabled = true;
        TPS.enabled = true;

        Time.timeScale = 1f;
    }
}