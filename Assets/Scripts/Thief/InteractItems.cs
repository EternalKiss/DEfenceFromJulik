using UnityEngine;
using TMPro;

public class InteractItems : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _interactionUI;
    [SerializeField] private TextMeshProUGUI _interactionText;
    [SerializeField] private float _interactionDistance;

    private float _diviionForCenterOfScreen = 2f;

    private void Update()
    {
        InteractionRay();
    }

    private void InteractionRay()
    {
        Ray ray = _mainCamera.ViewportPointToRay(Vector3.one / _diviionForCenterOfScreen);
        RaycastHit hit;

        bool isHitSomething = false;

        if(Physics.Raycast(ray, out hit, _interactionDistance))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                isHitSomething = true;
                _interactionText.text = interactable.GetDescription();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
        }

        _interactionUI.SetActive(isHitSomething);
    }
}
