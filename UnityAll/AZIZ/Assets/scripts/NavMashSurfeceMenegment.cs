using UnityEngine;
using NavMeshPlus.Components;
public class NavMashSurfeceMenegment : MonoBehaviour
{
   public static NavMashSurfeceMenegment Instance { get; private set; }

    private NavMeshSurface navMeshSurface;

    private void Awake()
    {
        Instance = this;
        navMeshSurface = GetComponent<NavMeshSurface>();
        navMeshSurface.hideEditorLogs = true;

    }
    public void RebakeNavmeshSurface()
    {
        navMeshSurface.BuildNavMesh();
    }
}
