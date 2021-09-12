using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
public class JudgeLine : MonoBehaviour
{
	public GameObject tap;
	public GameObject flick;
	public GameObject drag;
	public float speed;
	public int delay;
	private AudioSource source;
	private List<Note> m_notes = new List<Note>();
	private List<Note> m_shown = new List<Note>();
	private float distance;

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
			if (note.type == 2 && (endtime - starttime >= 300))
				note.holdtime = endtime - starttime;
			note.judgetime = starttime;
			note.appeartime = starttime - distance / speed;
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
				cam.transform.position.y + cam.orthographicSize*2/cam.aspect,
				-1);
			note.note.transform.localScale += new Vector3(0, note.holdtime * speed);
			m_notes.Add(note);
		}
		source = gameObject.GetComponent<AudioSource>();
		source.Play(0);
	}
    // Update is called once per frame
    private void Update()
    {
		for (int i = 0; i < m_notes.Count; i++)
		{
			Note note = m_notes[i];
			if (note.appeartime+delay <= (float)source.timeSamples / source.clip.frequency * 1000)
			{
				m_notes.Remove(note);
				note.note.transform.position += new Vector3(0, 0, 1);
				m_shown.Add(note);
			}
			else if (note.judgetime+delay<= (float)source.timeSamples / source.clip.frequency * 1000)
			{
				m_shown.Remove(note);
				Destroy(note.note);
				note.note = null;				
			}
			else
			{
				break;
			}
		}
	}
    void FixedUpdate()
	{
		for (int i=0;i<m_shown.Count;i++)
		{
			Note note = m_shown[i];
			note.note.transform.Translate(new Vector3(0, -speed * 1000 * Time.fixedDeltaTime));
		}
		
	}
    public struct Note
    {
		public int type;
		public float holdtime;
		public float appeartime;
		public float judgetime;
		public GameObject note;
	}
}
