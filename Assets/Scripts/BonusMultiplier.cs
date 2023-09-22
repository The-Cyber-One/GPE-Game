using System.Collections;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class BonusMultiplier : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Slider slider;

    private float _timer;

    public Vector2Int Position => new((int)transform.position.x, (int)transform.position.y);

    private IEnumerator Start()
    {
        while (_timer < time)
        {
            yield return null;
            _timer += Time.deltaTime;
            slider.value = 1 - _timer / time;
        }
        Destroy(gameObject);
    }
}
