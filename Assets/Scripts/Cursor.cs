using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public float rotateX;
    public float rotateY;
    public float rotateZ;
    public float clickDistance;
    public InputManager inputManager;
    public int startingMeshFilter;
    public List<MeshFilter> meshList = new List<MeshFilter>();
    private MeshRenderer mesh;
    private MeshFilter meshFilter;
    private int currentMeshFilterIndex;
    [Header("Random Color")]
    [Range(0f, 1f)]
    public float minHue;
    [Range(0f, 1f)]
    public float maxHue;
    [Range(0f, 1f)]
    public float minSat;
    [Range(0f, 1f)]
    public float maxSat;
    [Range(0f, 1f)]
    public float minVal;
    [Range(0f, 1f)]
    public float maxVal;

    private bool grabbed;

    private void Awake()
    {
        mesh = GetComponentInChildren<MeshRenderer>();
        meshFilter = GetComponentInChildren<MeshFilter>();
        currentMeshFilterIndex = startingMeshFilter;
        meshFilter.mesh = meshList[currentMeshFilterIndex].sharedMesh;
    }
    public void Update()
    {
        if (grabbed)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(inputManager.mousePos);
            Vector3 finalMousePos = new Vector3(mousePos.x, mousePos.y, 1f);
            transform.position = finalMousePos;
        }
        mesh.material.color = Random.ColorHSV(minHue, maxHue, minSat, maxSat, minVal, maxVal);
        transform.Rotate(rotateX * Time.deltaTime, rotateY * Time.deltaTime, rotateZ * Time.deltaTime);
    }

    public bool Clicked(Vector3 mousePosSent)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mousePosSent);
        Vector3 finalMousePos = new Vector3(mousePos.x, mousePos.y, 1f);
        if (Vector3.Distance(finalMousePos, transform.position) < clickDistance)
        {
            grabbed = true;
            UnityEngine.Cursor.visible = false;
        }
        return grabbed;
    }

    private void OnEnable()
    {
        PlayerController.OnShapeChange += ChangeShape;
    }

    private void OnDisable()
    {
        PlayerController.OnShapeChange -= ChangeShape;
    }

    public void ChangeShape(int index)
    {
        currentMeshFilterIndex = index;
        meshFilter.mesh = meshList[currentMeshFilterIndex].sharedMesh;
    }
}
