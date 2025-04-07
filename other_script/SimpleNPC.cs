using UnityEngine;
using UnityEngine.UI;

public class SimpleNPC : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Text interactionPromptText;
    [SerializeField] private float interactionDistance = 3f;

    private string[] dialogueMessages = new string[] {
        "����Ű�� ���� ������ �� �ֽ��ϴ�.(������ ��� �����÷��� ���� �����ּ���)",
        "CtrlŰ�� �̿��Ͽ� ������ �� �� �ֽ��ϴ�.",
        "�� ���������� �������� �����߸��� ���� ���������� ���ư� �� �ֽ��ϴ�.",
        "��� ���������� �����ϸ� ������ Ŭ����˴ϴ�.",
        "�׷� ����� ���ϴ�."
    };

    private int currentDialogueIndex = 0;
    private bool hasInteracted = false;
    private bool isPlayerInRange = false;
    private bool isLastDialogueShown = false;

    private void Start()
    {
        if (dialoguePanel == null || dialogueText == null || interactionPromptText == null)
        {
            Debug.LogError("�ʿ��� UI ������Ʈ�� �Ҵ���� �ʾҽ��ϴ�!");
            return;
        }

        dialoguePanel.SetActive(false);
        if (interactionPromptText != null)
        {
            interactionPromptText.gameObject.SetActive(false);
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Collider playerCollider = player.GetComponent<Collider>();
        Collider npcCollider = GetComponent<Collider>();

        // �÷��̾�� NPC ���� �浹�� ����
        if (playerCollider != null && npcCollider != null)
        {
            Physics.IgnoreCollision(playerCollider, npcCollider);
        }
    }

    private void Update()
    {
        CheckPlayerDistance();

        if (isPlayerInRange && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                ShowNextDialogue();
            }
        }

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit2, 1.1f))
        {
            // ������ �浹�� �����Ǿ��� �� ó��
            // ex) ĳ���͸� �� ���� ����
        }

    }

    private void CheckPlayerDistance()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            isPlayerInRange = distance <= interactionDistance;

            if (interactionPromptText != null && !isLastDialogueShown)
            {
                interactionPromptText.gameObject.SetActive(isPlayerInRange);
            }
        }
    }

    private void ShowNextDialogue()
    {
        // ���� ��� ��縦 �������� �ʾҴٸ�
        if (currentDialogueIndex < dialogueMessages.Length)
        {
            dialoguePanel.SetActive(true);
            dialogueText.text = dialogueMessages[currentDialogueIndex];

            // ������ ������� Ȯ��
            if (currentDialogueIndex == dialogueMessages.Length - 1)
            {
                isLastDialogueShown = true;
                Invoke("HideDialogue", 3f);
            }

            currentDialogueIndex++;
        }
        // ��� ��縦 �� ������ٸ�
        else
        {
            HideDialogue();
            hasInteracted = true;
        }
    }

    private void HideDialogue()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        if (interactionPromptText != null)
        {
            interactionPromptText.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}