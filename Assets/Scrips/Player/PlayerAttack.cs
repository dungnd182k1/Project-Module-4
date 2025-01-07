using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform weaponTranform; // Vị trí vũ khí, tâm bám kính tìm kiếm enemy
    [SerializeField] float detectionRadius = 10f; // Bán kính tìm kiếm
    [SerializeField] float delayTime = 2f; // Khoản cách giữa các lầm tìm kiếm enemy mới
    [SerializeField] GameObject closetEnemy;

    [SerializeField] GameObject targerRing;

    private void Start()
    {
        StartCoroutine(FindClosetEnemyFixtest());
    }
    void Update()
    {
         TargetRing();
    }
    IEnumerator FindClosetEnemyFixtest()
    {
        while (true)
        {
            if (closetEnemy == null)
            {
                closetEnemy = FindClosetEnemy();
            }
            else
            {
                yield return new WaitForSeconds(delayTime);
                closetEnemy = FindClosetEnemy();
            }
        }
    }
    GameObject FindClosetEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(weaponTranform.position, detectionRadius);
        GameObject closet = null;
        float shortesDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if ( collider.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(weaponTranform.position, collider.transform.position);
                if ( distance < shortesDistance )
                {
                    shortesDistance = distance;
                    closet = collider.gameObject;
                }
            }
        }
        return closet;
    }
    void TargetRing()
    {
        if (closetEnemy == null)
        {
            targerRing.transform.position = new Vector3 (targerRing.transform.position.x, -5f, targerRing.transform.position.z);
        }
        else
        {
            targerRing.transform.position = closetEnemy.transform.position;
        }
    }
}
