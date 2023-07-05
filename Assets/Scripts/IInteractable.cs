using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IInteractable
{
    public enum InteractableType { Default, Storrage, Resource }
    public InteractableType Type { get; }
    public void Interact(Interactor interactor, out bool interactionSuccesful);
    public void EndInteraction();
}
