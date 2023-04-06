using System;
using UnityEngine;
using UnityEngine.AI;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		private Camera _mainCamera;
		private NavMeshAgent _navMeshAgent;
		[Range(1f, 50f)]
		[SerializeField] private float _rayMaxDistance = 20f;
		[SerializeField] LayerMask _groundLayer;
		
		public NavMeshAgent NavMeshAgent => _navMeshAgent;
		
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		private void Awake()
		{
			_mainCamera = Camera.main;    
			_navMeshAgent = GetComponent<NavMeshAgent>();
		}
		
#if ENABLE_INPUT_SYSTEM

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
#endif

		
		private void Update()
		{
			if (!Input.GetMouseButtonDown(0))
				return;
        
			Ray cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(cameraRay, out var hitInfo, _rayMaxDistance, _groundLayer.value))
			{
				var newMoveDirection = hitInfo.point;
				_navMeshAgent.SetDestination(newMoveDirection);
			}
		}
		
		public bool IsPathComplete()
		{
			if (Vector3.Distance( _navMeshAgent.destination, transform.position) <= _navMeshAgent.stoppingDistance)
			{
				if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
				{
					return true;
				}
			}
 
			return false;
		}

		public void LookInput(Vector2 newLookDirection)
		{
			//look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			//jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}