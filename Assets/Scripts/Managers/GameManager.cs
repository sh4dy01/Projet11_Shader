using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Outline Settings")]
    [SerializeField] private Material _outlineMaterial;
    [ColorUsage(true, true)]
    [SerializeField] private Color _itemOutlineColor;
    [ColorUsage(true, true)]
    [SerializeField] private Color _enemyOutlineColor;
    [ColorUsage(true, true)]
    [SerializeField] private Color _playerOutlineColor;
    
    private Material _enemyOutlineMaterial;
    private Material _itemOutlineMaterial;
    private Material _playerOutlineMaterial;
    
    public Material OutlineMaterial => _outlineMaterial;
    public Material EnemyOutlineMaterial => _enemyOutlineMaterial;
    public Material ItemOutlineMaterial => _itemOutlineMaterial;
    public Material PlayerOutlineMaterial => _playerOutlineMaterial;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _enemyOutlineMaterial = new Material(_outlineMaterial)
        {
            color = _enemyOutlineColor
        };

        _itemOutlineMaterial = new Material(_outlineMaterial)
        {
            color = _itemOutlineColor
        };
        
        _playerOutlineMaterial = new Material(_outlineMaterial)
        {
            color = _playerOutlineColor
        };
    }
}
