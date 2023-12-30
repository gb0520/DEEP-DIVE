using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Star : ItemBase
{
    [SerializeField] private ParticleSystem boomParticle;

    protected override void CrashObj()
    {
        target.Bomb();
        boomParticle.Play();

        transform.DOScale(Vector2.zero, 0.75f).SetEase(Ease.OutQuart);
        transform.DOLocalMove(Vector2.one, 1.2f).OnComplete(() => gameObject.SetActive(false));
    }
}
