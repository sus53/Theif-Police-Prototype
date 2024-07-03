using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SkinnedMeshtoMesh : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public VisualEffect vfxGraph;
    public float refreshRate = 0.02f;
    void Start()
    {
        StartCoroutine(UpdateVFXGraph());
    }

    IEnumerator UpdateVFXGraph()
    {
        while (gameObject.activeSelf)
        {
            Mesh mesh = new Mesh();
            skinnedMeshRenderer.BakeMesh(mesh);

            Vector3[] vertices = mesh.vertices;
            Mesh mesh2 = new Mesh();
            mesh2.vertices = vertices;

            vfxGraph.SetMesh("mesh", mesh2);
            yield return new WaitForSeconds(refreshRate);
        }
    }

}
