
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public class TestHelpers
    {
        public static void Execute(int iterations, int repeats, string name, Action test, Action betweenRepeats = null)
        {
            Debug.Log(String.Format("Running tests for '{0}'", name));

            var deltas = new List<TimeSpan>();
            for (int i = 0; i < repeats; i++)
            {
                var startTime = DateTime.Now;
                for (int j = 0; j < iterations; j++)
                    test();
                var endTime = DateTime.Now;
                var deltaTime = endTime - startTime;
                deltas.Add(deltaTime);
                Debug.Log(String.Format("'{0}' iteration[{1}] took {2}ms", name, i, deltaTime.Milliseconds));
                if (betweenRepeats != null) betweenRepeats();
            }

            var average = deltas.Sum(d => d.Milliseconds) / deltas.Count;
            Debug.Log(String.Format("'{0}' average elapsed time was {1}ms", name, average));
        }

        public static void Time(Action call)
        {
            var before = DateTime.Now;
            call();
            var after = DateTime.Now;
            Debug.Log("Took "+(after-before).TotalMilliseconds + "ms");
        }
    }
}
