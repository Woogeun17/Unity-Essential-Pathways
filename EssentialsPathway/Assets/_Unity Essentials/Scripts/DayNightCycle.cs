using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Day Cycle Settings")]
    [Tooltip("하루(24시간)가 실제로 몇 초에 해당하는지 설정합니다.")]
    public float dayLengthInSeconds = 120f; // 하루가 120초에 해당 (기본값)

    [Tooltip("초기 시간을 0~1 사이 비율로 설정 (0=자정, 0.5=정오)")]
    [Range(0f, 1f)]
    public float startTime = 0f;

    [Tooltip("회전 축 (기본값: X축 회전)")]
    public Vector3 rotationAxis = new Vector3(1f, 0f, 0f);

    private float timeOfDay = 0f; // 0~1 사이 값으로 하루의 진행 상태 (0=자정, 0.5=정오)
    private Light sunLight;

    void Start()
    {
        sunLight = GetComponent<Light>();
        timeOfDay = startTime;
    }

    void Update()
    {
        if (dayLengthInSeconds <= 0) return;

        // 하루의 진행 비율 (0~1)
        timeOfDay += Time.deltaTime / dayLengthInSeconds;
        if (timeOfDay > 1f) timeOfDay -= 1f; // 24시간 주기 반복

        // 현재 진행 비율을 360도 회전으로 변환
        float sunAngle = timeOfDay * 360f;
        transform.rotation = Quaternion.Euler(rotationAxis * sunAngle);
    }
}
