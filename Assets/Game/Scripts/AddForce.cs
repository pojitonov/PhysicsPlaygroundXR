using UnityEngine;
using Cysharp.Threading.Tasks;

public class CradleEndBall : MonoBehaviour
{
    public CradleEndBall otherEndBall;
    public Rigidbody rb;
    public float forceAmount = 100f;
    public bool startFirst;
    public int cooldown = 1;
    public AudioSource audioSource;

    private UniTaskCompletionSource collisionTcs;
    private bool isAnimating = false;

    void Start()
    {
        if (startFirst)
        {
            AnimateOnce().Forget();
        }
    }

    async UniTaskVoid AnimateOnce()
    {
        if (isAnimating) return;
        isAnimating = true;

        collisionTcs = new UniTaskCompletionSource();

        rb.linearVelocity = Vector3.zero;
        rb.AddForce(Vector3.left * forceAmount, ForceMode.Impulse);

        await collisionTcs.Task;

        await UniTask.Delay(cooldown * 1000);

        otherEndBall.AnimateOnce().Forget();
        isAnimating = false;
    }

    void OnCollisionEnter(Collision _)
    {
        if (collisionTcs != null && !collisionTcs.Task.Status.IsCompleted())
        {
            collisionTcs.TrySetResult();
            audioSource.Play();
        }
    }
}