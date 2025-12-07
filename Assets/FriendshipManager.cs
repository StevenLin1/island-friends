using UnityEngine;
using TMPro;

public class FriendshipManager : MonoBehaviour
{
    public static FriendshipManager Instance;

    public int friendshipPoints = 0;          // 当前友谊点
    public TextMeshProUGUI friendshipText;    // 左上角显示文字

    void Awake()
    {
        // 简单单例
        Instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddFriendship(int amount)
    {
        friendshipPoints += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (friendshipText != null)
            friendshipText.text = "Friendship: " + friendshipPoints;
    }
}
