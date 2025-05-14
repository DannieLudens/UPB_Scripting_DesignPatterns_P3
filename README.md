# UPB_Scripting_DesignPatterns_P3

# 🎮 Taller/Parcial 3 – Scripting 

**Materia**: Scripting  
**Universidad**: Universidad Pontificia Bolivariana  
**Profesor**: Andrés Pérez Campillo   
**Estudiante**: Daniel Esteban Ardila Alzate
**Motor utilizado**: Unity 6 (6000.0.34f1)

---

## 🧠 Descripción general

Este proyecto corresponde al **Taller práctico 3** con el objetivo de implementar distintos **patrones de diseño de software** dentro del motor de videojuegos Unity

Cada ejercicio plantea una situación diferente en la que se aplican conceptos de clase como:

- **Factory Pattern**
- **Facade Pattern**
- **Event Delegates**
- **Object Pooling**
- **Programación desacoplada y modulr**

La solución a cada ejercicio está implementada como una **escena individual**, y existe una escena maestra para navegar entre ellas en un ejecutable.

---

## ✅ Escena 1 - Ejercicio 1 – Instanciación con Factory y Facade

### 🎯 Objetivo

Permitir seleccionar uno de tres tipos de objetos (`Cubo`, `Esfera`, `Cápsula`) mediante botones, y luego instanciar el objeto seleccionado. Debe usarse el patrón **Factory** para crear los objetos y una **Facade** para centralizar la lógica de instanciación

---

### ⚙️ Patrones de diseño implementados

| Patrón     | Descripción                                                                 |
|------------|------------------------------------------------------------------------------|
| Factory (patrón de creacion)   | Cada tipo de objeto (cubo, esfera, cápsula) tiene su propia clase fábrica |
| Facade  (patrón estructural)   | Una clase intermediaria (`ShapeFacade`) maneja la lógica de creación    |

---

### 🖱️ Interacción del usuario

1. Seleccionar el tipo de objeto mediante un botón (que se resalta en amarillo al ser presionado)
2. Al hacer clic en **"Instanciar"**, el objeto es creado usando la fábrica correspondiente
3. plus el objeto aparece en una posición **aleatoria visible dentro de la cámara**

---

### 📂 Estructura del ejerciio

```

Assets/
├── Prefabs/
│   ├── RedCube.prefab
│   ├── GreenSphere.prefab
│   └── BlueCapsule.prefab
├── Scenes/
│   ├── FactoryScene.unity
├── Scripts/
│   ├── IShapeFactory.cs
│   ├── CubeFactory.cs
│   ├── SphereFactory.cs
│   ├── CapsuleFactory.cs
│   ├── ShapeFacade.cs
│   ├── ShapeManager.cs
│   └── ButtonSelector.cs

```

---

### 🧠 Scripts explicados

#### 🔹 `IShapeFactory.cs` Clase Interfaz

Define la interfaz común para todas las fábricas de objetos. Establece un contrato con el método `CreateShape()` que cada fábrica debe implementar

```csharp
public interface IShapeFactory {
    GameObject CreateShape();
}
```
---

#### 🔹 `CubeFactory.cs`, `SphereFactory.cs`, `CapsuleFactory.cs`

Cada uno de estos scripts implementa la interfaz `IShapeFactory` y contiene una referencia pública a su prefab correspondiente. Su único propósito es instanciar dicho prefab cuando se llama al método `CreateShape()`

```csharp
public class CubeFactory : IShapeFactory
{
    public GameObject prefab;

    public GameObject CreateShape()
    {
        return GameObject.Instantiate(prefab);
    }
}
```

---

#### 🔹 `ShapeFacade.cs`

Esta clase representa el patrón **Facade**, al centralizar la lógica de selección de fábrica y delegar la creación del objeto. Permite cambiar fácilmente de tipo de objeto sin que el resto del sistema sepa cómo está implementada cada fábrica.

```csharp
public class ShapeFacade
{
    private IShapeFactory currentFactory;

    public void SetFactory(IShapeFactory factory)
    {
        currentFactory = factory;
    }

    public GameObject CreateShape()
    {
        return currentFactory?.CreateShape();
    }
}
```

---

#### 🔹 `ShapeManager.cs`

Este script se comunica directamente con los botones de la interfaz. Controla:

* Qué tipo de fábrica está activa
* La creación del objeto seleccionado
* La posición aleatoria en la que aparece dicho objeto

Incluye métodos como:

```csharp
public void SetCubeFactory() { ... }
public void SetSphereFactory() { ... }
public void SetCapsuleFactory() { ... }

public void CreateObject()
{
    Vector3 pos = new Vector3(Random.Range(-4f, 4f), Random.Range(-2f, 2f), 0f);
    GameObject obj = facade.CreateShape();
    obj.transform.position = pos;
}
```

---

#### 🔹 `ButtonSelector.cs` 

