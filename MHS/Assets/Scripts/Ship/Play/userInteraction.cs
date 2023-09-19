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

    public float speed = 10.0f;  // 자동차의 주행 속도
    public float turnSpeed = 3.0f;  // 핸들 꺾는 속도

    bool isHandle = false;
    public static bool driveAble = true;

    // Start is called before the first frame update
    void Start()
    {
        handle.localRotation = Quaternion.Euler(0.0f, 90f, -90.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (driveAble)
        {
            // 자동차의 전진
            transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);

            // 핸들의 방향 확인
            //Debug_Handle();
            checkHandle();
        }

    }

    void Handle_Left()
    {
        //회전
        transform.Rotate(0, Time.deltaTime * -30, 0);

    }

    void Handle_Right()
    {
        transform.Rotate(0, Time.deltaTime * 30, 0);
    }


    void checkHandle()
    {
        if (isHandle)
        {
            Vector3 vector3 = handle.localRotation.eulerAngles;
            //핸들을 잡은 상태 + - 35 / 35 범주를 넘어서면rotate
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
        
    }

    public void setIsHandle(bool isHandle)
    {
        Debug.Log("Handle");
        this.isHandle = isHandle;
    }

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
}
