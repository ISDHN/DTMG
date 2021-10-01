using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
	public int type;
	public bool shown = false;
	public bool scored = false;
	public float holdtime;
	public float appeartime;
	public float judgetime;
	public float speed;
	public int delay;
	public JudgeLine line;
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		float now = line.Now;
		if (appeartime + delay <= now)
		{
			if (!shown)
			{
				gameObject.transform.position += new Vector3(0, 0, 1);
				shown = true;
			}
		}
		float min = judgetime - 80 + delay;
		float max = judgetime + 80 + delay;
		if (min <= now & max >= now)
		{
			float x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
			switch (type)
			{
				case 1:
					if (Input.GetMouseButton(0) & x >= gameObject.transform.position.x - 3 & x <= gameObject.transform.position.x + 3 )
					{
						scored = true;
					}
					break;
				case 2:
					if (Input.GetMouseButtonDown(0) & x >= gameObject.transform.position.x - 3 & x <= gameObject.transform.position.x + 3)
					{
						scored = true;
					}
					break;
			}
		}
		if (max < now & now <= judgetime + holdtime + delay)
		{
			float x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
			if ((!Input.GetMouseButton(0) | x > gameObject.transform.position.x + 3 | x < gameObject.transform.position.x - 3)&scored)
				scored = false;
		}
		if (max + holdtime < now)
		{
			if (!scored)
			{
				line.DecreaseScore();
			}
			Destroy(gameObject);
		}
		
	}
	private void FixedUpdate()
	{
		if (shown)
			gameObject.transform.Translate(new Vector3(0, -speed * 1000 * Time.fixedDeltaTime));
	}
}
