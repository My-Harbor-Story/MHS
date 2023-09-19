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

    public float speed = 10.0f;  // �ڵ����� ���� �ӵ�
    public float turnSpeed = 3.0f;  // �ڵ� ���� �ӵ�

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
            // �ڵ����� ����
            transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);

            // �ڵ��� ���� Ȯ��
            //Debug_Handle();
            checkHandle();
        }

    }

    void Handle_Left()
    {
        //ȸ��
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
            //�ڵ��� ���� ���� + - 35 / 35 ���ָ� �Ѿ��rotate
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
