using UnityEngine;

namespace SharpMonEngine.Unity.Sprites
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class OverworldCharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Sprite walkingFaceUp;
        [SerializeField] private Sprite walkingFaceDown;
        [SerializeField] private Sprite walkingFaceRight;
        [SerializeField] private Sprite walkingFaceLeft;

        [SerializeField] private Sprite runFaceUp;
        [SerializeField] private Sprite runFaceDown;
        [SerializeField] private Sprite runFaceRight;
        [SerializeField] private Sprite runFaceLeft;
    }
}