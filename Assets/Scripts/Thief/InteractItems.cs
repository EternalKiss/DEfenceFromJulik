using UnityEngine;
using TMPro;

public class InteractItems : MonoBehaviour
{
    [SerializeField] private Player _mainPlayer;
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
        Ray ray = _mainPlayer.MainCamera.ViewportPointToRay(Vector3.one / _diviionForCenterOfScreen);
        RaycastHit hit;

        bool isHitSomething = false;

        if(Physics.Raycast(ray, out hit, _interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if(interactable != null )
            {
                isHitSomething = true;
                _interactionText.text = interactable.GetDescription();

                if(Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }    
        }

        _interactionUI.SetActive(isHitSomething);
    }
}
