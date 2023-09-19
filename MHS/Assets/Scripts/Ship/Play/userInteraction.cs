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

    [SerializeField] private Transform navCamera; // �׺���̼��� ������ ī�޶�

    public float speed = 10.0f;  // �ڵ����� ���� �ӵ�
    public float turnSpeed = 30.0f;  // �ڵ� ���� �ӵ�

    bool isHandle = false;
    public static bool driveAble = true;

    // Start is called before the first frame update
    void Start()
    {
        // �ڵ� ȸ���Ȱ� �ʱ�ȭ
        handle.localRotation = Quaternion.Euler(0.0f, 90f, -90.0f);

        // ���� �ʱ� ��ġ �ʱ�ȭ
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
            // ���� ����
            transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);

            // �ڵ��� ���� Ȯ��
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
        //ȸ��
        transform.Rotate(0, Time.deltaTime * turnSpeed * -1, 0);

    }

    void Handle_Right()
    {
        transform.Rotate(0, Time.deltaTime * turnSpeed, 0);
    }


    // �ڵ��� ȸ�� Ȯ�� �Լ�
    void checkHandle()
    {
        // �ڵ��� ��� �ְ�
        if (isHandle)
        {
            Vector3 vector3 = handle.localRotation.eulerAngles;
            // ���� ������ �Ǵ��Ͽ� rotate
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
            // ������ �ʱ�ȭ
            handle.localRotation = Quaternion.Euler(0.0f, 90.0f, -90.0f);
        }
    }

    // �ڵ� ��ų� ���� �� �Ǵ� (������ true, ������ false)
    public void setIsHandle(bool isHandle)
    {
        Debug.Log("Handle");
        this.isHandle = isHandle;
    }

    // VR ���� ��� Ȯ���� �� ����� �Լ� (���� ��ɿ� ��� X)
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
