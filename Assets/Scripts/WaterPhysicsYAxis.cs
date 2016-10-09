using UnityEngine;
using System.Collections;

public class WaterPhysicsYAxis : MonoBehaviour {
    float[] xpositions;
    float[] ypositions;
    float[] velocities;
    float[] accelerations;
    LineRenderer Body;

    GameObject[] meshobjects;
    Mesh[] meshes;

    GameObject[] colliders;


    public GameObject splash;

    public Material mat;
    public GameObject watermesh;



    const float springconstant = 0.03f;
    const float damping = 0.04f;
    const float spread = 0.05f;
    const float z = -1f;
    // Use this for initialization
    float basewidth;
    float bottom;
    float right;
    void Start()
    {

        //SpawnWater(-10, 50, 0, 30);
        //Debug.Log(Camera.current.ScreenToWorldPoint(new Vector3(0, Screen.width, 0)));
        //Debug.Log(Camera.current.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)));
        SpawnWater(-10, 30, 0, 20);
    }
    public void Splash(float ypos, float velocity)
    {
        if (ypos >= ypositions[0] && ypos <= ypositions[ypositions.Length - 1])
        {
            ypos -= ypositions[0];
            int index = Mathf.RoundToInt((ypositions.Length - 1) * (ypos / (ypositions[ypositions.Length - 1] - ypositions[0])));
            velocities[index] += velocity;


        }
    }
    // Update is called once per frame

    public void Update()
    {

    }

    public void SpawnWater(float bot, float height, float Left, float Right)
    {
        int edgecount = Mathf.RoundToInt(height) * 5;
        int nodecount = edgecount + 1;

        //Body = gameObject.AddComponent<LineRenderer>();
        //Body.material = mat;
        //Body.material.renderQueue = 1000;
        //Body.SetVertexCount(nodecount);
        //Body.SetWidth(0.1f, 0.1f);
        
        xpositions = new float[nodecount];
        ypositions = new float[nodecount];
        velocities = new float[nodecount];
        accelerations = new float[nodecount];

        meshobjects = new GameObject[edgecount];
        meshes = new Mesh[edgecount];
        colliders = new GameObject[edgecount];

        basewidth = Left;
        right = Right;
        bottom = bot;

        for (int i = 0; i < nodecount; i++)
        {
            ypositions[i] = bottom + height * i/edgecount;
            xpositions[i] = Left;
            accelerations[i] = 0;
            velocities[i] = 0;
            //Body.SetPosition(i, new Vector3(Left, ypositions[i], z));
        }

        for (int i = 0; i < edgecount; i++)
        {
            meshes[i] = new Mesh();
            Vector3[] Vertices = new Vector3[4];
            Vertices[0] = new Vector3(xpositions[i], ypositions[i], z);
            Vertices[1] = new Vector3(xpositions[i + 1], ypositions[i + 1], z);
            Vertices[2] = new Vector3(Right, ypositions[i], z);
            Vertices[3] = new Vector3(Right, ypositions[i + 1], z);

            Vector2[] UVs = new Vector2[4];
            UVs[0] = new Vector2(0, 1);
            UVs[1] = new Vector2(1, 1);
            UVs[2] = new Vector2(0, 0);
            UVs[3] = new Vector2(1, 0);

            int[] tris = new int[6] { 0, 1, 3, 3, 2, 0 };

            meshes[i].vertices = Vertices;
            meshes[i].uv = UVs;
            meshes[i].triangles = tris;

            meshobjects[i] = Instantiate(watermesh, Vector3.zero, Quaternion.identity) as GameObject;
            meshobjects[i].GetComponent<MeshFilter>().mesh = meshes[i];
            meshobjects[i].transform.parent = transform;

            colliders[i] = new GameObject();
            colliders[i].name = "Trigger";
            colliders[i].AddComponent<BoxCollider2D>();
            colliders[i].transform.parent = transform;
            colliders[i].transform.position = new Vector3(Left + 0.5f, bottom + height * (i + 0.5f) / edgecount, 0);
            colliders[i].transform.localScale = new Vector3(1, height / edgecount, 1);
            colliders[i].GetComponent<BoxCollider2D>().isTrigger = true;
            colliders[i].AddComponent<WaterDetector>();

        }

		//transform.parent.gameObject.transform.position.Set (0, 0, 1);
		GameObject.Find ("Fluid").transform.position.Set(0,0,1);
    }

    void UpdateMeshes()
    {
        for (int i = 0; i < meshes.Length; i++)
        {

            Vector3[] Vertices = new Vector3[4];
            Vertices[0] = new Vector3(xpositions[i], ypositions[i], z);
            Vertices[1] = new Vector3(xpositions[i + 1], ypositions[i + 1], z);
            Vertices[2] = new Vector3(right, ypositions[i], z);
            Vertices[3] = new Vector3(right, ypositions[i + 1], z);

            meshes[i].vertices = Vertices;
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < xpositions.Length; i++)
        {
            float force = springconstant * (xpositions[i] - basewidth) + velocities[i] * damping;
            accelerations[i] = -force;
            xpositions[i] += velocities[i];
            velocities[i] += accelerations[i];
            //Body.SetPosition(i, new Vector3(xpositions[i], ypositions[i], z));
        }

        float[] leftDeltas = new float[ypositions.Length];
        float[] rightDeltas = new float[ypositions.Length];
        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i < ypositions.Length; i++)
            {
                if (i > 0)
                {
                    leftDeltas[i] = spread * (xpositions[i] - xpositions[i - 1]);
                    velocities[i - 1] += leftDeltas[i];
                }
                if (i < ypositions.Length - 1)
                {
                    rightDeltas[i] = spread * (xpositions[i] - xpositions[i + 1]);
                    velocities[i + 1] += rightDeltas[i];
                }
            }

            //Now we apply a difference in position
            for (int i = 0; i < ypositions.Length; i++)
            {
                if (i > 0)
                    xpositions[i - 1] += leftDeltas[i];
                if (i < xpositions.Length - 1)
                    xpositions[i + 1] += rightDeltas[i];
            }
        }

        UpdateMeshes();
    }

}
