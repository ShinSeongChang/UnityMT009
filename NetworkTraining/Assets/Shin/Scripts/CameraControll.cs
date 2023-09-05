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
            // ���콺 �Է°� �ޱ�

            // ���콺�� ���� �� ��� == ī�޶��� ������ �ϰ����� �̾��� => ���� �ι����ؼ� �������ش�.
            Vector2 mouseMove = new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")) * 500f * Time.deltaTime;
            Vector3 camera = transform.rotation.eulerAngles;

            mouseMove.x = Mathf.Clamp(mouseMove.x, -90f, 90f);


            transform.rotation = Quaternion.Euler(camera.x + mouseMove.x, camera.y + mouseMove.y, camera.z);

            Debug.DrawRay(transform.position, transform.forward * 30f, Color.yellow);
            // ī�޶� ���� ȸ�� ( ī�޶� Y ������Ʈ �� )
            
        }

        transform.position = transform.parent.GetChild(0).transform.position;
    }
}
