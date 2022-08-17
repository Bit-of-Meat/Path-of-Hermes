# Компонент Wind [[исходный код](https://github.com/Bit-of-Meat/Path-of-Hermes/blob/master/Assets/Scripts/Physics/Wind.cs)]
## Добавление на сцену
Для начала необходимо создать пустой GameObject.

<p align="center">
  <img src="https://user-images.githubusercontent.com/36131441/184846025-ff3d4d7a-93b4-4ab7-a1ef-7e38315d3e43.png" />
</p>

Теперь добавим на него компонент ``Wind``.

<p align="center">
  <img src="https://user-images.githubusercontent.com/36131441/184847239-b06063e5-d862-461b-a11e-ca37b9762657.png" />
</p>

## Настройка ветра
### Параметры
* ``Strength`` - отвечает за силу ветра (по умолчанию 20 метров)
* ``WindLayerMask`` - слой объектов к которым будет применена сила ветра (по умолчанию ``nothing`` - ни один слой не указан)
* ``MaximumOfDetectionObjects`` - отвечает за максимальное кол-во объектов к которым может быть применена сила ветра (по умолчанию 10 объектов).
От этого параметра зависит производительность, т.к. он задаёт размера созданой кучи для хранения коллайдеров в памяти (подробнее об этом: [Performance optimization tips: Physics in Unity | Tutorial](https://youtu.be/pTz3LMQpvfA?t=583))

<p align="center">
  <img src="https://user-images.githubusercontent.com/36131441/185073962-1f2faa29-46a1-4625-ab0d-091bb232ce3e.png" />
</p>

Для изменение позиции, размера и направления ветра используйте трансформацию объекта. Красная стрелка является направлением ветра (по умолчанию направлена вверх).

<p align="center">
  <img src="https://user-images.githubusercontent.com/36131441/185073696-a478bbd3-9452-4f4d-a798-5bb07cb76f05.png" />
</p>