using UnityEngine;

public class StarfishDialogue : MonoBehaviour
{
    public GameObject dialogueUI;   // 对话框 Panel
    public float talkDistance = 3f; // 可对话距离
    public Transform player;        // 主角（Wildman_V2）

    private AudioSource audioSource;          // 海星的声音（可选）
    private bool hasGivenFriendship = false; // 是否已经给过友谊点

    void Start()
    {
        // 开始时先关掉对话框
        if (dialogueUI != null)
            dialogueUI.SetActive(false);

        // 取身上的 AudioSource（如果你给海星加了声音）
        audioSource = GetComponent<AudioSource>();
    }

    // 点击海星时触发：打开对话框 + 禁止移动
    void OnMouseDown()
    {
        if (dialogueUI == null || player == null) return;

        // 距离不够就不对话
        float dist = Vector3.Distance(player.position, transform.position);
        if (dist > talkDistance) return;

        // 禁止角色移动 —— 注意这里用的是 Hero
        Hero hero = player.GetComponent<Hero>();
        if (hero != null)
            hero.canMove = false;

        // 播放海星的声音（可选）
        if (audioSource != null)
            audioSource.Play();

        // 显示对话框
        dialogueUI.SetActive(true);
    }

    // 由对话框上的按钮调用：关闭对话 + 恢复移动 + 只加一次友谊点
    public void CloseDialogue()
    {
        if (dialogueUI != null)
            dialogueUI.SetActive(false);

        // 允许角色再次移动 —— 同样用 Hero
        if (player != null)
        {
            Hero hero = player.GetComponent<Hero>();
            if (hero != null)
                hero.canMove = true;
        }

        // 只在第一次对话结束时加友谊点
        if (!hasGivenFriendship && FriendshipManager.Instance != null)
        {
            FriendshipManager.Instance.AddFriendship(1);
            hasGivenFriendship = true;
        }
    }
}
