using UnityEngine;

public class StarfishDialogue : MonoBehaviour
{
    public GameObject dialogueUI;   // 对话框Panel
    public float talkDistance = 3f; // 可对话距离
    public Transform player;        // 角色（Wildman_V2）

    void Start()
    {
        if (dialogueUI != null)
            dialogueUI.SetActive(false);
    }

    void OnMouseDown()
    {
        if (dialogueUI == null || player == null) return;

        // 只在靠近时才允许对话
        float dist = Vector3.Distance(player.position, transform.position);
        if (dist > talkDistance) return;

        // 显示对话框
        dialogueUI.SetActive(true);
    }
}
