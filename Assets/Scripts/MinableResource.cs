using UnityEngine;

public class MinableResource : MonoBehaviour
{
    [SerializeField] private ItemData _resource;
    [SerializeField] private Tool.ToolTypes _toolType;
    [SerializeField] private int _resourceAmount;
    [SerializeField] private int _level;
    [SerializeField] private int _health;

    public void Mine(Tool tool)
    {
        if (tool == null)
        {
            return;
        }
        if (tool.ToolType == _toolType)
        {
            if (tool.Level >= _level)
            {
                Vector3 randomDirection = Random.insideUnitCircle.normalized;
                int damage = Mathf.Abs(tool.Level - _level + 1);
                if (_health - damage > 0)
                {
                    _health -= damage;
                }
                else
                {
                    _resource.Spawn(transform.position, _resourceAmount, 0f, 0.2f ,0.6f);
                    Destroy(gameObject);
                }
            }
            else
            {
                Debug.Log("Tool level is too low");
            }
        }
        else
        {
            Debug.Log("Wrong tool");
        }
    }
}
