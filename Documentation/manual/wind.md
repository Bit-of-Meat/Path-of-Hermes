# Wind Component [[source code](https://github.com/Bit-of-Meat/Path-of-Hermes/blob/master/Assets/Scripts/Physics/Wind.cs)]
[Russian](../translate/ru/guide/wind.md)
## Adding to the scene
To start you need to create empty GameObject.

<p align="center">
  <img src="https://user-images.githubusercontent.com/36131441/184846025-ff3d4d7a-93b4-4ab7-a1ef-7e38315d3e43.png" />
</p>

Now let's add to it ``Wind`` component.

<p align="center">
  <img src="https://user-images.githubusercontent.com/36131441/184847239-b06063e5-d862-461b-a11e-ca37b9762657.png" />
</p>

## Wind settings
### Variables
* ``Strength`` - wind force (default: 20 meters)
* ``WindLayerMask`` - layer mask of objects to which the wind force will be applied (default: ``nothing`` - no layer specified)
* ``MaximumOfDetectionObjects`` - is responsible for the maximum number of objects to which the force of the wind can be applied (default: 10 objects).
Performance depends on this parameter, because it sets the size of the created heap for storing colliders in memory (more about this: [Performance optimization tips: Physics in Unity | Tutorial](https://youtu.be/pTz3LMQpvfA?t=583))

<p align="center">
  <img src="https://user-images.githubusercontent.com/36131441/185073962-1f2faa29-46a1-4625-ab0d-091bb232ce3e.png" />
</p>

To change position, size and direction of the wind force use ``transform``. red arrow points wind direction (by default points up).

<p align="center">
  <img src="https://user-images.githubusercontent.com/36131441/185073696-a478bbd3-9452-4f4d-a798-5bb07cb76f05.png" />
</p>