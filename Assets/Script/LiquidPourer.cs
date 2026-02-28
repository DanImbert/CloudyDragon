using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidPourer : MonoBehaviour
{
    float PourInterval = 0;
    public float pourVolume = 1f;
    public float pourForce = 1f;

    [SerializeField] private PourAudioSourceDriver pourAudio;

    LIquidContainer myContainer;
    ParticleSystem Particle;

    private void Awake()
    {
        myContainer = transform.parent.GetComponentInChildren<LIquidContainer>();
        Particle = GetComponent<ParticleSystem>();

        float rate = Particle.emission.rateOverTime.constant;
        PourInterval = rate > 0f ? 1f / rate : 0.05f;

        UpdateLiquid();
    }

    private void OnEnable()
    {
        if (Particle == null)
            Particle = GetComponent<ParticleSystem>();

        if (LiquidReciever.main != null)
            Particle.collision.AddPlane(LiquidReciever.main.transform);
    }

    void FixedUpdate()
    {
        PourLiquid();
        UpdatePourAudio();
    }

    public void PourLiquid()
    {
        if (PourCoroutine == null)
        {
            PourCoroutine = StartCoroutine(PourLiquidCoroutine());
        }
    }

    Coroutine PourCoroutine;

    public IEnumerator PourLiquidCoroutine()
    {
        StartPouring();
        while (transform.up.y < 0 && myContainer.totalVolume > 0)
        {
            yield return new WaitForSeconds(PourInterval);
        }
        StopPouring();
    }

    public void StartPouring()
    {
        UpdateLiquid();
        if (!Particle.isPlaying)
            Particle.Play();

        if (pourAudio != null)
            pourAudio.StartLoop();
    }

    public void StopPouring()
    {
        if (PourCoroutine != null)
            StopCoroutine(PourCoroutine);

        Particle.Stop();

        if (pourAudio != null)
            pourAudio.StopLoop();

        PourCoroutine = null;
    }

    public void UpdateLiquid()
    {
        ParticleSystem.TrailModule pSys = Particle.trails;
        pSys.colorOverTrail = new ParticleSystem.MinMaxGradient(myContainer.RimColor, myContainer.BodyColor);
    }

    void UpdatePourAudio()
    {
        if (pourAudio == null) return;

        float tiltAmount = Mathf.Clamp01(-transform.up.y); // 0 upright -> 1 inverted
        pourAudio.SetIntensity(tiltAmount);
    }

    public void TransferLiquid(LIquidContainer otherC)
    {
        myContainer.TransferLiquid(otherC, pourVolume * PourInterval, true);
    }

    private void OnDisable()
    {
        StopPouring();
    }
}
