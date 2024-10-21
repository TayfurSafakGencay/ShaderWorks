using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 endPosition;

    private void Start()
    {
        startPosition = new Vector3(5.75f, transform.position.y, transform.position.z);
        transform.position = startPosition;

        StartCoroutine(MoveCamera());
    }

    private IEnumerator MoveCamera()
    {
        yield return new WaitForSeconds(3f);

        endPosition = new Vector3(0f, transform.position.y, transform.position.z);
        yield return StartCoroutine(MoveOverTime(1f, startPosition, endPosition));

        yield return new WaitForSeconds(3f);

        startPosition = endPosition;
        endPosition = new Vector3(-5.75f, transform.position.y, transform.position.z);
        yield return StartCoroutine(MoveOverTime(1f, startPosition, endPosition));
        
        yield return new WaitForSeconds(3f);
        startPosition = endPosition;
        endPosition = new Vector3(0, 10f, -22.5f);
        yield return StartCoroutine(MoveOverTime(3f, startPosition, endPosition));
    }

    private IEnumerator MoveOverTime(float duration, Vector3 fromPosition, Vector3 toPosition)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(fromPosition, toPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null; 
        }

        transform.position = toPosition;
    }
}
