using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 initPos;
    private Vector3 offset; // 플레이어와 카메라의 위치 차이

    private float zoomSpeed;

    private void Awake()
    {
        initPos = transform.position;
        offset = player.position - initPos;
    }

    private void Update()
    {
        Zoom();
    }

    private void LateUpdate()
    {
        gameObject.transform.position = player.position - offset;
    }

    private void Zoom()
    {
        if (Input.touchCount == 2) //두 손가락 터치 줌인
        {
            zoomSpeed = 0.08f;

            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * zoomSpeed;
            GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView, 0.1f, 179.9f);

            if (GetComponent<Camera>().fieldOfView < 20) //줌인 
            {
                GetComponent<Camera>().fieldOfView = 20;
            }
            else if (GetComponent<Camera>().fieldOfView > 90) //줌아웃
            {
                GetComponent<Camera>().fieldOfView = 90;
            }
        }

        else
        {
            zoomSpeed = 50;

            float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * -1;
            GetComponent<Camera>().fieldOfView += scroll;

            if (GetComponent<Camera>().fieldOfView < 20) //줌인 
            {
                GetComponent<Camera>().fieldOfView = 20;
            }
            else if (GetComponent<Camera>().fieldOfView > 90) //줌아웃
            {
                GetComponent<Camera>().fieldOfView = 90;
            }
        }        
    }
}
