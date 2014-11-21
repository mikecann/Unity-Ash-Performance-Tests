Unity-Ash-Performance-Tests
===========================

Tests a number of critical performance points in the [Unity-Ash](https://github.com/mikecann/Unity-Ash) project.

GetComponents Tests
-------------------

One of the critical performance locations is in Entity's Update. To detect if any components have been added or removed it must first get a list of components from the GameObject. Unity provides a few options to do that, this test runs each to see which performs the best:

![GetComponents test](http://i.imgur.com/MJywxoC.png)

The results are fairly clear, passing in a pre-created to list to GetComponents is the fastest, and thus it has been incorperated into Ash:

```
GetComponents<Component>(componentCache);
```

Runtime Tests
-------------

Its all well and good testing a single line of code over a million iterations but often thats not particularly indicative of real performance so I was interested to see what the actual cost of the Ash Entity may be.

The Runtime tests runs a number of tests when you press the button.

1. First it creates 10,000 GameObjects with an AsteroidController which simulates the way you would normally use Unity. The code in AsteroidController updates the gameobject its attached to.
2. Then it creates 10,000 GameObjects with an Entity which causes the asteroids to be updated by the MovementSystem. 

It then repeats step 2 but for the various UpdateFrequency settings for Entity.

Each step is allowed to run for 10 seconds, the number of frames is counted and the FPS is calculated.

![Runtime tests image](http://i.imgur.com/iNtXYik.jpg)

The results show that Entity does indeed have quite an expensive impact on performance. You can only get about half the framerate than when running without Ash.

If you progressively lower the UpdateFrequency however, the performance increases to the point where if you set UpdateFrequency to None its practically the same framerate.

So the lession is, if your entity doesnt need to have components added or removed at runtime set it's Entity.updateFrequency to Never and dont incur any performance penalty.

It is noted that if we were to merge Enitity and EntityBase together then we could still do dynamic component add and remove which would keep the same performance:

```
var node = (AsteroidNode)asteroids.Head;
node.Entity.AddComponent<MyComponent>();
```