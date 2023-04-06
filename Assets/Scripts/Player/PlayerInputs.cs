using System;
using UnityEngine;
using UnityEngine.AI;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class PlayerInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		[Range(1f, 50f)]
		[SerializeField] private float _rayMaxDistance = 20f;
		[SerializeField] LayerMask _clickableLayerMask;
		
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
		
		private Camera _mainCamera;
		private NavMeshAgent _navMeshAgent;
		
		public NavMeshAgent NavMeshAgent => _navMeshAgent;

		private void Awake()
		{
			_mainCamera = Camera.main;    
			_navMeshAgent = GetComponent<NavMeshAgent>();
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				LeftClickInput();
			}
			
			if (Input.GetKeyDown(KeyCode.LeftShift))
			{
				SprintInput(true);
			} 
			else if (Input.GetKeyUp(KeyCode.LeftShift))
			{
				SprintInput(false);
			}
		}

		public void LeftClickInput()
		{
			Ray cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(cameraRay, out var hitInfo, _rayMaxDistance, _clickableLayerMask))
			{
				GameObject hitObject = hitInfo.collider.gameObject;
				int layerId = hitObject.layer;
				
				if (layerId == LayerMask.NameToLayer("Ground"))
				{
					var newMoveDirection = hitInfo.point;
					_navMeshAgent.SetDestination(newMoveDirection);
				}
				else if (layerId == LayerMask.NameToLayer("Collectibles"))
				{
					//Collect item
				}
				else if (layerId == LayerMask.NameToLayer("Enemies"))
				{
					//Attack enemy
				}
			}
		}

		public void SprintInput(bool value)
		{
			sprint = value;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
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
	}
}