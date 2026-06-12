using UnityEngine;
using TMPro;

public class ZoneTrigger : MonoBehaviour
{
    [SerializeField] private string zoneName;
    [SerializeField] private GameObject interactPrompt;

    private bool playerInZone = false;
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Interaction.performed += OnInteract;
    }

    private void OnDisable()
    {
        if (inputActions != null)
        {
            inputActions.Player.Interaction.performed -= OnInteract;
            inputActions.Player.Disable();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            if (interactPrompt != null)
                interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            if (interactPrompt != null)
                interactPrompt.SetActive(false);
        }
    }

    private void OnInteract(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
    if (playerInZone && !ProgressTracker.Instance.IsZoneDone(zoneName))
    {
        ChallengeManager.Instance.OpenChallenge(zoneName);
    }
    }
}