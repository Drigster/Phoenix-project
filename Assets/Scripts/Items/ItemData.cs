using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ItemData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField][TextArea(4,4)] private string _description;
    [SerializeField] private Sprite _uiIcon;
    [SerializeField] private GroundItem _prefab;
    [SerializeField] private int _maxStack = 1;
    [SerializeField] private ItemTypes _itemType;
    public enum ItemTypes { Resource, Tool, Weapon }
    public string Name => _name;
    public string Description => _description;
    public Sprite UIIcon => _uiIcon;
    public GroundItem Prefab => _prefab;
    public int MaxStack => _maxStack;
    public ItemTypes ItemType => _itemType;

    public void Spawn(Vector3 position, int amount, float spawnMinRange = 1f, float spawnMinForce = 0.2f, float spawnMaxForce = 0.4f)
    {
        int alreadySpawned = 0;
        do
        {
            int amountToSpawn;
            if (amount > _maxStack)
            {
                amountToSpawn = _maxStack;
            }
            else
            {
                amountToSpawn = amount;
            }
            alreadySpawned += amountToSpawn;
            Vector3 randomDirection = Random.insideUnitCircle.normalized;
            GroundItem item = Instantiate(_prefab, position + randomDirection * spawnMinRange, Quaternion.identity);
            item.SetItem(this, amountToSpawn);
            float randomForce = Random.Range(spawnMinForce, spawnMaxForce);
            item.GetComponent<Rigidbody2D>().AddForce(randomDirection * randomForce * 5f, ForceMode2D.Impulse);
        } while (alreadySpawned < amount);
    }

    public virtual bool Action(Camera cam) {
        return false;
    }

    public virtual bool SecondaryAction(Camera cam) {
        return false;
    }
}
