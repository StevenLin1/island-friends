using UnityEngine;

public class FloatEffect : MonoBehaviour
{
    public Transform player;          // Wildman_V2
    public Transform target;          // SeaStar，不填就用 transform
    public RectTransform arrowUI;     // Player Arrow（Image）

    public float showDistance = 8f;
    public float amplitude = 30f;
    public float frequency = 1.6f;

    Vector2 startPos;

    void Awake()
    {
        if (arrowUI == null)
            arrowUI = GetComponentInChildren<RectTransform>(true); // 找子物体UI

        if (target == null)
            target = transform;

        if (arrowUI != null)
            startPos = arrowUI.anchoredPosition;
    }

    void Update()
    {
        if (player == null || target == null || arrowUI == null) return;

        // 算水平距离
        Vector3 p1 = player.position;
        Vector3 p2 = target.position;
        p1.y = 0; p2.y = 0;
        float dist = Vector3.Distance(p1, p2);

        bool inRange = dist <= showDistance;

        // 只操作 arrowUI，不操作 this.gameObject
        if (arrowUI.gameObject.activeSelf != inRange)
            arrowUI.gameObject.SetActive(inRange);

        if (!inRange) return;

        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
        arrowUI.anchoredPosition = new Vector2(startPos.x, newY);
    }
}
