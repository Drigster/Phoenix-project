using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private LayerMask _interactionLayer;
    [SerializeField] private float _interactionRadius = 1f;
    private List<IInteractable> _interactablesInRadius;
    private GameObject _closestInteractable;
    private IInteractable _currentInteractable;

    [SerializeField] private Material _outlineMaterial;
    private Material _originalMaterial;

    private PlayerInput _playerInput;

    public bool IsInteracting { get; private set; }

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _interactablesInRadius = new List<IInteractable>();
    }

    private void OnEnable()
    {
        _playerInput.Player.Interact.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Player.Interact.Disable();
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_interactionPoint.position, _interactionRadius, _interactionLayer);

        _interactablesInRadius.Clear();
        GameObject oldClosestInteractable = _closestInteractable;
        _closestInteractable = null;

        if (colliders.Length > 0)
        {
            float closestDistance = _interactionRadius + 1;

            for (int i = 0; i < colliders.Length; i++)
            {
                var interactable = colliders[i].GetComponent<IInteractable>();

                if (interactable != null)
                {
                    _interactablesInRadius.Add(interactable);

                    float currentDistance = Vector2.Distance(_interactionPoint.position, colliders[i].gameObject.transform.position);
                    if (currentDistance < closestDistance)
                    {
                        closestDistance = currentDistance;
                        _closestInteractable = colliders[i].gameObject;
                    }
                }
            }

            if (_playerInput.Player.Interact.WasPerformedThisFrame())
            {
                if (IsInteracting)
                {
                    EndInteraction(_currentInteractable);
                }
                StartInteraction(_closestInteractable.GetComponent<IInteractable>());
            }
        }
        if (_closestInteractable != null)
        {
            if (oldClosestInteractable != null)
            {
                oldClosestInteractable.GetComponent<SpriteRenderer>().material = _originalMaterial;
                _originalMaterial = _closestInteractable.GetComponent<SpriteRenderer>().material;
                _closestInteractable.GetComponent<SpriteRenderer>().material = _outlineMaterial;
            }
            else
            {
                _originalMaterial = _closestInteractable.GetComponent<SpriteRenderer>().material;
                _closestInteractable.GetComponent<SpriteRenderer>().material = _outlineMaterial;
            }
        }
        else if(oldClosestInteractable != null)
        {
            oldClosestInteractable.GetComponent<SpriteRenderer>().material = _originalMaterial;
            _originalMaterial = null;
        }

        if (!_interactablesInRadius.Contains(_currentInteractable) && _currentInteractable != null)
        {
            EndInteraction(_currentInteractable);
        }
    }

    void StartInteraction(IInteractable interactable)
    {
        interactable.Interact(this, out bool interactSuccesful);
        IsInteracting = interactSuccesful;
        _currentInteractable = interactable;
    }

    void EndInteraction(IInteractable interactable)
    {
        interactable.EndInteraction();
        IsInteracting = false;
        _currentInteractable = null;
    }
}
