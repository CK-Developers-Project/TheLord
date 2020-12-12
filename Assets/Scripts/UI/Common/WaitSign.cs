using System;
using UnityEngine;


public class WaitSign : MonoBehaviour
{
    const float Packet_Error_Time = 180f;
    float roamingTime;

    public bool IsActive { get => gameObject.activeInHierarchy; }

    void OnEnable ( )
    {
        InputManager.Instance.IsIgnore = true;
        InputManager.Instance.EventSystem.SetSelectedGameObject ( gameObject );
        roamingTime = 0f;
    }

    void OnDisable ( )
    {
        InputManager.Instance.IsIgnore = false;
    }

    void LateUpdate ( )
    {
        roamingTime += Time.deltaTime;
        if(roamingTime > Packet_Error_Time)
        {
            //gameObject.SetActive ( false );
            // [TODO] 패킷 에러 보내기
        }
    }
}