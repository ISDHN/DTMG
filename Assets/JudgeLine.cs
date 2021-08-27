using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public class JudgeLine : MonoBehaviour
{
    public GameObject tap;
    public GameObject flick;
    public GameObject drag;
    private AudioSource source;
    private List<Note> m_notes = new List<Note>();
    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        source.Play(0);
    }
    // Update is called once per frame
    void Update()
    {
        foreach (Note note in m_notes)
        {
            if (note.appeartime <= (float)source.timeSamples / source.clip.frequency*1000)
            {
                m_notes.Remove(note);
                note.showed = true;               
            }
            else
            {
                break;
            }
        }
    }
}
