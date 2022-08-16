# Компонент Wind
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
* ``WindLayerMask`` - слой объектов к которым будет применена сила ветра (по умолчанию не реагирует на объекты вообще)
* ``MaximumOfDetectionObjects`` - отвечает за максимальное кол-во объектов к которым может быть применена сила ветра (по умолчанию 10 объектов).
От этого параметра зависит производительность, т.к. он задаёт размера созданой кучи для хранения коллайдеров в памяти (подробнее об этом: [Performance optimization tips: Physics in Unity | Tutorial](https://youtu.be/pTz3LMQpvfA?t=583))

<p align="center">
  <img src="https://user-images.githubusercontent.com/36131441/184854183-437ff2e5-3e7f-4b9f-a8c7-f11b1080f1d4.png" />
</p>

Для изменение позиции, размера и направления ветра используйте трансформацию объекта. Красная линия является направлением ветра (по умолчанию направлена вверх).

<p align="center">
  <img src="https://user-images.githubusercontent.com/36131441/184855958-56c1c1fe-9a3d-4228-a6ab-d64168938d7d.png" />
</p>
