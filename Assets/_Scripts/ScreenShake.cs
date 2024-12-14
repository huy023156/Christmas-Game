using DG.Tweening;
using UnityEngine;

public class ScreenShake : Singleton<ScreenShake>
{

    [ContextMenu("Shake")]
    public void ShakeTest()
    {
        Shake(1f);
    }

    public void Shake(float duration = 1f, float intensity = 1f)
    {
        Camera.main.DOShakePosition(duration, intensity);
    }
}
