using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Vignette : MonoBehaviour
{
    Volume volume;
    LensDistortion lensDistortion;

    public AnimationCurve animationCurve;

    // Start is called before the first frame update
    void Start()
    {
        Volume volume = gameObject.GetComponent<Volume>();
        LensDistortion tmp;

        if (volume.profile.TryGet<LensDistortion>(out tmp))
        {
            lensDistortion = tmp;
        }
    }

    IEnumerator AutoShake()
    {
        float progress = 0;
        Vector2 center;

        while (progress < 1)
        {
            Debug.Log(progress);
            progress += Time.deltaTime;
            float x = Mathf.Lerp(0.5f, Random.Range(-0.5f, 2), animationCurve.Evaluate(progress));
            float y = Mathf.Lerp(0.5f, Random.Range(-1.3f, 2), animationCurve.Evaluate(progress));
            center = new Vector2(x, y);
            lensDistortion.center = new Vector2Parameter(center, true);
            yield return new WaitForSeconds(0);
        }
    }
}
