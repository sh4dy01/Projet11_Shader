using UnityEngine;
using UnityEngine.AI;

namespace StarterAssets
{
	public class PlayerInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Interaction Settings")]
		[SerializeField] private float _rayMaxDistance = 20f;
		[SerializeField] LayerMask _clickableLayerMask;
		[SerializeField] Material _outlineMaterial;
		
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		
		private Camera _mainCamera;
		private NavMeshAgent _navMeshAgent;
		private PlayerAttacks _playerAttacks;
		
		public NavMeshAgent NavMeshAgent => _navMeshAgent;

		private void Awake()
		{
			_mainCamera = Camera.main;    
			_navMeshAgent = GetComponent<NavMeshAgent>();
			_playerAttacks = GetComponent<PlayerAttacks>();
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				LeftClickInput();
			}
			else if (Input.GetMouseButtonDown(1))
			{
				RightClickInput();
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
				_playerAttacks.ResetAttack();
				
				GameObject hitObject = hitInfo.collider.gameObject;
				int layerId = hitObject.layer;
				
				if (layerId == LayerMask.NameToLayer("Ground"))
				{
					_navMeshAgent.SetDestination(hitInfo.point);
				}
				else if (layerId == LayerMask.NameToLayer("Collectibles"))
				{
					if (hitObject.TryGetComponent(out CollectibleItem item))
					{
						item.IsCollected();

						_navMeshAgent.SetDestination(hitInfo.point);
					}
				}
				else if (layerId == LayerMask.NameToLayer("Enemies"))
				{
					Debug.Log("Enemy");
					if (hitObject.TryGetComponent(out DamageableEnemy enemy))
					{
						_playerAttacks.SetAttackTarget(enemy);
					}
				}
			}
		}

		private void RightClickInput()
		{
			
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