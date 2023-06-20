using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Items/Tool")]
public class Tool : ItemData
{
    [SerializeField] private ToolTypes _toolType;
    [SerializeField] private int _level;
    public enum ToolTypes { Axe, Pickaxe }
    public ToolTypes ToolType => _toolType;
    public int Level => _level;

    public override bool Action(Camera cam)
    {
        Debug.Log("Action2");

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        Debug.Log("2");
        if (hit.collider == null) { return false; }

        if (hit.collider.gameObject.TryGetComponent(out MinableResource resource))
        {
            Debug.Log("Mine");
            resource.Mine(this);
            return true;
        }
        return false;
    }
}
