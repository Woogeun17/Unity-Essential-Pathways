using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    private Animator doorAnimator;

    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어만 문을 열 수 있도록
        if (other.CompareTag("Player"))
        {
            if (doorAnimator != null)
            {
                doorAnimator.SetTrigger("Door_Open");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 플레이어가 범위를 벗어나면 문 닫기
        if (other.CompareTag("Player"))
        {
            if (doorAnimator != null)
            {
                doorAnimator.SetTrigger("Door_Close");
            }
        }
    }
}
