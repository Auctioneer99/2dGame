using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.AI.Detection.Player
{
    internal class PlayerDetection : MonoBehaviour, IPlayerDetection
    {
        private static int DETECTION_LAYER = 1;

        public PlayerController Player => _players.FirstOrDefault();

        private List<PlayerController> _players = new List<PlayerController>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out var c))
            {
                _players.Add(c);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out var c))
            {
                _players.Remove(c);
            }
        }

        public bool IsPlayerVisible(Vector3 from)
        {
            var p = Player;
            if (p == null)
            {
                return false;
            }
            var visible = Physics.Linecast(p.transform.position, from, DETECTION_LAYER) == false;
            return visible;
        }
    }
}
