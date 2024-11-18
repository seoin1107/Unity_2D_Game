using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;

    private TypewriterEffect typewriterEffect;

    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        CloseDialogueBox();
        ShowDialogue(testDialogue);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialouge(dialogueObject));
    }

    private IEnumerator StepThroughDialouge(DialogueObject dialogueObject) 
    {
        //yield return new WaitForSeconds(1.0f);

        foreach(string dialouge in dialogueObject.Dialogue)
        {
            //타이핑효과
            yield return typewriterEffect.Run(dialouge, textLabel);
            //G눌러야 다음대화
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.G));
        }

        CloseDialogueBox();
    }

    private void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
