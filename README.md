# Boobstrap(^_^) and async programming
# Task#1:
В классе Bootstrap или Launcher реализовать загрузку игры через асинхронные операции. Все методы должны возвращать Task с результатом или без. Реализовать прогресс бар, показывающий процент загрузки (должно зависить от прогресса операций)
Должны присутствовать операции в таком порядке:
- Загрузка картинки по URL и вставка её в компонет Image
- Асинхронная загрузка ресурса
- Переключение сцены асинхронно

# Выполнение:
- [MainMenuEntryPoint.cs](https://github.com/BashkaCoder/Unity_practice_1/blob/Task1/Assets/Scripts/MainMenuEntryPoint.cs)
- [WebLoader.cs](https://github.com/BashkaCoder/Unity_practice_1/blob/Task1/Assets/Scripts/WebLoader/WebLoader.cs)
- [WebLoaderView.cs](https://github.com/BashkaCoder/Unity_practice_1/blob/Task1/Assets/Scripts/WebLoader/WebLoaderView.cs)
- [ResourceLoader.cs](https://github.com/BashkaCoder/Unity_practice_1/blob/Task1/Assets/Scripts/ResourceLoader/ResourceLoader.cs)
- [ResourceLoaderView.cs](https://github.com/BashkaCoder/Unity_practice_1/blob/Task1/Assets/Scripts/ResourceLoader/ResourceLoaderView.cs)
- [NextSceneLoader.cs](https://github.com/BashkaCoder/Unity_practice_1/blob/Task1/Assets/Scripts/NextSceneLoader/NextSceneLoader.cs)
- [NextSceneLoaderView.cs](https://github.com/BashkaCoder/Unity_practice_1/blob/Task1/Assets/Scripts/NextSceneLoader/NextSceneLoaderView.cs)

# Итог:
Таска была выполнена.

`MainMenuEntryPoint.cs` - бутстрап проекта. Инициализирует и запускает все загрузчики.

Загрузчик `WebLoader` загружает изображение из интернета и передает его в `WebLoaderView`, который меняет поле **sprite** в **_Image_**.
`WebLoaderView` содержит ссылку на `WebLoader` и обновляет визуал по событиям загрузчика.

Загрузчик `ResourceLoader` загружает ресурс(изображение) из папки проекта и передает его в `ResourceLoaderView`, который меняет поле **sprite** в **_Image_**.
`ResourceLoaderView` содержит ссылку на `ResourceLoader` и обновляет визуал по событиям загрузчика.

Загрузчик `NextSceneLoader` загружает сцену. `NextSceneLoaderView` содержит ссылку на `NextSceneLoader` и обновляется по его событиям.
`NextSceneLoaderView` также содержит кнопку перехода на следующую сцену.Когда следующая сцена загружена, кнопка перехода становится активной. По нажатию кнопки активируется следующая сцена.