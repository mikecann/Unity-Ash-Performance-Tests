using Assets.Scripts.Nodes;
using Net.RichardLord.Ash.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class MovementSystem : SystemBase
    {
        private Bounds bounds;
        private NodeList nodes;

        public MovementSystem()
        {
            var size = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            bounds = new Bounds(Vector3.zero, new Vector3(size.x * 2, size.y * 2));
        }

        override public void AddToGame(IGame game)
        {
            nodes = game.GetNodeList<MovementNode>();
        }

        override public void Update(float time)
        {
            var cam = Camera.main;
            for (var node = (MovementNode)nodes.Head; node != null; node = (MovementNode)node.Next)
            {
                var transform = node.Transform;
			    var rigidbody = node.Rigidbody;

                if (transform.position.x < bounds.min.x)
			    {
                    transform.position = new Vector3(transform.position.x + bounds.size.x, transform.position.y, transform.position.z);
			    }
                if (transform.position.x > bounds.max.x)
                {
                    transform.position = new Vector3(transform.position.x - bounds.size.x, transform.position.y, transform.position.z);
                }
                if (transform.position.y < bounds.min.y)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + bounds.size.y, transform.position.z);
                }
                if (transform.position.y > bounds.max.y)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - bounds.size.y, transform.position.z);
                }
            }
        }

        override public void RemoveFromGame(IGame game)
        {
            nodes = null;
        }
    }
}
