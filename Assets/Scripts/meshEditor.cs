using UnityEngine;
using System.Collections;

public class meshEditor : MonoBehaviour {

	Vector3 []vertices;
	Vector3 []normals;

	// Use this for initialization
	void Start () {
		Mesh mesh = GetComponent<MeshFilter> ().mesh;

		if (vertices == null)
			vertices = mesh.vertices;

		if (normals == null)
			normals = mesh.normals;

		for (int i = 0; i < vertices.Length; i++)
			//vertices [i] = vertices [i + 3];
			print("Vertex: "+vertices[i].x+","+vertices[i].y+","+vertices[i].z);

		vertices [2] = vertices [3];

		vertices [0] = new Vector3 (0, -0.5f);

		mesh.vertices = vertices;
	}
	
	// Update is called once per frame
	void Update () {
	

	}
}
