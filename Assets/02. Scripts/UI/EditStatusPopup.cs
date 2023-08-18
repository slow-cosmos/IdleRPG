using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditStatusPopup : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject status;

    public void Start()
    {
        MakeStatus().Init("몬스터 생성 주기", 1);
        MakeStatus().Init("몬스터 최대 수", 1);
        MakeStatus().Init("몬스터의 체력", 50);
        MakeStatus().Init("몬스터의 공격력", 5);
        MakeStatus().Init("몬스터의 초당 공격 횟수", 1);
        MakeStatus().Init("몬스터의 공격 사거리", 1);
        MakeStatus().Init("플레이어의 체력", 50);
        MakeStatus().Init("플레이어의 공격력", 5);
        MakeStatus().Init("플레이어의 초당 공격 횟수", 1);
        MakeStatus().Init("플레이어의 공격 사거리", 1);
        MakeStatus().Init("플레이어의 스킬2 범위", 1);
    }

    public void PopupButton()
    {
        gameObject.SetActive(true);
    }

    public void ExitButton()
    {
        gameObject.SetActive(false);
    }

    private Status MakeStatus()
    {
        Status instance = Instantiate(status, parent).GetComponent<Status>();
        return instance;
    }
}
