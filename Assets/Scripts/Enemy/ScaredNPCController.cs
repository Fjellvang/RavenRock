using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(CharacterController2D))]
    public class ScaredNPCController : MonoBehaviour
    {
        public float moveSpeed = 10f;
    }
}
