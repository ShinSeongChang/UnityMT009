using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(transform.parent.name);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            // 마우스 입력값 받기

            // 마우스의 수직 값 상승 == 카메라의 수직축 하강으로 이어짐 => 값을 인버스해서 대입해준다.
            float mouseV = -Input.GetAxis("Mouse Y") * 500f * Time.deltaTime;
            float mouseH = Input.GetAxis("Mouse X") * 500f * Time.deltaTime;

            mouseV = Mathf.Clamp(mouseV, -90f, 90f);

            // 카메라 수평 회전 ( 카메라 Y 로테이트 값 )
            transform.RotateAround(transform.parent.position, Vector3.up, mouseH);

            //카메라 수직 회전 ( 카메라 X 로테이트 값 )
            transform.RotateAround(transform.parent.position, Vector3.right, mouseV);

            // 최종적으로 캐릭터를 바라본다.
            transform.LookAt(new Vector3(transform.parent.position.x -0.5f, transform.parent.position.y + 1f, transform.parent.position.z));
        }
    }
}
