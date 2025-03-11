using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Transform playerCameraPosition; // Normal gameplay camera
    [SerializeField] private Transform conversationCameraPosition; // Camera for conversation
    [SerializeField] private Transform cameraTransform; // Reference to the actual camera

    private bool isInConversation = false; // Tracks if the player is in conversation mode

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F) && !isInConversation)
            {
                StartConversation();
            }
            else if (Input.GetKeyDown(KeyCode.G) && isInConversation)
            {
                EndConversation();
            }
        }
    }

    void StartConversation()
    {
        isInConversation = true;
        Cursor.lockState = CursorLockMode.None;
        ConversationManager.Instance.StartConversation(myConversation);

        // Disable player movement
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Move camera to conversation position
        cameraTransform.SetParent(conversationCameraPosition);
        cameraTransform.position = conversationCameraPosition.position;
        cameraTransform.rotation = conversationCameraPosition.rotation;
    }

    void EndConversation()
    {
        isInConversation = false;
        Cursor.lockState = CursorLockMode.Locked;
        ConversationManager.Instance.EndConversation(); // Optional, if you want to manually stop it

        // Enable player movement
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        // Move camera back to player view
        cameraTransform.SetParent(playerCameraPosition);
        cameraTransform.position = playerCameraPosition.position;
        cameraTransform.rotation = playerCameraPosition.rotation;
    }
}
