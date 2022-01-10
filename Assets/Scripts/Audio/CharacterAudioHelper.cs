using UnityEngine;

public class CharacterAudioHelper : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource = null;

    [Header("Character Sounds")]
    [SerializeField] private AudioClip _footStepSound;  //Footstep
    [SerializeField] private AudioClip _attackSound;    //Attack
    [SerializeField] private AudioClip _hurtSound;      //Hurt
    [SerializeField] private AudioClip _deathSound;     //Death

    [Header("Player Specific Sounds")]
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _attackTwoSound;

    public void PlayFootStepSound() => AudioManager.Instance.PlaySound(_audioSource, _footStepSound);
    public void PlayAttackSound() => AudioManager.Instance.PlaySound(_audioSource, _attackSound);
    public void PlayHurtSound() => AudioManager.Instance.PlaySound(_audioSource, _hurtSound);
    public void PlayJumpSound() => AudioManager.Instance.PlaySound(_audioSource, _jumpSound);
    public void PlayDeathSound() => AudioManager.Instance.PlaySound(_audioSource, _deathSound);
    public void PlayAttackTwoSound() => AudioManager.Instance.PlaySound(_audioSource, _attackTwoSound);
}
