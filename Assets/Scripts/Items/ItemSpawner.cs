using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;
    [SerializeField] private int _amount;
    enum SpawnerType { Single, Repeatable, Constant }
    [SerializeField] private SpawnerType _type;
    [SerializeField] private float _spawnTimeout = 5;
    [SerializeField] private bool _spawnOnStart = false;
    private float _lastSpawnTime;

    void Start()
    {
        if (_spawnOnStart)
        {
            if(_type == SpawnerType.Constant)
            {
                _itemData.Spawn(transform.position, _amount, 0, 0, 0);
            }
            else
            {
                _itemData.Spawn(transform.position, _amount);
            }
        }
        gameObject.name = _itemData.name + " Spawner";
        _lastSpawnTime = Time.time;
    }

    private void Update()
    {
        if(Time.time > _lastSpawnTime + _spawnTimeout)
        {
            if(_type == SpawnerType.Constant)
            {
                bool isOccupied = false;
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
                foreach (var hitCollider in hitColliders)
                {
                    if(hitCollider.gameObject.TryGetComponent(out GroundItem item))
                    {
                        if(item.Item.ItemData == _itemData)
                        {
                            isOccupied = true;
                        }
                    }
                }
                if (!isOccupied)
                {
                    _itemData.Spawn(transform.position, _amount, 0, 0, 0);
                }
            }
            else
            {
                _itemData.Spawn(transform.position, _amount);
            }
            _lastSpawnTime = Time.time;
        }
        if (_type == SpawnerType.Single)
        {
            Destroy(gameObject);
        }
    }
}
