using UnityEngine;

public class FloatEffect : MonoBehaviour
{
    public Transform player;          // Wildman_V2
    public Transform target;          // 海星，不填就用自己
    public RectTransform arrowUI;     // Player Arrow

    public float showDistance = 8f;
    public float amplitude = 30f;
    public float frequency = 1.6f;

    Vector2 startPos;
    bool lastInRange = false; // 上一帧是否在范围内

    void Awake()
    {
        if (arrowUI == null)
            arrowUI = GetComponentInChildren<RectTransform>(true);

        if (target == null)
            target = transform;

        if (arrowUI != null)
            startPos = arrowUI.anchoredPosition;
    }

    void Update()
    {
        if (player == null || target == null || arrowUI == null) return;

        // 计算水平距离
        Vector3 p1 = player.position;
        Vector3 p2 = target.position;
        p1.y = 0; p2.y = 0;
        float dist = Vector3.Distance(p1, p2);

        bool inRange = dist <= showDistance;

        // 进入范围的这一帧，重新记录起始位置并打开UI
        if (inRange && !lastInRange)
        {
            if (!arrowUI.gameObject.activeSelf)
                arrowUI.gameObject.SetActive(true);

            startPos = arrowUI.anchoredPosition;
        }

        // 离开范围，关闭UI
        if (!inRange && lastInRange)
        {
            arrowUI.gameObject.SetActive(false);
        }

        lastInRange = inRange;

        if (!inRange) return;

        // 在范围内才做浮动
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
        arrowUI.anchoredPosition = new Vector2(startPos.x, newY);
    }
}
