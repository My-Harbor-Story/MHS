using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using OculusSampleFramework;

public class InputFieldInteraction : MonoBehaviour
{
    public OVRCameraRig cameraRig; // OVRCameraRig ������Ʈ
    public Transform leftControllerAnchor; // ���� ��Ʈ�ѷ� ��Ŀ (Oculus Touch �޼�)
    public Transform rightControllerAnchor; // ������ ��Ʈ�ѷ� ��Ŀ (Oculus Touch ������)

    private InputField currentInputField; // ���� ���õ� InputField

    private void Update()
    {
        // Oculus ��Ʈ�ѷ��� Ʈ���� ��ư�� ����Ͽ� InputField�� Ŭ���� �� �ֵ��� �մϴ�.
        if (OVRInput.GetDown(OVRInput.Button.Any) && currentInputField != null)
        {
            Debug.LogWarning("GETDOWNNNNNNNNNNNN");
            ActivateKeyboard(currentInputField);
        }
    }

    // InputField�� ���õ� �� ȣ��Ǵ� �޼���
    public void OnInputFieldSelected(InputField inputField)
    {
        currentInputField = inputField;
    }

    // InputField���� ��Ŀ���� ��� �� ȣ��Ǵ� �޼���
    public void OnInputFieldDeselected(InputField inputField)
    {
        currentInputField = null;
    }

    // ���� Ű���� Ȱ��ȭ �޼���
    private void ActivateKeyboard(InputField inputField)
    {
        // ���� ���õ� InputField�� ���� ���� ���� Ű���带 Ȱ��ȭ�մϴ�.
        if (inputField != null)
        {
            Debug.Log("Ű���� Ȱ��ȭ");
            // ���� Ű���� Ȱ��ȭ
            EventSystem.current.SetSelectedGameObject(null); // ���õ� ������Ʈ �ʱ�ȭ
            inputField.ActivateInputField();

            // ��Ʈ�ѷ� ��Ŀ�� �������� Ű���� ��ġ�� ������ �� �ֽ��ϴ�.
            //Transform controllerAnchor = OVRInput.GetActiveController() == OVRInput.Controller.LTouch ?
            //    leftControllerAnchor : rightControllerAnchor;
            //cameraRig.transform.position = controllerAnchor.position;
            //cameraRig.transform.rotation = controllerAnchor.rotation;
        }
    }
}