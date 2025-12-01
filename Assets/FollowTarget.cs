using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;      // 角色
    public float distance = 5f;   // 背后距离
    public float height = 2f;     // 高度
    public float smooth = 10f;    // 平滑度

    void LateUpdate()
    {
        if (target == null) return;

        // 角色背后的方向
        Vector3 back = -target.forward;

        // 期望位置：角色背后 + 高度
        Vector3 desiredPos = target.position + back * distance + Vector3.up * height;

        // 平滑移动
        transform.position = Vector3.Lerp(transform.position, desiredPos, smooth * Time.deltaTime);

        // 始终看向角色
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
