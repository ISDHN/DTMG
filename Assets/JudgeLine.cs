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
	private List<Note> m_notes = new List<Note>();
	private float score=10000;
	private float distance;
	private float perscore;
	public float Now
    {
        get
        {
			return (float)source.timeSamples / source.clip.frequency * 1000;
		}
    }
	// Start is called before the first frame update
	void Start()
	{
		source = gameObject.GetComponent<AudioSource>();
		Camera cam = Camera.main;		
		distance = cam.transform.position.y + cam.orthographicSize * 2 / cam.aspect - this.gameObject.transform.position.y;
		StreamReader file = new StreamReader(Application.streamingAssetsPath + "/notes.txt");
		int count=0;
		while (!file.EndOfStream)
		{			
			int type = int.Parse(file.ReadLine());		
			int starttime = int.Parse(file.ReadLine());
			int endtime = int.Parse(file.ReadLine());
			Note note;
			switch (type)
			{
				case 1:
					note = Instantiate(drag).GetComponent<Note>();
					break;
				case 2:
					note = Instantiate(tap).GetComponent<Note>();
					break;
				default:
					continue;
			}
			note.type = type;
			note.speed=speed;
			note.delay=delay;
			note.line = this;
			if (type == 2 && (endtime - starttime >= 300))
				note.holdtime = endtime - starttime;
			note.judgetime = starttime;
			note.appeartime = starttime - distance / speed;			
			note.gameObject.transform.position=new Vector3(
				Random.Range(cam.transform.position.x - cam.orthographicSize*2, cam.transform.position.x + cam.orthographicSize*2),
				//cam.transform.position.x,
				cam.transform.position.y + cam.orthographicSize*2/cam.aspect+ note.holdtime * speed/2,
				-1);
			note.gameObject.transform.localScale += new Vector3(0, note.holdtime * speed * 2);
			count++;
			//m_notes.Add(note);
		}
		perscore = 10000 / count;		
		source.Play(0);
	}
	// Update is called once per frame
	void Update()
	{
		
	}
	void FixedUpdate()
	{
		
	}
	public void DecreaseScore()
    {
		score -= perscore;
		text.GetComponent<Text>().text = ((int)score).ToString();
	}
}
