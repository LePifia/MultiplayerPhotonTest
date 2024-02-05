using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

public class Attack : MonoBehaviour
{
    [SerializeField] GameObject fireballPrefab;
    [SerializeField] Transform fireballSpawnPoint;
    [SerializeField] float fireballSpeed;

    private void Awake()
    {
        Assert.IsNotNull(fireballPrefab);
        Assert.IsNotNull(fireballSpawnPoint);
    }

    public void Fire()
    {
        Debug.Log("Fire");
        GameObject attack = Instantiate(fireballPrefab, fireballSpawnPoint.position, gameObject.GetComponent<Player>().GetDirection());
        attack.GetComponent<Rigidbody>().velocity = attack.transform.forward * fireballSpeed;
        Destroy(attack, 3.5f);
    }
}
