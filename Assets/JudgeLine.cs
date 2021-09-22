using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine.UI;

public class JudgeLine : MonoBehaviour
{
	public GameObject tap;
	public GameObject flick;
	public GameObject drag;
	public GameObject text;
	public float speed;
	public int delay;
	private AudioSource source;
	private float score=10000;
	private List<Note> m_notes = new List<Note>();
	private float distance;
	private float perscore;
	// Start is called before the first frame update
	void Start()
	{
		Camera cam = Camera.main;
		LineRenderer line = this.GetComponent<LineRenderer>();
		
		distance = cam.transform.position.y + cam.orthographicSize * 2 / cam.aspect - line.GetPosition(0).y;
		StreamReader file = new StreamReader(Application.streamingAssetsPath + "/notes.txt");    
		while (!file.EndOfStream)
		{
			Note note= new Note();
			
			note.type = int.Parse(file.ReadLine());
			int starttime = int.Parse(file.ReadLine());
			int endtime = int.Parse(file.ReadLine());
			//if (note.type == 2 && (endtime - starttime >= 300))note.holdtime = endtime - starttime;
			note.judgetime = starttime;
			note.appeartime = starttime - distance / speed;
			note.shown = false;
			switch (note.type)
			{
				case 1:
					note.note = Instantiate(drag);
					break;
				case 2:
					note.note = Instantiate(tap);
					break;
			}
			note.note.transform.position=new Vector3(
				Random.Range(cam.transform.position.x - cam.orthographicSize*2, cam.transform.position.x + cam.orthographicSize*2),
				cam.transform.position.y + cam.orthographicSize*2/cam.aspect+ note.holdtime * speed/4,
				-1);
			note.note.transform.localScale += new Vector3(0, note.holdtime * speed);
			m_notes.Add(note);
		}
		perscore = 10000 / m_notes.Count;
		source = gameObject.GetComponent<AudioSource>();
		source.Play(0);
	}
	// Update is called once per frame
	void Update()
	{
		for (int i = 0; i < m_notes.Count; i++)
		{
			Note note = m_notes[i];
			float now = (float)source.timeSamples / source.clip.frequency * 1000;			
			if (note.appeartime+delay <= now)
			{
				if (!note.shown)
				{
					note.note.transform.position += new Vector3(0, 0, 1);
					note.shown = true;
				}
				m_notes[i]=note;
			}
			else
			{
				break;
			}
			float min = note.judgetime - 80 + delay;
			float max = note.judgetime + 80 + delay;
			if (min <= now & max >= now)
			{
				float x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
				switch (note.type)
				{
					case 1:
						if (Input.GetMouseButton(0) & x <= note.note.transform.position.x + 3 & x >= note.note.transform.position.x - 3)
						{
							m_notes.RemoveAt(i);
							Destroy(note.note);
							note.note = null;
						}
						break;
					case 2:					
						if (Input.GetMouseButtonDown(0) & x <= note.note.transform.position.x + 3 & x >= note.note.transform.position.x - 3)
						{
							m_notes.RemoveAt(i);
							Destroy(note.note);
							note.note = null;
						}
						break;
				}
			}
			if (note.judgetime+note.holdtime + delay +200<= now)
			{
				score -= perscore;
				text.GetComponent<Text>().text = ((int)score).ToString();
				m_notes.RemoveAt(i);
				Destroy(note.note);
				note.note = null;
			}
		}
	}
	void FixedUpdate()
	{
		for (int i=0;i<m_notes.Count;i++)
		{
			Note note = m_notes[i];
			if(note.shown)
				note.note.transform.Translate(new Vector3(0, -speed * 1000 * Time.fixedDeltaTime));
			
		}
		
	}
    public struct Note
	{
		public int type;
		public bool shown;
		public float holdtime;
		public float appeartime;
		public float judgetime;
		public GameObject note;
	}
}
