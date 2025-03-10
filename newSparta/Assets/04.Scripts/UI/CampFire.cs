using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    public int damage;
    public float damageRate;

    private List<IDamageable> things = new List<IDamageable>();
    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("DealDamage", 0 ,damageRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DealDamage()
    {
        for(int i = 0; i<things.Count; i++)
        {
            things[i].TakePhysicalDamage(damage);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IDamageable damageable))
        {
            things.Add(damageable);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out IDamageable damageable))
        {
            things.Remove(damageable);
        }
        
    }
}
