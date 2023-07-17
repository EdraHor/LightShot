using Pool;
using UnityEngine;

[RequireComponent(typeof(PoolObject), typeof(AudioSource))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private TrailRenderer trail;
    private AudioSource audioSource;
    private PoolObject pool;
    [SerializeField] private AudioClip startSound;
    [SerializeField] private AudioClip endSound;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        pool = GetComponent<PoolObject>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<IDamagable>()?.Kill();
        particle.Play();
        pool.ReturnToPoolDelay(particle.main.duration);
    }

    private void OnEnable()
    {
        audioSource.PlayOneShot(startSound);
        trail.Clear();
        trail.emitting = true;
    }

    private void OnDisable()
    {
        particle.Play();
        trail.emitting = false;
    }

}
