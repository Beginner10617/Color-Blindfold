using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    RaycastHit2D rayHitInfo;
    Vector2 endPoint;
    public bool laserOn;
    [SerializeField] GameObject startBeam;
    [SerializeField] GameObject midBeam;
    [SerializeField] GameObject particlesPrefab;
    [SerializeField] Vector2 colliderOffset;
    GameObject particles;
    [SerializeField] LayerMask layerOfBounds;
    [SerializeField] float damagePerSecond = 25;
    void Start()
    {
        particles = Instantiate(particlesPrefab, transform.parent);
    }
    void Update()
    {
        if(!laserOn)
        {
            particles.SetActive(false);
            ClearRay();
            return;
        }
        particles.SetActive(true);
        rayHitInfo = Physics2D.Raycast(transform.position, -transform.up, Mathf.Infinity, ~layerOfBounds);
        if(rayHitInfo.collider == null) rayHitInfo = Physics2D.Raycast(transform.position, -transform.up, Mathf.Infinity);
        if(rayHitInfo.collider != null)
        {
            endPoint = rayHitInfo.point;
            if(rayHitInfo.collider.CompareTag("Player"))
            {
                rayHitInfo.collider.GetComponent<HealthSystem>().TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
        
        particles.transform.position = endPoint;
        ClearRay();
        DrawRay();
    }
    void ClearRay()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
    void DrawRay()
    {
        Instantiate(startBeam, transform.position, transform.rotation, transform);
        for(int i = 1; i < (endPoint - (Vector2) transform.position).magnitude; i++)
        {
            Instantiate(midBeam, (Vector2) transform.position + (endPoint - (Vector2) transform.position).normalized * i, transform.rotation, transform);
        }
        if(Vector3.Distance(transform.position, endPoint) > 1) Instantiate(midBeam, endPoint+(Vector2)transform.up.normalized*0.5f, transform.rotation, transform);
    }
}