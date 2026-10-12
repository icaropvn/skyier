using UnityEngine;
using UnityEngine.Tilemaps;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Animator flagAnimator;
    [SerializeField] private Animator flagBaseAnimator;

    [SerializeField] private SpriteRenderer flagRenderer;
    [SerializeField] private SpriteRenderer flagBaseRenderer;
    [SerializeField] private SpriteRenderer flagstaffRenderer;

    public Transform respawnPoint;
    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated)
            return;

        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if (player != null)
        {
            player.SetCheckpoint(respawnPoint.position);
            activated = true;

            SFXManager.Instance.Play(
                SFXManager.Instance.checkpointSound
            );

            Color checkpointColor = new Color(1f, 0.8f, 0f);
            flagRenderer.color = checkpointColor;
            flagBaseRenderer.color = checkpointColor;
            flagstaffRenderer.color = checkpointColor;

            flagAnimator.SetTrigger("Activate");
            flagBaseAnimator.SetTrigger("Activate");
        }
    }
}
