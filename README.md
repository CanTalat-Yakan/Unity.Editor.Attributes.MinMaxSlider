# Unity Essentials

This module is part of the Unity Essentials ecosystem and follows the same lightweight, editor-first approach.
Unity Essentials is a lightweight, modular set of editor utilities and helpers that streamline Unity development. It focuses on clean, dependency-free tools that work well together.

All utilities are under the `UnityEssentials` namespace.

```csharp
using UnityEssentials;
```

## Installation

Install the Unity Essentials entry package via Unity's Package Manager, then install modules from the Tools menu.

- Add the entry package (via Git URL)
    - Window → Package Manager
    - "+" → "Add package from git URL…"
    - Paste: `https://github.com/CanTalat-Yakan/UnityEssentials.git`

- Install or update Unity Essentials packages
    - Tools → Install & Update UnityEssentials
    - Install all or select individual modules; run again anytime to update

---

# Min Max Slider Attribute

> Quick overview: Draw a single slider that edits a min/max pair. Works with `Vector2` (float) and `Vector2Int` (int), with inline numeric fields, clamping, and integer rounding where appropriate.

A compact inspector control for ranges. Add `[MinMaxSlider(min, max)]` to a `Vector2` or `Vector2Int` field to get one control with two numeric fields and a draggable min–max slider between them. Values are clamped to the specified limits and the min will never exceed the max.

![screenshot](Documentation/Screenshot.png)

## Features
- One control for ranges: two numeric fields + a min–max slider
- Supports `Vector2` (float) and `Vector2Int` (int)
- Clamps values to `[min, max]` and enforces `min <= max`
- Integer mode: slider rounds to whole numbers for `Vector2Int`
- Editor-only; zero runtime overhead

## Requirements
- Unity Editor 6000.0+ (Editor-only; attribute lives in Runtime for convenience)
- No external dependencies

Tip: Choose sensible slider bounds that match your use case (e.g., 0–1 for normalized ranges, 0–100 for percentages).

## Usage
Float range (Vector2)

```csharp
using UnityEngine;
using UnityEssentials;

public class Movement : MonoBehaviour
{
    // x = min speed, y = max speed; clamped to [0, 20]
    [MinMaxSlider(0f, 20f)]
    public Vector2 speedRange = new Vector2(2f, 8f);
}
```

Integer range (Vector2Int)

```csharp
public class Spawner : MonoBehaviour
{
    // x = min count, y = max count; clamped to [0, 100], slider snaps to ints
    [MinMaxSlider(0, 100)]
    public Vector2Int countRange = new Vector2Int(1, 5);
}
```

Normalized ranges

```csharp
public class AudioSettings : MonoBehaviour
{
    // x = min volume, y = max volume; clamped to [0, 1]
    [MinMaxSlider(0f, 1f)]
    public Vector2 volumeWindow = new Vector2(0.1f, 0.9f);
}
```

## How It Works
- The property drawer supports `Vector2` and `Vector2Int` only
- UI layout: Label | Min field | [Min–Max Slider] | Max field
- For float ranges, values are edited as floats; for int ranges, slider values are rounded to ints
- After slider/field edits, values are clamped: `min = Clamp(min, attr.Min, max)`, `max = Clamp(max, min, attr.Max)`

## Notes and Limitations
- Supported field types: `Vector2`, `Vector2Int` only; other types show an inline error
- Attribute values: ensure `min <= max` in the attribute usage for predictable behavior
- The attribute’s bounds are static (compile-time constants), not data-driven at runtime
- Multi-object editing: values are applied per-inspected object as with standard fields

## Files in This Package
- `Runtime/MinMaxSliderAttribute.cs` – `[MinMaxSlider(min, max)]` attribute
- `Editor/MinMaxSliderEditor.cs` – PropertyDrawer (float/int handling, rounding, clamping, layout)
- `Runtime/UnityEssentials.MinMaxSliderAttribute.asmdef` – Runtime assembly definition
- `Editor/UnityEssentials.MinMaxSliderAttribute.Editor.asmdef` – Editor assembly definition

## Tags
unity, unity-editor, attribute, propertydrawer, slider, minmax, range, vector2, vector2int, inspector, ui, tools, workflow
