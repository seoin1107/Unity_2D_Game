using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    //[SerializeField] private DialogueObject testDialogue;

    public bool IsOpen { get; private set; }

    private TypewriterEffect typewriterEffect;

    private void Start()
    {
        //타이핑 효과를 쓴다
        typewriterEffect = GetComponent<TypewriterEffect>();
        //시작할 때는 대화UI 안보이는 상태
        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
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
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
