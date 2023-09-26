using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent((typeof(Collider2D)))]
[RequireComponent((typeof(Animator)))]
public class GrenadeProjectile : MonoBehaviour
{
    [SerializeField]
    public float lifeTime;
    public float explosionDamage;
    public float explosionSize;
    public float knockBackForce;

    [NonSerialized] public GameObject firedBy = null;

    private string _firedByTag;
    private float _timePassed;
    private Rigidbody2D _rb;
    private Animator _animator;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        if (firedBy != null)
            _firedByTag = firedBy.gameObject.tag;
    }

    void Update()
    {
        _timePassed += Time.deltaTime;

        if (_timePassed > lifeTime)
        {
            _rb.simulated = false;
            transform.rotation = Quaternion.identity;
            StartCoroutine(PlayAndWaitForAnim(_animator, "Explosion"));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _rb.simulated = false;
        transform.rotation = Quaternion.identity;
        transform.localScale *= explosionSize;
        StartCoroutine(PlayAndWaitForAnim(_animator, "Explosion"));
    }

    private void attack()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, explosionSize);

        foreach (var c in cols)
        {
            GameObject g = c.gameObject;

                //&& (_firedByTag == null ? true :!g.CompareTag(_firedByTag))
                //&& g.layer == LayerMask.NameToLayer("Damageable")
            if (g.TryGetComponent(out IDamageable hit))
            {
                hit.damage(explosionDamage);

                if (g.TryGetComponent(out Rigidbody2D rb2d))
                {
                    Vector3 direction = transform.position - g.transform.position;
                    Debug.Log(direction * knockBackForce);
                    rb2d.AddForce(-direction * knockBackForce, ForceMode2D.Impulse);
                }
            }
        }
    }

    public IEnumerator PlayAndWaitForAnim(Animator targetAnim, string stateName)
    {
        attack();


        // targetAnim.CrossFadeInFixedTime(stateName, 0.6f);

        //Wait until we enter the current state
        targetAnim.Play(stateName);
        while (!targetAnim.GetCurrentAnimatorStateInfo(0).IsName(stateName))
        {
            yield return null;
        }

        float counter = 0;
        float waitTime = targetAnim.GetCurrentAnimatorStateInfo(0).length;

        //Now, Wait until the current state is done playing
        while (counter < (waitTime))
        {
            counter += Time.deltaTime;
            yield return null;
        }
        

        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionSize);
    }
}
