using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IInteractable
{
    public UnityAction<IInteractable> OnIteractionComplete { get; set; }

    public void Interact(Interactor interactor, out bool interactionSuccesful);

    public void EndInteraction();
}
