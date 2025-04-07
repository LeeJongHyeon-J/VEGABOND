using UnityEngine;
using UnityEngine.UI;

public class SimpleNPC : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Text interactionPromptText;
    [SerializeField] private float interactionDistance = 3f;

    private string[] dialogueMessages = new string[] {
        "방향키를 눌러 움직일 수 있습니다.(설명을 계속 들으시려면 저를 눌러주세요)",
        "Ctrl키를 이용하여 점프를 할 수 있습니다.",
        "각 스테이지의 보스몹을 쓰러뜨리면 다음 스테이지로 나아갈 수 있습니다.",
        "모든 스테이지를 격파하면 던전이 클리어됩니다.",
        "그럼 행운을 빕니다."
    };

    private int currentDialogueIndex = 0;
    private bool hasInteracted = false;
    private bool isPlayerInRange = false;
    private bool isLastDialogueShown = false;

    private void Start()
    {
        if (dialoguePanel == null || dialogueText == null || interactionPromptText == null)
        {
            Debug.LogError("필요한 UI 컴포넌트가 할당되지 않았습니다!");
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

        // 플레이어와 NPC 간의 충돌을 무시
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
            // 땅과의 충돌이 감지되었을 때 처리
            // ex) 캐릭터를 땅 위에 고정
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
        // 아직 모든 대사를 보여주지 않았다면
        if (currentDialogueIndex < dialogueMessages.Length)
        {
            dialoguePanel.SetActive(true);
            dialogueText.text = dialogueMessages[currentDialogueIndex];

            // 마지막 대사인지 확인
            if (currentDialogueIndex == dialogueMessages.Length - 1)
            {
                isLastDialogueShown = true;
                Invoke("HideDialogue", 3f);
            }

            currentDialogueIndex++;
        }
        // 모든 대사를 다 보여줬다면
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