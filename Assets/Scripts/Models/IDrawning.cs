using UnityEngine;

namespace Assets.Scripts.Models
{
    public interface IDrawning
    {
        public void Draw(Vector2 uvposition);

        public void Init();
    }
}
