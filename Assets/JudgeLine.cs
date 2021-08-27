using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public class JudgeLine : MonoBehaviour
{
    public GameObject tap;
    public GameObject flick;
    public GameObject drag;
    private Thread notedispatcher;
    private AudioSource source;
    private List<Note> m_notes = new List<Note>();
    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        source.Play(0);
    }
    private void DisPatchNote()
    {
        foreach (Note note in m_notes)
        {
            if (note.appeartime<=(float)source.timeSamples/source.clip.frequency)
            {

            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
