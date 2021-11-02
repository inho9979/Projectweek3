using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{

    public bool playEffect = false;
    public ParticleSystem attackEffect;
    public ParticleSystem healEffect;
    public ParticleSystem powerEffect;
    public ParticleSystem knockBackEffect;
    public ParticleSystem finishAuraEffect;

    float timer = 0f;
    void Start()
    {
        playEffect = true;
        attackEffect.Stop();
        healEffect.Stop();
        powerEffect.Stop();
        knockBackEffect.Stop();
        finishAuraEffect.Stop();
    }

    public void AttackEffect()
    {
        attackEffect.Play();
    }

    public void HealEffect()
    {
        healEffect.Play();
    }

    public void PowerEffect()
    {
        powerEffect.Play();
    }
    public void KnockBackEffect()
    {
        knockBackEffect.Play();
    }

    public void FinishAuraEffect()
    {
        if (finishAuraEffect != null)
        {
            finishAuraEffect.Play();
        }
    }

    public void FinishAuraEffectOff()
    {
        if (finishAuraEffect != null)
        {
            finishAuraEffect.Stop();
            Destroy(finishAuraEffect);
        }
    }

    //void Update()
    //{
    //    timer += Time.deltaTime;
    //    if (timer > 1f)
    //    {
    //        if (playEffect == true)
    //            particleObject.Play();
    //        else
    //            particleObject.Stop();
    //        timer = 0f;
    //    }
    //}
}
