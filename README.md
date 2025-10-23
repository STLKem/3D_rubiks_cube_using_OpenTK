# RubikCube3D (OpenTK, C#)

A 3D Rubik’s Cube simulation built using **OpenTK** and **C#**.  
This project demonstrates low-level graphics programming concepts such as rendering, texture mapping, matrix transformations, and custom rotation logic for each cube layer.  

It was developed as a solo learning project to better understand 3D rendering pipelines and transformations in OpenGL through OpenTK.

---

## 🧩 Features

- **3D Rubik’s Cube** fully modeled from smaller cube segments (27 total, without center cube).
- **Smooth rotation system** for each face using transformation matrices.
- **Independent face rotations** controlled by keyboard input (`T`, `Y`, `U`, `G`, `H`, `J`).
- **Custom camera system** allowing free movement and rotation around the cube.
- **Dynamic vertex and UV generation** for all cube faces.
- **Efficient rendering pipeline** using VAO, VBO, and EBO buffers.
- **Texture mapping** for cube faces via a single sprite texture.
- **Real-time updates** of vertex data for smooth animation.

---

## 🧠 Technical Overview

**Language & Frameworks:**
- C#
- OpenTK (OpenGL for .NET)
- GLSL shaders

**Main Systems Implemented:**
- `Camera` — handles user view and projection matrices  
- `Chunk` — generates all cube faces, manages rotations and rendering  
- `Block` — defines vertex positions and texture coordinates for a single sub-cube  
- `Shader` — loads and applies GLSL vertex & fragment shaders  
- `VAO`, `VBO`, `EBO` — manage vertex, UV, and index data in GPU memory  
- `Texture` — loads cube face texture and binds it during rendering  

**Key Files:**
- `Game.cs` — main render loop and input handling  
- `Chunk.cs` — cube generation and per-face rotation logic  
- `BlockData.cs` — defines cube geometry and face data  
- `Shader.cs` — manages OpenGL shader compilation  
- `Camera.cs` — handles view and projection matrix  
- `Texture.cs` — loads and binds texture assets  

---

## 🎮 Controls

| Key | Action |
|-----|--------|
| `Y` | Rotate Top face |
| `U` | Rotate Front face |
| `J` | Rotate Back face |
| `T` | Rotate Left face |
| `H` | Rotate Bottom face |
| `G` | Rotate Right face |
| Mouse | Move camera |
| WASD | Move camera position |

---

## 🖼️ Screenshot

![RubikCube3D Preview](111.png)

---

## 🧩 Learning Goals

This project was created to explore:
- Matrix transformations (Model, View, Projection)
- Vertex buffer management
- Real-time object manipulation
- Shader programming (GLSL)
- Texture mapping and lighting
- Efficient cube structure generation and rotation logic

---