Script encargado de la interfaz visual. Gestiona el color de los botones para indicar cuál está seleccionado. Cambia directamente el color a amarillo cuando se selecciona un boton

```csharp
public void SelectButton(Button selected)
{
    foreach (Button btn in buttons)
    {
        Image image = btn.GetComponent<Image>();
        image.color = (btn == selected) ? selectedColor : normalColor;
    }
}
```

---

## ✅ Ejercicio 2 – Eventos y cambio de color con clic

### 🎯 Objetivo

Detectar clics izquierdos del mouse y transmitir un número cíclico del 1 al 4 a través de un evento personalizado. Un objeto(cubo) en la escena cambia su color dependiendo del número recibido, y otro componente imprime ese número en consola.

---

### 🧪 Interacción del usuario

1. clic izquierdo en cualquier parte de la pantalla
2. Se lanza un evento con un número que va del 1 al 4 (de forma cíclica)
3. Un cubo cambia su color dependiendo del número recibido
4. Se imprime ese número en la consola y en pantalla

---

### ⚙️ Patrones de diseño implementados

| Patrón  | Descripción |
|---------|-----|
| Observer (Patrón de comportamiento) | Utiliza eventos (`Action<int>`) para notificar a múltiples suscriptores sobre un cambio de estado (clic del mouse), permitiendo desacoplamiento entre emisores y receptores. |

---

### 📂 Estructura del proyecto

```

Assets/
├── Scenes/
│ └── EventScene.unity
├── Scripts/
│ ├── ClickBroadcaster.cs
│ ├── ColorChanger.cs
│ ├── NumberDisplay.cs
│ └── NumberLogger.cs

```

---

### 🧠 Scripts explicados

#### 🔹 `ClickBroadcaster.cs`

Este script detecta el clic izquierdo y lanza un evento global llamado `OnButtonClicked` que transmite un número entre 1 y 4, de forma cíclica.

```csharp
public static event Action<int> OnButtonClicked;

void Update()
{
    if (Input.GetMouseButtonDown(0)) {
        counter++;
        int value = (counter - 1) % 4 + 1;
        OnButtonClicked?.Invoke(value);
    }
}
````

---

#### 🔹 `ColorChanger.cs`

Este script va en el cubo que cambia de color. Se suscribe al evento emitido por `ClickBroadcaster` y actualiza el color del material del objeto según el número recibido.

```csharp
void ChangeColor(int value)
{
    switch (value)
    {
        case 1: rend.material.color = Color.red;
            break;
        case 2: rend.material.color = Color.green;
            break;
        case 3: rend.material.color = Color.blue;
            break;
        case 4: rend.material.color = Color.yellow;
            break;
    }
}
```

---

#### 🔹 `NumberLogger.cs` y `NumberDisplay.cs`

Script que simplemente escucha el evento y **escribe el número actual en la consola** con `Debug.Log`. y NumberDisplay suscrito tambien muestra el numero en pantalla. Se usa para demostrar que múltiples componentes pueden responder al mismo evento.

```csharp
void LogNumber(int value)
{
    Debug.Log("Número actual: " + value);
}
```

---

## ✅ Ejercicio 3 – Proyectiles y Object Pooling

### 🎯 Objetivo

Simular una mecánica de disparo con tres tipos de proyectiles, cada uno con un comportamiento distinto al impactar. Se debe usar pooling para optimizar el rendimiento.

---

### ⚙️ Patrones y conceptos aplicados

| Elemento         | Descripción                                                                 |
|------------------|-----------------------------------------------------------------------------|
| Object Pooling   | Cada tipo de proyectil se recicla mediante un pool especializado.          |
| Herencia         | Los pools heredan de una clase abstracta común.                            |
| Colisiones       | Al detectar una colisión, se dispara el comportamiento propio del proyectil.|
| Eventos          | Se usa un evento para bloquear el disparo en ciertos casos.                |

---

### 🧪 Comportamiento de los proyectiles

| Tipo      | Comportamiento al colisionar                                                   |
|-----------|----------------------------------------------------------------------------------|
| Proyectil 1 | Imprime un mensaje en la consola.                                             |
| Proyectil 2 | Desactiva el collider y bloquea disparo durante 1 segundo.                   |
| Proyectil 3 | Instancia un efecto de partículas (que también se recicla).                   |

---

### 📂 Estructura del proyecto

```

Assets/
├── Scenes/
│   └── ProjectileScene.unity
├── Prefabs/
│   ├── Projectile1.prefab
│   ├── Projectile2.prefab
│   ├── Projectile3.prefab
│   └── ImpactParticle.prefab
├── Scripts/
│   ├── BaseProjectile.cs
│   ├── ProjectileType1.cs
│   ├── ProjectileType2.cs
│   ├── ProjectileType3.cs
│   ├── AbstractPool.cs
│   ├── ProjectilePool.cs
│   ├── ProjectileShooter.cs

```

---


