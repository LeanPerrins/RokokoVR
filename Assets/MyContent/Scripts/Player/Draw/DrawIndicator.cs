using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawIndicator : MonoBehaviour
{

    [SerializeField]private ParticleSystem particle;
    [SerializeField] private List<LineRenderer> lineRenderers;
    // Start is called before the first frame update
    void Start()
    {
        StartIndicatingPath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartIndicatingPath()
    {
        particle.transform.position = lineRenderers[0].GetPosition(0);
        Debug.Log(lineRenderers[0].GetPosition(1));
        StartCoroutine(FollowLines());
    }

    private IEnumerator FollowLines()
    {
        float ta = 0;
        particle.transform.position = lineRenderers[0].GetPosition(0);
        
        for (int i = 0; i < 1000; i++)
        {
            
            ta = (float)i/1000;
            particle.transform.position = Vector3.Lerp(lineRenderers[0].GetPosition(0), lineRenderers[0].GetPosition(1), ta);
            Debug.Log(ta);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(1);
        StartIndicatingPath();

    }
}
