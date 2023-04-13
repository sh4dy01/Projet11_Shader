using UnityEngine;
using UnityEngine.AI;

namespace StarterAssets
{
	public class PlayerInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public bool sprint;

		[Header("Interaction Settings")]
		[SerializeField] private float _rayMaxDistance = 20f;
		[SerializeField] LayerMask _clickableLayerMask;
		
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		
		private Camera _mainCamera;
		private NavMeshAgent _navMeshAgent;
		private PlayerEntity playerEntity;
		
		public NavMeshAgent NavMeshAgent => _navMeshAgent;

		private void Awake()
		{
			_mainCamera = Camera.main;    
			_navMeshAgent = GetComponent<NavMeshAgent>();
			playerEntity = GetComponent<PlayerEntity>();
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
				playerEntity.ResetAttack();
				
				GameObject hitObject = hitInfo.collider.gameObject;
				int layerId = hitObject.layer;
				
				// MoveTo
				if (layerId == LayerMask.NameToLayer("Ground"))
				{
					MoveTo(hitInfo);
				}
				// Collect
				else if (layerId == LayerMask.NameToLayer("Collectibles"))
				{
					if (hitObject.TryGetComponent(out CollectibleItem item))
					{
						CollectItem(item, hitInfo);
					}
				}
				// Melee attack
				else if (layerId == LayerMask.NameToLayer("Enemies"))
				{
					if (hitObject.TryGetComponent(out DamageableEnemy enemy))
					{
						playerEntity.MeleeAttack(enemy);
					}
				}
			}
		}
		
		private void RightClickInput()
		{
			Ray cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(cameraRay, out var hitInfo, _rayMaxDistance, _clickableLayerMask))
			{
				playerEntity.ResetAttack();
				
				GameObject hitObject = hitInfo.collider.gameObject;
				int layerId = hitObject.layer;
				
				// Distance attack
				if (layerId == LayerMask.NameToLayer("Enemies"))
				{
					if (hitObject.TryGetComponent(out DamageableEnemy enemy))
					{
						playerEntity.RangeAttack(enemy);
					}
				}
			}
		}
		
		private void MoveTo(RaycastHit hitInfo)
		{
			playerEntity.ResetAttack();
			playerEntity.ResetAttackAnimation();
			_navMeshAgent.SetDestination(hitInfo.point);
		}

		private void CollectItem(CollectibleItem item, RaycastHit hitInfo)
		{
			item.IsCollected();

			_navMeshAgent.SetDestination(hitInfo.point);
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