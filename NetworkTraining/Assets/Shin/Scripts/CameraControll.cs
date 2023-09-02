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
            // ���콺 �Է°� �ޱ�

            // ���콺�� ���� �� ��� == ī�޶��� ������ �ϰ����� �̾��� => ���� �ι����ؼ� �������ش�.
            float mouseV = -Input.GetAxis("Mouse Y") * 500f * Time.deltaTime;
            float mouseH = Input.GetAxis("Mouse X") * 500f * Time.deltaTime;

            mouseV = Mathf.Clamp(mouseV, -90f, 90f);

            // ī�޶� ���� ȸ�� ( ī�޶� Y ������Ʈ �� )
            transform.RotateAround(transform.parent.position, Vector3.up, mouseH);

            //ī�޶� ���� ȸ�� ( ī�޶� X ������Ʈ �� )
            transform.RotateAround(transform.parent.position, Vector3.right, mouseV);

            // ���������� ĳ���͸� �ٶ󺻴�.
            transform.LookAt(new Vector3(transform.parent.position.x -0.5f, transform.parent.position.y + 1f, transform.parent.position.z));
        }
    }
}
