using UnityEngine;
using UnityEngine.UI;

public class SirenEffectUI : MonoBehaviour
{
    public Image screenImage;
    public AudioSource sirenSound;

    [Header("Configurações Visuais")]
    public Color baseColor = Color.red; // agora você escolhe no Inspector!
    public float speed = 2f;
    public float maxAlpha = 0.6f;

    [Header("Configurações de Som")]
    public bool loopSiren = true;
    public float pitchVariation = 0.3f;

    private float basePitch;

    void Start()
    {
        if (screenImage == null)
            screenImage = GetComponent<Image>();

        if (sirenSound == null)
            sirenSound = GetComponent<AudioSource>();

        basePitch = sirenSound != null ? sirenSound.pitch : 1f;

        if (sirenSound != null)
        {
            sirenSound.loop = loopSiren;
            sirenSound.Play();
        }
    }

    void Update()
    {
        float sin = (Mathf.Sin(Time.time * speed) + 1f) / 2f;

        // Atualiza cor (piscando)
        float alpha = sin * maxAlpha;
        screenImage.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);

        // Atualiza som (sincroniza o pitch)
        if (sirenSound != null)
        {
            sirenSound.pitch = basePitch + (sin - 0.5f) * pitchVariation;
        }
    }
}
