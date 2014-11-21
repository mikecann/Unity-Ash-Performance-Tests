using Assets.Scripts.Helpers;
using Net.RichardLord.Ash.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Tests
{
    public class RuntimeTests : MonoBehaviour
    {
        public GameObject asteroidsContainer;
        public AsteroidsGame game;

        public GameObject unityAsteroidPrefab;
        public GameObject ashAsteroidPrefab;

        private int asteroidCount = 10000;

        private Bounds bounds;
        private int testStage = -1;
        private int frameCount = 0;
        private DateTime startTime;

        public void RunTests()
        {
            testStage = 1;
            var size = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            bounds = new Bounds(Vector3.zero, new Vector3(size.x * 2, size.y * 2));
        }
        
        void Update()
        {
            if (testStage == -1) { }
            else if (testStage == 1)
            {
                Debug.Log("Creating " + asteroidCount + " Unity Asteroids..");

                TestHelpers.Time(() =>
                {
                    for (int i = 0; i < asteroidCount; i++)
                        CreateAsteroid(unityAsteroidPrefab, asteroidsContainer.transform);
                });

                testStage++;
            }
            else if (testStage == 2)
            {
                startTime = DateTime.Now;
                frameCount = 0;
                testStage++;
            }
            else if (testStage == 3)
            {
                frameCount++;
                if ((DateTime.Now - startTime).TotalSeconds > 10)
                {
                    Debug.Log("In 10 seconds " + frameCount + " frames were proccessed (" + (frameCount/10.0) +"FPS)");
                    DestroyAllChildrenOf(asteroidsContainer);
                    testStage++;
                }
            }
            else if (testStage == 4)
            {
                Debug.Log("Creating " + asteroidCount + " Ash Asteroids using update every frame");

                TestHelpers.Time(() =>
                {
                    for (int i = 0; i < asteroidCount; i++)
                    {
                        CreateAsteroid(ashAsteroidPrefab, game.transform)
                            .GetComponent<Entity>()
                            .updateFrequency = UpdateFrequency.EveryFrame;
                    }
                        
                });

                testStage++;
            }
            else if (testStage == 5)
            {
                startTime = DateTime.Now;
                frameCount = 0;
                testStage++;
            }
            else if (testStage == 6)
            {
                frameCount++;
                if ((DateTime.Now - startTime).TotalSeconds > 10)
                {
                    Debug.Log("In 10 seconds " + frameCount + " frames were proccessed (" + (frameCount / 10.0) + "FPS)");
                    DestroyAllChildrenOf(game.gameObject);
                    testStage++;
                }
            }
            else if (testStage == 7)
            {
                Debug.Log("Creating " + asteroidCount + " Ash Asteroids using update every other frame");

                for (int i = 0; i < asteroidCount; i++)
                {
                    CreateAsteroid(ashAsteroidPrefab, game.transform)
                        .GetComponent<Entity>()
                        .updateFrequency = UpdateFrequency.EveryOtherFrame;
                }                    

                startTime = DateTime.Now;
                frameCount = 0;
                testStage++;
            }
            else if (testStage == 8)
            {
                frameCount++;
                if ((DateTime.Now - startTime).TotalSeconds > 10)
                {
                    Debug.Log("In 10 seconds " + frameCount + " frames were proccessed (" + (frameCount / 10.0) + "FPS)");
                    DestroyAllChildrenOf(game.gameObject);
                    testStage++;
                }
            }
            else if (testStage == 9)
            {
                Debug.Log("Creating " + asteroidCount + " Ash Asteroids using update every 10 frames");

                for (int i = 0; i < asteroidCount; i++)
                {
                    CreateAsteroid(ashAsteroidPrefab, game.transform)
                        .GetComponent<Entity>()
                        .updateFrequency = UpdateFrequency.Every10Frames;
                }

                startTime = DateTime.Now;
                frameCount = 0;
                testStage++;
            }
            else if (testStage == 10)
            {
                frameCount++;
                if ((DateTime.Now - startTime).TotalSeconds > 10)
                {
                    Debug.Log("In 10 seconds " + frameCount + " frames were proccessed (" + (frameCount / 10.0) + "FPS)");
                    DestroyAllChildrenOf(game.gameObject);
                    testStage++;
                }
            }
            else if (testStage == 11)
            {
                Debug.Log("Creating " + asteroidCount + " Ash Asteroids using update never");

                for (int i = 0; i < asteroidCount; i++)
                {
                    CreateAsteroid(ashAsteroidPrefab, game.transform)
                        .GetComponent<Entity>()
                        .updateFrequency = UpdateFrequency.Never;
                }

                startTime = DateTime.Now;
                frameCount = 0;
                testStage++;
            }
            else if (testStage == 12)
            {
                frameCount++;
                if ((DateTime.Now - startTime).TotalSeconds > 10)
                {
                    Debug.Log("In 10 seconds " + frameCount + " frames were proccessed (" + (frameCount / 10.0) + "FPS)");
                    DestroyAllChildrenOf(game.gameObject);
                    testStage++;
                }
            }
            else if (testStage == 13)
            {
                Debug.Log("Creating " + asteroidCount + " Ash Asteroids using update if component count changes");

                for (int i = 0; i < asteroidCount; i++)
                {
                    CreateAsteroid(ashAsteroidPrefab, game.transform)
                        .GetComponent<Entity>()
                        .updateFrequency = UpdateFrequency.IfComponentCountChanges;
                }

                startTime = DateTime.Now;
                frameCount = 0;
                testStage++;
            }
            else if (testStage == 14)
            {
                frameCount++;
                if ((DateTime.Now - startTime).TotalSeconds > 10)
                {
                    Debug.Log("In 10 seconds " + frameCount + " frames were proccessed (" + (frameCount / 10.0) + "FPS)");
                    DestroyAllChildrenOf(game.gameObject);
                    testStage++;
                }
            }
        }

        private void DestroyAllChildrenOf(GameObject obj)
        {
            while (obj.transform.childCount != 0)
                DestroyImmediate(obj.transform.GetChild(0).gameObject);
        }

        private GameObject CreateAsteroid(GameObject prefab, Transform parent)
        {
            var asteroid = (GameObject)GameObject.Instantiate(prefab);
            asteroid.transform.parent = parent;

            // Randomly position it
            asteroid.transform.position = new Vector2(UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
                            UnityEngine.Random.Range(bounds.min.y, bounds.max.y));

            // Give it a random kick
            var xVel = UnityEngine.Random.Range(-100f, 100f);
            var yVel = UnityEngine.Random.Range(-100f, 100f);
            var torque = UnityEngine.Random.Range(-100f, 100f);

            var rigidBody = asteroid.GetComponent<Rigidbody2D>();
            rigidBody.AddForce(new Vector2(xVel, yVel));
            rigidBody.AddTorque(torque);

            // Run a single update (to init things)
            asteroid.SendMessage("Update");

            return asteroid;
        }
    }
}
