using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _hoverButton;
    [SerializeField] private AudioClip _clickButton;
    [SerializeField] private Button _start;
    [SerializeField] private Button _quit;

    public void HoverButton()
    {
        _source.PlayOneShot(_hoverButton);
    }

    public void SelectButton()
    {
        _source.PlayOneShot(_clickButton);
    }
}
