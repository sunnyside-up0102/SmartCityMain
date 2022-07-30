using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollower : MonoBehaviour
{
    public GameObject player; // �ٶ� �÷��̾� ������Ʈ�Դϴ�. 
    public float xmove = 2; // X�� ���� �̵��� 
    public float ymove = 25; // Y�� ���� �̵��� 
    public float distance = 1;
    private Vector3 velocity = Vector3.zero;
    private int toggleView = 3; // 1=1��Ī, 3=3��Ī 
    private float wheelspeed = 10.0f;
    private Vector3 Player_Height;
    private Vector3 Player_Side;
    void Start()
    {
        Player_Height = new Vector3(0, 3f, 0f);
        Player_Side = new Vector3(-0.8f, 0f, 0f);
    }

    // Update is called once per frame 
    void Update()
    {
        // ���콺 ��Ŭ�� �߿��� ī�޶� ���� ���� 
        if (Input.GetMouseButton(1))
        {
            xmove += Input.GetAxis("Mouse X");
            // ���콺�� �¿� �̵����� xmove �� �����մϴ�. 
            ymove -= Input.GetAxis("Mouse Y");
            // ���콺�� ���� �̵����� ymove �� �����մϴ�. 
        }
        transform.rotation = Quaternion.Euler(ymove, xmove, 0); // �̵����� ���� ī�޶��� �ٶ󺸴� ������ �����մϴ�. 
        if (Input.GetMouseButtonDown(2)) toggleView = 4 - toggleView;
        if (toggleView == 3)
        {
            distance -= Input.GetAxis("Mouse ScrollWheel") * wheelspeed;
            if (distance < 1f) distance = 1f;
            if (distance > 100.0f) distance = 100.0f;
        }
        if (toggleView == 1)
        {
            Vector3 Eye = player.transform.position + Player_Height;
            Vector3 reverseDistance = new Vector3(0.0f, 0.0f, 1f);
            // ī�޶� �ٶ󺸴� �չ����� Z ���Դϴ�. �̵����� ���� Z ������� ���͸� ���մϴ�. 
            transform.position = Eye + transform.rotation * reverseDistance;
            // �÷��̾��� ��ġ���� ī�޶� �ٶ󺸴� ���⿡ ���Ͱ��� ������ ��� ��ǥ�� �����մϴ�. 
        }
        else if (toggleView == 3)
        {
            Vector3 Eye = player.transform.position
                + transform.rotation * Player_Side + Player_Height;
            Vector3 reverseDistance = new Vector3(0.0f, 0.5f, distance);
            // ī�޶� �ٶ󺸴� �չ����� Z ���Դϴ�. �̵����� ���� Z ������� ���͸� ���մϴ�. 
            transform.position = Eye - transform.rotation * reverseDistance;
        }
    }
}