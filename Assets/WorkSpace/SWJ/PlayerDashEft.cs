using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerDashEft : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle_Charge;
    [SerializeField] private ParticleSystem particle_Use;

    [ContextMenu("Play_Charge")]
    public void Play_Charge()
    {
        particle_Charge.Play();
    }
    [ContextMenu("Stop_Charge")]
    public void Stop_Charge()
    {
        particle_Charge.Stop();
    }

    [ContextMenu("Play_Use")]
    public void Play_Use()
    {
        particle_Use.transform.DOKill();

        particle_Use.Play();
        particle_Use.transform.DOLocalMove(Vector2.zero, 0.1f).OnComplete(() => particle_Use.Stop());
    }
    [ContextMenu("Stop_Use")]
    public void Stop_Use()
    {
        particle_Use.transform.DOKill();

        particle_Use.Stop();
    }
}
