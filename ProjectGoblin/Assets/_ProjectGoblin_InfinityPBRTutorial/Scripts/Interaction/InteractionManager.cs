using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adapted from Unity's Open Project #1 "Chop Chop"
// https://github.com/UnityTechnologies/open-project-1

public enum InteractionType { None = 0, Examine }

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;
    [SerializeField] StringValue _examineObjectText;

    [Header("Broadcasting on")]
    [SerializeField] private GameEventChannel switchExamineDialogueChannel;

    [HideInInspector] public InteractionType currentInteraction;
    private InteractionType _potentialInteraction;

    // We'll use this to turn off the interaction icon
    private bool _isInteracting = false;

    // Storing the object we are currently interacting with
    private GameObject _currentInteractableObject = null;

    // Exactly which object channel to broadcast on
    private GameEventChannel _interactableObjectEventChannelToBroadcastOn;

    private void OnEnable()
    {
        _inputReader.interactEvent += OnInteractionButtonPress;
    }

    private void OnDisable()
    {
        _inputReader.interactEvent -= OnInteractionButtonPress;
    }

    private void OnInteractionButtonPress()
    {
        //_inputReader.EnableGameplayInput();

        switch (_potentialInteraction)
        {
            case InteractionType.None:
                return;

            case InteractionType.Examine:
                if (_currentInteractableObject != null && _isInteracting == false)
                {
                    _isInteracting = true;
                    _interactableObjectEventChannelToBroadcastOn.RaiseEvent();
                    switchExamineDialogueChannel.RaiseEvent();
                    Debug.Log("I'm examining " + _currentInteractableObject.name);
                    currentInteraction = InteractionType.Examine;
                }
                else if (_currentInteractableObject != null && _isInteracting == true)
                {
                    _isInteracting = false;
                    _interactableObjectEventChannelToBroadcastOn.RaiseEvent();
                    switchExamineDialogueChannel.RaiseEvent();
                    currentInteraction = InteractionType.Examine;
                }
                break;

            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _isInteracting = false;
        if (other.CompareTag("ExaminableObject"))
        {
            GetObjectInfo(other);
            _interactableObjectEventChannelToBroadcastOn.RaiseEvent();
            _potentialInteraction = InteractionType.Examine;
        }
    }

    private void GetObjectInfo(Collider other)
    {
        _currentInteractableObject = other.gameObject;
        ExaminableObjectInfo objectInfo = _currentInteractableObject.GetComponent<ExaminableObjectInfo>();
        _examineObjectText.value = objectInfo.objectTextInfo.value;
        GameEventListener objectListener = objectInfo.GetComponent<GameEventListener>();
        _interactableObjectEventChannelToBroadcastOn = objectListener.eventChannelToListenTo;
    }

    private void OnTriggerExit(Collider other)
    {
        ResetInteraction();
    }

    private void ResetInteraction()
    {
        if (_isInteracting == true)
        {
            switchExamineDialogueChannel.RaiseEvent();
            _isInteracting = false;
        }
        else if (_isInteracting == false)
        {
            if (_interactableObjectEventChannelToBroadcastOn != null)
            _interactableObjectEventChannelToBroadcastOn.RaiseEvent();
        }
        _interactableObjectEventChannelToBroadcastOn = null;
        _currentInteractableObject = null;
        _potentialInteraction = InteractionType.None;
    }
}