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
    bool driveAble = true;

    // Start is called before the first frame update
    void Start()
    {

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
            Debug_Handle();
            //checkHandle();
        }

    }

    void Handle_Left()
    {
        //ȸ��
        transform.Rotate(0, Time.deltaTime * -30, 0);
        transform.Translate(0, 0, speed * 0.65f * Time.deltaTime);

    }

    void Handle_Right()
    {
        transform.Rotate(0, Time.deltaTime * 30, 0);
        transform.Translate(0, 0, speed * 2 / 5 * Time.deltaTime);
    }


    void checkHandle()
    {
        Vector3 vector3 = handle.localRotation.eulerAngles;
        //�ڵ��� ���� ���� + - 35 / 35 ���ָ� �Ѿ��rotate
        if (180 <= vector3.y)
        {
            Handle_Left();
        }
        else
        {
            Handle_Right();
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
            Handle_Left();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Handle_Right();
        }

    }
}
