using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private Animator _swordAnim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _swordAnim = transform.GetChild(1).GetComponent<Animator>();
    }

    public void Move(float _input)
    {
        _anim.SetFloat("x", Mathf.Abs(_input));
    }

    public void Jump(bool isJumping)
    {
        _anim.SetBool("isJumping", isJumping);
    }

    public void DoubleJump(bool isJumping)
    {
        _anim.SetBool("isDoubleJumping", isJumping);
    }

    public void IsGrounded(bool isGrounded)
    {
        _anim.SetBool("isGrounded", isGrounded);
    }

    public void Attack()
    {
        _anim.SetTrigger("attack");
        if (GameManager.Instance.HasFlameSword)
        {
            _anim.SetBool("flameSword", true);
            _swordAnim.SetTrigger("swordAnimation");
        }
    }

    public void Death()
    {
        _anim.SetTrigger("dead");
    }

    public void Hit()
    {
        _anim.SetTrigger("hit");
    }

    public bool OnHitState() => _anim.GetCurrentAnimatorStateInfo(0).IsName("Hit");
}
