using UnityEngine;
using UnityEngine.AI;

public class Hero : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    // 记录上一次是否在移动
    private bool wasMoving = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // 建议在代码里关掉 NavMeshAgent 自带的瞬间旋转
        agent.updateRotation = false;
    }

    void Update()
    {
        // 鼠标左键点击地面移动
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                agent.isStopped = false;
                agent.SetDestination(hit.point);
            }
        }

        // 当前是否在移动
        bool isMoving = agent.velocity.magnitude > 0.05f && !agent.isStopped;

        // 刚刚从“停”变成“动”这一刻：随机一次 Walk 或 Run
        if (isMoving && !wasMoving)
        {
            if (Random.value < 0.5f)
                animator.Play("Walk");   // Animator 里走路状态的名字
            else
                animator.Play("Run");    // Animator 里奔跑状态的名字
        }

        // 刚刚从“动”变成“停”这一刻：切回 Idle
        if (!isMoving && wasMoving)
        {
            animator.Play("Idle");       // Animator 里待机状态的名字
        }

        // 平滑转向移动方向
        SmoothRotate();

        wasMoving = isMoving;
    }

    void SmoothRotate()
    {
        // 移动得足够快时才转身
        if (agent.velocity.magnitude < 0.01f) return;

        // 水平面的移动方向
        Vector3 dir = new Vector3(agent.velocity.x, 0f, agent.velocity.z).normalized;
        if (dir.sqrMagnitude < 0.0001f) return;

        // 目标朝向
        Quaternion targetRot = Quaternion.LookRotation(dir);

        // 旋转速度（可调小一点让转身更慢）
        float rotateSpeed = 2f;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
    }
}
