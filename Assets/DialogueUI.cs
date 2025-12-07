using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    public void Close()
    {
        // 先把对话框关掉
        gameObject.SetActive(false);

        // 然后加友谊点
        if (FriendshipManager.Instance != null)
        {
            FriendshipManager.Instance.AddFriendship(1);
        }
    }
}
