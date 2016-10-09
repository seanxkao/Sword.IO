using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
    protected string state;
    protected float stateStartTime;

    protected virtual IEnumerator main() {
        while (true)
        {
            stateStartTime = Time.time;
            yield return StartCoroutine(state);
        }
    }
    
    protected void changeState(string state){
        this.state = state;
    }

    protected float stateTime() {
        return Time.time - stateStartTime;
    }
	
}
