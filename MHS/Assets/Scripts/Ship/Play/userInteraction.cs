using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userInteraction : MonoBehaviour
{
    [SerializeField] private Transform handle;

    [SerializeField] private OneGrabRotateTransformer oneHand;
    [SerializeField] private TwoGrabRotateTransformer twoHand;
    [SerializeField] private Grabbable grabbable;

    [SerializeField] private Transform navCamera; // 네비게이션을 보여줄 카메라

    public float speed = 10.0f;  // 자동차의 주행 속도
    public float turnSpeed = 30.0f;  // 핸들 꺾는 속도

    bool isHandle = false;
    public static bool driveAble = true;

    // Start is called before the first frame update
    void Start()
    {
        // 핸들 회전된거 초기화
        handle.localRotation = Quaternion.Euler(0.0f, 90f, -90.0f);

        // 선박 초기 위치 초기화
        transform.position = new Vector3(100.0f, 1.66f, -124.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (driveAble)
        {
            // 선박 직진
            transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);

            // 핸들의 방향 확인
            //Debug_Handle();
            checkHandle();

        }

        navCamera.transform.position = new Vector3(
            transform.position.x,
            transform.position.y + 12,
            transform.position.z
        );
    }

    void Handle_Left()
    {
        //회전
        transform.Rotate(0, Time.deltaTime * turnSpeed * -1, 0);

    }

    void Handle_Right()
    {
        transform.Rotate(0, Time.deltaTime * turnSpeed, 0);
    }


    // 핸들의 회전 확인 함수
    void checkHandle()
    {
        // 핸들을 잡고 있고
        if (isHandle)
        {
            Vector3 vector3 = handle.localRotation.eulerAngles;
            // 왼쪽 오른쪽 판단하여 rotate
            if (180 <= vector3.x)
            {
                Debug.Log("Turn Left");
                Handle_Left();
            }
            else
            {
                Debug.Log("Turn Right");
                Handle_Right();
            }
        }

        else
        {
            // 놓으면 초기화
            handle.localRotation = Quaternion.Euler(0.0f, 90.0f, -90.0f);
        }
    }

    // 핸들 잡거나 놨을 때 판단 (잡으면 true, 놓으면 false)
    public void setIsHandle(bool isHandle)
    {
        Debug.Log("Handle");
        this.isHandle = isHandle;
    }

    // VR 없이 기능 확인할 때 사용한 함수 (최종 기능엔 사용 X)
    void Debug_Handle()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            handle.localRotation = Quaternion.Euler(-90.0f, 90f, -90.0f);
            Handle_Left();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            handle.localRotation = Quaternion.Euler(90.0f, 90f, -90.0f);
            Handle_Right();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Destination"))
        {
            driveAble = false;
        }
    }
}
