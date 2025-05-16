using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Game
{
    public class UniTaskTests : MonoBehaviour
    {
        private void Start()
        {
            MakePizza().Forget();
        }

        private async UniTask MakePizza()
        {
            Debug.Log("Putting pizza at oven");
            await UniTask.Delay(5000);
            Debug.Log("Pizza is Ready");
        }
    }
}