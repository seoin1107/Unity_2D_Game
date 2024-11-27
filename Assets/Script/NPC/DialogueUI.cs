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
        //Ÿ���� ȿ���� ����
        typewriterEffect = GetComponent<TypewriterEffect>();
        //������ ���� ��ȭUI �Ⱥ��̴� ����
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
            //Ÿ����ȿ��
            yield return typewriterEffect.Run(dialouge, textLabel);
            //G������ ������ȭ
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
