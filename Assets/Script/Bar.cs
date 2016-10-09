using UnityEngine;
using System.Collections;

abstract public class Bar : MonoBehaviour
{
    public Sprite barSprite;
    public Sprite frameSprite;
    public Sprite delaySprite;
    public GameObject bar;
    public GameObject frame;
    public GameObject delay;
    public Vector3 scale;
    public Vector3 offset;
    public float max;
    public float value;
    public float dvalue; 
    public Unit unit;

	// Use this for initialization
	virtual protected void Start () {
        bar.transform.localScale = scale;
        frame.transform.localScale = scale;
        delay.transform.localScale = scale;
        bar.GetComponent<SpriteRenderer>().sprite = barSprite;
        frame.GetComponent<SpriteRenderer>().sprite = frameSprite;
        delay.GetComponent<SpriteRenderer>().sprite = delaySprite;
        transform.localPosition = offset;
	}

    void FixedUpdate()
    {
        if (unit == null || unit.isDead)
        {
            Destroy(gameObject);
        }
        else
        {
            updateValue();
            updateRenderer();
        }
    }

    abstract protected void updateValue();
    
    protected void updateRenderer() {

        if (max == 0) 
            return;
        float spriteWidth = bar.GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        Vector3 scaleNow = bar.transform.localScale;
        Vector3 posNow = bar.transform.localPosition;
        scaleNow.x = Mathf.Lerp(scale.x*value/max, scaleNow.x, 0.5f);
        posNow.x = Mathf.Lerp(scale.x*(value/max-1)/2*spriteWidth, posNow.x, 0.5f);
        bar.transform.localScale = scaleNow;
        bar.transform.localPosition = posNow;

        Vector3 dScaleNow = delay.transform.localScale;
        Vector3 dPosNow = delay.transform.localPosition;
        dScaleNow.x = Mathf.Lerp(scale.x * value / max, dScaleNow.x, 0.9f);
        dPosNow.x = Mathf.Lerp(scale.x * (value / max - 1) / 2 * spriteWidth, dPosNow.x, 0.9f);
        delay.transform.localScale = dScaleNow;
        delay.transform.localPosition = dPosNow;

        bar.GetComponent<SpriteRenderer>().sortingOrder = (int)(-unit.transform.position.y * 100) + 1;
        frame.GetComponent<SpriteRenderer>().sortingOrder = (int)(-unit.transform.position.y * 100) + 2;
        delay.GetComponent<SpriteRenderer>().sortingOrder = (int)(-unit.transform.position.y * 100);
    }
}
