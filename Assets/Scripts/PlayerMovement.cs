using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float raycastDistance = 2f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private PlayerInputActions inputActions;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private GameObject lastHitZone;
    private Color originalColor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        if (inputActions != null)
        {
            inputActions.Player.Move.performed -= OnMove;
            inputActions.Player.Move.canceled -= OnMoveCanceled;
            inputActions.Player.Disable();
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;

        bool isWalking = moveInput != Vector2.zero;
        animator.SetBool("isWalking", isWalking);

        if (moveInput.x < 0)
            spriteRenderer.flipX = true;
        else if (moveInput.x > 0)
            spriteRenderer.flipX = false;

        HandleRaycast();
    }

    private void HandleRaycast()
    d{
    Vector2 direction = moveInput != Vector2.zero ? moveInput.normalized : Vector2.down;

    Debug.DrawRay(transform.position, direction * raycastDistance, Color.red);
    
    RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, raycastDistance);

    bool foundZone = false;
    foreach (RaycastHit2D hit in hits)
    {
        if (hit.collider.gameObject == gameObject) continue;

        if (hit.collider.CompareTag("MentorshipLounge") ||
            hit.collider.CompareTag("DevicesRoom") ||
            hit.collider.CompareTag("SkillsLab") ||
            hit.collider.CompareTag("CommunityBoard"))
        {
            GameObject hitZone = hit.collider.gameObject;
            foundZone = true;

            if (hitZone != lastHitZone)
            {
                ResetLastZone();
                lastHitZone = hitZone;
                SpriteRenderer sr = hitZone.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    originalColor = sr.color;
                    sr.color = new Color(
                        Mathf.Min(originalColor.r + 0.2f, 1f),
                        Mathf.Min(originalColor.g + 0.2f, 1f),
                        Mathf.Min(originalColor.b + 0.2f, 1f),
                        originalColor.a
                    );
                }
            }
            break;
        }
    }

    if (!foundZone)
        ResetLastZone();
    }

    private void ResetLastZone()
    {
        if (lastHitZone != null)
        {
            SpriteRenderer sr = lastHitZone.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.color = originalColor;
            lastHitZone = null;
        }
    }
}