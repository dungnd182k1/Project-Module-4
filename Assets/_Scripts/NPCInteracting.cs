using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteracting : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    LayerMask mask;
    float distance;
    const string CHOOSE_WEPON_NPC_TAG = "ChooseWeaponNPC";
    const string UPGRADE_NPC_TAG = "UpgradeNPC";
    const string BUFF_INFO_NPC_TAG = "BuffInfoNPC";

    private void Awake()
    {
        mask = LayerMask.GetMask("NPC");
        distance = Camera.main.transform.position.y;
    }

    void Update()
    {
        CheckTouch();
    }

    void CheckTouch()
    {
        if (Input.touchCount > 0/*Input.GetMouseButtonDown(0)*/)
        {
            ChooseNPC();
        }
    }

    void ChooseNPC()
    {
        ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position/*Input.mousePosition*/);
        if (Physics.Raycast(ray, out hit, distance, mask))
        {
            DisplayUI(hit.collider.gameObject);
        }
    }

    void DisplayUI(GameObject npcSensorGameObject)
    {
        switch (true)
        {
            case bool when npcSensorGameObject.CompareTag(CHOOSE_WEPON_NPC_TAG):
                Debug.Log("Hiện Weapon UI");
                return;
            case bool when npcSensorGameObject.CompareTag(BUFF_INFO_NPC_TAG):
                Debug.Log("Hiện Buff Info UI");
                return;
            case bool when npcSensorGameObject.CompareTag(UPGRADE_NPC_TAG):
                Debug.Log("Hiện Upgrade UI");
                return;
        }
    }
}