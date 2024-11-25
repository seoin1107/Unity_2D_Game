using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Player") && collider.TryGetComponent(out Player2D player2D))
        {
            player2D.Interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Player") && collider.TryGetComponent(out Player2D player2D))
        {
            if(player2D.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player2D.Interactable = null;
            }
        }
    }

    public void Interact(Player2D player2D)
    {
        player2D.DialogueUI.ShowDialogue(dialogueObject);
    }
}
