using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource    _source;
    [SerializeField] private AudioClip      _hoverButton;
    [SerializeField] private AudioClip      _clickButton;

    public void HoverButton()
    {
        _source.PlayOneShot(_hoverButton);
    }

    public void SelectButton()
    {
        _source.PlayOneShot(_clickButton);
    }
}
