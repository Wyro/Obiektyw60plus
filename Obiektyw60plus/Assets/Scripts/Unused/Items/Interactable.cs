
using UnityEngine;

public class Interactable : MonoBehaviour {

    public float radius = 3f; //how close player can get to interact
    public Transform interactionTransform;

    bool isFocus = false;
    bool hasInteracted = false;
    Transform player;
    
    public virtual void Interact()
    {

    }

    /// <summary>
    /// Every frame checks the distance between player and interaction object
    /// If it is below certain radius, calls the method for interaction
    /// </summary>
    void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if(distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused( Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        hasInteracted = false;
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
