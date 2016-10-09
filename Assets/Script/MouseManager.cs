using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour {
    public GameObject hoveredObject;
    public Vector2 cursorPos;

	void Start () {
        hoveredObject = null;
	}
	void Update () {
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(cursorPos, Vector2.zero, 0f);
        hoveredObject = null;
        foreach(RaycastHit2D hit in hits){
            if (hit.collider!=null)
            {
                GameObject selected = hit.collider.gameObject;
                if(selected!=null&&selected.GetComponent<SelectArea>()){
                    hoveredObject = selected.GetComponent<SelectArea>().owner;
                    break;
                }
            }
        }
	}
}
