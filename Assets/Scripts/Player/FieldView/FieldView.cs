using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FieldView : MonoBehaviour
{
    #region Parameters

    private LayerMask layerMask;
    private MeshFilter _meshFilter;
    private Transform playerPos;
    private float fov;
    private int rayCount;
    private float angle;
    private float angleIncrease;
    private float viewDistance;
    private float rotateModifier;
    private float startingAngle;

    #endregion
    
    private void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        Physics2D.queriesStartInColliders = false;
        playerPos = PlayerStatus.playerObj.transform;

        layerMask = PlayerStatus.status.GetLayerMaskView();
        fov = PlayerStatus.status.GetFov();
        rayCount = PlayerStatus.status.GetRayCount();
        viewDistance = PlayerStatus.status.GetViewDistance();
        rotateModifier = PlayerStatus.status.GetRotateModifier();
        angleIncrease = fov / rayCount;
        
    }

    private void LateUpdate()
    {
        Mesh mesh = new Mesh();
        _meshFilter.mesh = mesh;

        Vector3 origin = playerPos.position;
        SetAimDirection(Camera.main.ScreenToWorldPoint(Input.mousePosition) - origin);
        angle = startingAngle;
        
        
        
        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;  
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin,GetVectorFromAngle(angle) , viewDistance, layerMask);
            if (raycastHit2D.collider != null)  vertex = raycastHit2D.point;
            vertices[vertexIndex] = vertex;
            
            if(i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }
            
            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad),Mathf.Sin(angleRad));
    }

    private float GetAngleFromVector(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public void SetAimDirection(Vector3 aimDir)
    {
        startingAngle = GetAngleFromVector(aimDir) + fov / 2f;
    }
    
    private void lookToMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        angle = Vector2.SignedAngle(Vector2.right, direction);
        Vector3 targetRotation = new Vector3(0, 0, angle);
        transform.rotation = (Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), rotateModifier));
    }

}
