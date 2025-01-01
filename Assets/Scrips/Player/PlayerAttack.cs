using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(FindClosestEnemy());
    }
    void Update()
    {
        
    }
    IEnumerator FindClosestEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }

            if (closestEnemy != null)
            {
                Debug.Log("Closest Enemy: " + closestEnemy.name);
            }
        } 
    }
}
