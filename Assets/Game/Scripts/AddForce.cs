using UnityEngine;
using Cysharp.Threading.Tasks;

public class AddForce : MonoBehaviour
{
    public AddForce otherEndBall;
    public Rigidbody rb;
    public float forceAmount = 100f;
    public bool startFirst;
    public AudioSource audioSource;

    private UniTaskCompletionSource collisionTcs;
    private bool isAnimating;

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


        otherEndBall.AnimateOnce().Forget();
        isAnimating = false;
    }

    void OnCollisionEnter(Collision _)
    {
        if (collisionTcs != null && !collisionTcs.Task.Status.IsCompleted())
        {
            UniTask.Delay(100);
            collisionTcs.TrySetResult();
            audioSource.Play();
        }
    }
}