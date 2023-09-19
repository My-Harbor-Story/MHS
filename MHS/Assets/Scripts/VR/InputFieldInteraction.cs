using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using OculusSampleFramework;

public class InputFieldInteraction : MonoBehaviour
{
    public OVRCameraRig cameraRig; // OVRCameraRig 오브젝트
    public Transform leftControllerAnchor; // 왼쪽 컨트롤러 앵커 (Oculus Touch 왼손)
    public Transform rightControllerAnchor; // 오른쪽 컨트롤러 앵커 (Oculus Touch 오른손)

    private InputField currentInputField; // 현재 선택된 InputField

    private void Update()
    {
        // Oculus 컨트롤러의 트리거 버튼을 사용하여 InputField를 클릭할 수 있도록 합니다.
        if (OVRInput.GetDown(OVRInput.Button.Any) && currentInputField != null)
        {
            Debug.LogWarning("GETDOWNNNNNNNNNNNN");
            ActivateKeyboard(currentInputField);
        }
    }

    // InputField가 선택될 때 호출되는 메서드
    public void OnInputFieldSelected(InputField inputField)
    {
        currentInputField = inputField;
    }

    // InputField에서 포커스가 벗어날 때 호출되는 메서드
    public void OnInputFieldDeselected(InputField inputField)
    {
        currentInputField = null;
    }

    // 가상 키보드 활성화 메서드
    private void ActivateKeyboard(InputField inputField)
    {
        // 현재 선택된 InputField가 있을 때만 가상 키보드를 활성화합니다.
        if (inputField != null)
        {
            Debug.Log("키보드 활성화");
            // 가상 키보드 활성화
            EventSystem.current.SetSelectedGameObject(null); // 선택된 오브젝트 초기화
            inputField.ActivateInputField();

            // 컨트롤러 앵커를 기준으로 키보드 위치를 조절할 수 있습니다.
            //Transform controllerAnchor = OVRInput.GetActiveController() == OVRInput.Controller.LTouch ?
            //    leftControllerAnchor : rightControllerAnchor;
            //cameraRig.transform.position = controllerAnchor.position;
            //cameraRig.transform.rotation = controllerAnchor.rotation;
        }
    }
}