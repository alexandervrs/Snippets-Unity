
/**
 * Model.cs
 * 3D Model related snippets for Unity
 */

/* using */
using UnityEngine;
using System.Collections;


/* -----------------------------------------
   Mesh Operations
----------------------------------------- */
// set model visible or hidden
gameObject.GetComponent<MeshRenderer>().enabled = false;


/* -----------------------------------------
   Flip Normals
----------------------------------------- */

/*

    note: This is required in order to display a texture on a model on the inside and not outside
          e.g. a skybox, sphere or cylinder

 */

// requires a MeshFilter component [RequireComponent(typeof(MeshFilter))]
// Start():
MeshFilter filter = GetComponent(typeof (MeshFilter)) as MeshFilter;

if (filter != null) {

    Mesh mesh = filter.mesh;

    Vector3[] normals = mesh.normals;

    for (int i = 0; i < normals.Length; i++) {
        normals[i] = -normals[i];
    }

    mesh.normals = normals;

    for (int m = 0; m < mesh.subMeshCount; m++) {

        int[] triangles = mesh.GetTriangles(m);

        for (int i=0; i < triangles.Length; i += 3) {
            int temp = triangles[i + 0];
            triangles[i + 0] = triangles[i + 1];
            triangles[i + 1] = temp;
        }

        mesh.SetTriangles(triangles, m);
    }

}

