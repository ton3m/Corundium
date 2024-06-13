using UnityEngine;

public class TipsShower : MonoBehaviour
{
    [SerializeField] private GameObject _canvasTips;
    private bool isShowed;
    
    public void ShowTip(Transform interactObject, Transform playerCameraTransform)
    {
        RotateTip(playerCameraTransform);
        if (!isShowed)
        {
            isShowed = true;
            transform.position = interactObject.position;
            _canvasTips.SetActive(true);
        }
        
    }
    public void CloseTip()
    {
        if (isShowed)
        {
            isShowed = false;
            _canvasTips.SetActive(false);
        }
    }
    private void RotateTip(Transform playerCamerTransform)
    {
            Vector3 directionToTarget = playerCamerTransform.position - _canvasTips.transform.position;
            //directionToTarget.y = 0;
    
            if (directionToTarget != Vector3.zero)
            {
                _canvasTips.transform.rotation = Quaternion.LookRotation(directionToTarget);
    
                _canvasTips.transform.Rotate(0, 180 , 0);
            }
        
    }
}
