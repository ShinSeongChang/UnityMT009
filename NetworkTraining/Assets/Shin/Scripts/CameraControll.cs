using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
            Vector2 mouseMove = new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")) * 500f * Time.deltaTime;
            Vector3 camera = transform.rotation.eulerAngles;

            mouseMove.x = Mathf.Clamp(mouseMove.x, -90f, 90f);


            transform.rotation = Quaternion.Euler(camera.x + mouseMove.x, camera.y + mouseMove.y, camera.z);

            Debug.DrawRay(transform.position, transform.forward * 30f, Color.yellow);
            // 카메라 수평 회전 ( 카메라 Y 로테이트 값 )
            
        }

        transform.position = transform.parent.GetChild(0).transform.position;
    }
}
