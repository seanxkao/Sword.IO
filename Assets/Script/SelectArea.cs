using UnityEngine;
using System.Collections;

public class SelectArea : MonoBehaviour {
    public Unit unit;
    public GameObject owner;
    void Start() { 
        owner = transform.parent.gameObject;
        unit = owner.GetComponent<Unit>();
    }
}
