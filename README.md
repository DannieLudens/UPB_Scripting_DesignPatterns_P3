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
| Factory    | Cada tipo de objeto (cubo, esfera, cápsula) tiene su propia clase fábrica |
| Facade     | Una clase intermediaria (`ShapeFacade`) maneja la lógica de creación    |

---

### 🖱️ Interacción del usuario

1. El usuario selecciona el tipo de objeto mediante un botón (que se resalta en amarillo)
2. Al hacer clic en **"Instanciar"**, el objeto es creado usando la fábrica correspondiente
3. El objeto aparece en una posición **aleatoria visible dentro de la cámara**, sin encimarse

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

