using System.Collections;
using System.Collections.Generic;
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

    public void Attack()
    {
        _anim.SetTrigger("attack");
        _swordAnim.SetTrigger("swordAnimation");
    }
}
