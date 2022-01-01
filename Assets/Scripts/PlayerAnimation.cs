using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    public void Move(float _input)
    {
        _anim.SetFloat("x", Mathf.Abs(_input));
    }

    public void Jump(bool isJumping)
    {
        _anim.SetBool("isJumping", isJumping);
    }
}
