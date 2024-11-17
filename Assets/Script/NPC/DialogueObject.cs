using System.Collections;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]

public class DialogueObject : ScriptableObject
{
    [SerializeField][TextArea] private string[] dialogue;

    public string[] Dialogue => dialogue;


}
