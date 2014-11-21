using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class AsteroidController : MonoBehaviour
    {
        private Bounds bounds;

        void Awake()
        {
            var size = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            bounds = new Bounds(Vector3.zero, new Vector3(size.x * 2, size.y * 2));
        }

        void Update()
        {
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
}
