# Boobstrap(^_^) and async programming
# Task#2:
Реализовать программную анимацию прогрессбара, которая возвращает UniTask.
Вызывающий код дожидается завершения операции и производит бизнес-логику(сокрытие панели + показ текста) после завершения.
Если пользователь нажмёт на кнопку отмены три раза - анимация моментально завершается, завершается асинхронная операция и анимация ставится в конечное состояние.

# Выполнение:
- [MainMenuEntryPoint.cs](https://github.com/BashkaCoder/Unity_practice_1/blob/Task2/Assets/Scripts/MainMenuEntryPoint.cs)
- [LoadingOperation.cs](https://github.com/BashkaCoder/Unity_practice_1/blob/Task2/Assets/Scripts/LoadingOperation.cs)
- [LoadingOperationView.cs](https://github.com/BashkaCoder/Unity_practice_1/blob/Task2/Assets/Scripts/LoadingOperationView.cs)
- [QuitButton.cs](https://github.com/BashkaCoder/Unity_practice_1/blob/Task2/Assets/Scripts/QuitButton.cs)

# Итог:
Таска была выполнена.

`MainMenuEntryPoint.cs` содержит ссылку на `LoadingOperationView.cs` и в `Start()` запускает операцию загрузки.

`LoadingOperationView.cs` наследуется от **_MonoBehaviour_** и содержит ссылку на `LoadingOperation.cs`. Скрипт пробрасывает в `LoadingOperation.cs` запуск операции загрузки от `MainMenuEntryPoint.cs`

Ассинхронщина сделана на [UniTask](https://github.com/Cysharp/UniTask), из-за этого имеем ряд преимуществ:
* Отсутствие лишних аллокаций
* Отсутствие ошибок при обращении к объектам, которые "умирают" в жизненном цикле **_MonoBehaviour_**
* Отсутствие крашей Unity ввиду вышеуказанног пункта
* Присутсвует возможность ожидания [AsyncOperation](https://docs.unity3d.com/ScriptReference/AsyncOperation.html) "из коробки"