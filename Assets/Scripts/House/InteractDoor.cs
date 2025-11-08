using UnityEngine;

public class InteractDoor : MonoBehaviour, IInteractable
{
    public Animator m_Animator;
    public bool IsOpen;

    private void Start()
    {
        if (IsOpen)
        {
            m_Animator.SetBool("IsOpen", true);
        }    
    }

    public string GetDescription()
    {
        if(IsOpen)
        {
            return $"Press {KeyCode.E} to <color=red>close</color> the door";
        }

        return $"Press {KeyCode.E} to <color=green>open</color> the door";
    }

    public void Interact()
    {
        IsOpen = !IsOpen;
        
        if(IsOpen )
        {
            m_Animator.SetBool("IsOpen", true);
        }
        else
        {
            m_Animator.SetBool("IsOpen", false);
        }    
    }
}
