using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.layer == LayerMask.NameToLayer("Player")
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    public void Interact(Player2D player2D)
    {
        player2D.DialogueUI.ShowDialogue(dialogueObject);
    }
}
