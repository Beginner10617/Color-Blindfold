using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Transform player;
    [SerializeField] float minimumDamage = 75;
    [SerializeField] float minimumDamageDistance = 1.5f;
    [SerializeField] float explosionDelay = .5f;
    Animator animator;
    [SerializeField] RuntimeAnimatorController animationController;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(Explode(other));
    }
    IEnumerator Explode(Collider2D other)
    {
        GetComponent<SpriteRenderer>().sortingOrder = 20;
        animator = gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = animationController;
        while(explosionDelay > 0)
        {
            other.transform.position = transform.position;
            explosionDelay -= Time.deltaTime;
            yield return null;
        }
        if(other.CompareTag("Player"))
        {
            other.GetComponent<HealthSystem>().TakeDamage(100);
        }
        else if(Vector3.Distance(player.position, transform.position) < minimumDamageDistance)
        {
            Destroy(other.gameObject);
            player.GetComponent<HealthSystem>().TakeDamage(minimumDamage);
        }
        Destroy(gameObject);
        yield return null;
    }
}
