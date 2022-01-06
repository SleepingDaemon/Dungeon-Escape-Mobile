using UnityEngine;

public class Grounding : MonoBehaviour
{
    [SerializeField] private LayerMask  _layerMask;
    [SerializeField] private float      _groundCheckDistance;
    [SerializeField] private bool       _isGrounded;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, _layerMask);
        Debug.DrawRay(transform.position, Vector2.down * _groundCheckDistance, Color.red);

        if (hit.collider != null)
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }

    public bool IsGrounded() => _isGrounded;
}
