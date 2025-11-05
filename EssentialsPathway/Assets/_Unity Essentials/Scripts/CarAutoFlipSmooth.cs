using UnityEngine;

public class CarAutoFlipSmooth : MonoBehaviour
{
    [Header("Flip Detection")]
    [Tooltip("���� �������ٰ� �Ǵ��� ���� ���� (0.3~0.5 ����)")]
    public float flipThreshold = 0.4f;

    [Tooltip("������ ���·� �����Ǵ� �ð� (��)")]
    public float flipDelay = 2f;

    [Header("Smooth Recovery Settings")]
    [Tooltip("ȸ�� ���� �ӵ� (���� Ŭ���� ����)")]
    public float rotationSpeed = 2f;

    [Tooltip("���� �� ���� ���� ����")]
    public float liftHeight = 0.5f;

    private Rigidbody rb;
    private float flipTimer = 0f;
    private bool isRecovering = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // �����߽��� �ʹ� ������ �� �������Ƿ� �ణ �����ָ� ��������
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    void Update()
    {
        if (isRecovering) return;

        // ������ ���������� Ȯ��
        if (Vector3.Dot(transform.up, Vector3.up) < flipThreshold)
        {
            flipTimer += Time.deltaTime;

            if (flipTimer >= flipDelay)
            {
                StartCoroutine(SmoothRecover());
                flipTimer = 0f;
            }
        }
        else
        {
            flipTimer = 0f;
        }
    }

    System.Collections.IEnumerator SmoothRecover()
    {
        isRecovering = true;

        // ���� ���� ��� ����
        rb.isKinematic = true;

        Quaternion startRot = transform.rotation;
        Quaternion targetRot = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + Vector3.up * liftHeight;

        float elapsed = 0f;
        float duration = 1.5f / rotationSpeed; // �ӵ��� ���� ȸ�� �ð� ����

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // ���� ���� ����
        transform.rotation = targetRot;
        transform.position = targetPos;

        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        isRecovering = false;
    }
}
