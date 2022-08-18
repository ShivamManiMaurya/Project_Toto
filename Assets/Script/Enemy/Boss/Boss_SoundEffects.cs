using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_SoundEffects : MonoBehaviour
{
    [SerializeField] private AudioClip _arrivalSfx, _enrageSfx, _attackSfx, _rageAttackSfx, _deathSfx, _hitSfx;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayArrivalSfx()
    {
        audioSource.PlayOneShot(_arrivalSfx); 
    }

    public void PlayEnrageSfx()
    {
        audioSource.PlayOneShot(_enrageSfx);
    }

    public void PlayAttackSfx()
    {
        audioSource.PlayOneShot(_attackSfx);
    }

    public void PlayRageAttackSfx()
    {
        audioSource.PlayOneShot(_rageAttackSfx);
    }

    public void PlayHitSfx()
    {
        audioSource.PlayOneShot(_hitSfx);
    }

    public void PlayDeathSfx()
    {
        audioSource.PlayOneShot(_deathSfx);
    }


}
