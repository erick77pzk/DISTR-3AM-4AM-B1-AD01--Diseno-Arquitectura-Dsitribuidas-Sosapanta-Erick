# AD01 - Aplicaciones Distribuidas

## Sistema de Microservicios para Comercio y Distribución Multibodega

Proyecto desarrollado para la asignatura **Aplicaciones Distribuidas**, cuyo objetivo es implementar una arquitectura basada en microservicios REST independientes utilizando **ASP.NET Core Web API** y **SQL Server**.

La solución permite administrar el catálogo comercial de productos, controlar el inventario de múltiples bodegas y registrar los pedidos realizados por los clientes.

---

## 1. Objetivo del proyecto

Diseñar e implementar una solución distribuida compuesta por **3 microservicios REST independientes**, donde cada servicio tenga una responsabilidad específica y una base de datos propia.

Los microservicios son:

1. **Catalogo.Api**
2. **Inventario.Api**
3. **Pedidos.Api**

Cada microservicio utiliza su propia base de datos para mantener separadas las responsabilidades de cada dominio de negocio.

---

# 2. Arquitectura del sistema

La arquitectura general del proyecto está organizada de la siguiente manera:

```text
                        CLIENTE / POSTMAN
                               |
                               |
                         Pedidos.Api
                        /           \
                       /             \
                      v               v
              Catalogo.Api       Inventario.Api
                   |                   |
                   v                   v
              CatalogoDB          InventarioDB

                       Pedidos.Api
                            |
                            v
                       PedidosDB
```

Los servicios son independientes y se comunican mediante APIs REST.

### Bases de datos

```text
Catalogo.Api
     |
     └── CatalogoDB

Inventario.Api
     |
     └── InventarioDB

Pedidos.Api
     |
     └── PedidosDB
```

---

# 3. Tecnologías utilizadas

- **C#**
- **ASP.NET Core Web API**
- **.NET 10**
- **Entity Framework Core**
- **SQL Server**
- **Swagger / OpenAPI**
- **Postman**
- **Git / GitHub**
- **Visual Studio**

---

# 4. Microservicio 1 - Catalogo.Api

## Responsabilidad

`Catalogo.Api` administra la información comercial de los productos.

La base de datos asociada es:

```text
CatalogoDB
```

## Tablas

### Categoria

```text
IdCategoria
Nombre
Descripcion
```

### Marca

```text
IdMarca
Nombre
PaisOrigen
```

### Producto

```text
IdProducto
IdCategoria
IdMarca
Nombre
SKU
Precio
Estado
```

## Relaciones

```text
Categoria
    |
    | 1
    |
    | N
Producto
    |
    | N
    |
    | 1
Marca
```

Un producto pertenece a una categoría y a una marca.

## Endpoints principales

### Categorías

```http
GET    /api/Categorias
GET    /api/Categorias/{id}
POST   /api/Categorias
PUT    /api/Categorias/{id}
DELETE /api/Categorias/{id}
```

### Marcas

```http
GET    /api/Marcas
GET    /api/Marcas/{id}
POST   /api/Marcas
PUT    /api/Marcas/{id}
DELETE /api/Marcas/{id}
```

### Productos

```http
GET    /api/Productos
GET    /api/Productos/{id}
POST   /api/Productos
PUT    /api/Productos/{id}
DELETE /api/Productos/{id}
```

---

# 5. Microservicio 2 - Inventario.Api

## Responsabilidad

`Inventario.Api` administra el almacenamiento y los movimientos físicos de las mercancías.

La base de datos asociada es:

```text
InventarioDB
```

## Tablas

### Bodega

```text
IdBodega
Nombre
Ciudad
Direccion
```

### Existencia

```text
IdExistencia
IdProducto
IdBodega
Stock
```

### MovimientoInventario

```text
IdMovimiento
IdProducto
IdBodega
Tipo
Cantidad
Fecha
```

### ReservaStock

```text
IdReserva
IdProducto
IdBodega
Cantidad
Estado
```

## Relaciones

```text
Bodega
   |
   ├── Existencia
   |
   ├── MovimientoInventario
   |
   └── ReservaStock
```

`IdProducto` funciona como referencia lógica hacia el producto administrado por `Catalogo.Api`.

---

# 6. Microservicio 3 - Pedidos.Api

## Responsabilidad

`Pedidos.Api` registra y administra las órdenes de compra realizadas por los clientes.

La base de datos asociada es:

```text
PedidosDB
```

## Tablas

### Cliente

```text
IdCliente
Nombres
Cedula
Correo
```

### Pedido

```text
IdPedido
IdCliente
Fecha
Total
Estado
```

### DetallePedido

```text
IdDetalle
IdPedido
IdProducto
Cantidad
PrecioUnitario
```

## Relaciones

```text
Cliente
   |
   | 1
   |
   | N
Pedido
   |
   | 1
   |
   | N
DetallePedido
```

`IdProducto` representa una referencia lógica hacia `Catalogo.Api`.

---

# 7. Referencias entre microservicios

Una característica importante de la arquitectura es que **cada microservicio mantiene su propia base de datos**.

Por esta razón, no se deben crear FK físicas entre bases de datos pertenecientes a diferentes microservicios.

Por ejemplo:

```text
PedidosDB
   |
   └── DetallePedido.IdProducto
                |
                └── referencia lógica
                        |
                        v
                   Catalogo.Api
                        |
                        v
                  CatalogoDB.Producto
```

De forma similar:

```text
InventarioDB
     |
     └── Existencia.IdProducto
                 |
                 └── referencia lógica
                         |
                         v
                    Catalogo.Api
```

Las FK físicas se utilizan principalmente entre tablas que pertenecen al mismo dominio y a la misma base de datos.

---

# 8. Estructura del proyecto

La solución se organiza de la siguiente manera:

```text
AD01_Microservicios/
│
├── Catalogo.Api/
│   ├── Controllers/
│   │   ├── CategoriasController.cs
│   │   ├── MarcasController.cs
│   │   └── ProductosController.cs
│   │
│   ├── Data/
│   │   └── CatalogoDbContext.cs
│   │
│   ├── Models/
│   │   ├── Categoria.cs
│   │   ├── Marca.cs
│   │   └── Producto.cs
│   │
│   ├── appsettings.json
│   └── Program.cs
│
├── Inventario.Api/
│   ├── Controllers/
│   ├── Data/
│   ├── Models/
│   ├── appsettings.json
│   └── Program.cs
│
├── Pedidos.Api/
│   ├── Controllers/
│   ├── Data/
│   ├── Models/
│   ├── appsettings.json
│   └── Program.cs
│
├── README.md
└── .gitignore
```

---

# 9. Configuración de SQL Server

Cada microservicio debe utilizar una cadena de conexión correspondiente a su propia base de datos.

### Catalogo.Api

```json
{
  "ConnectionStrings": {
    "CatalogoConnection": "Server=localhost\\SQLEXPRESS;Database=CatalogoDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### Inventario.Api

```json
{
  "ConnectionStrings": {
    "InventarioConnection": "Server=localhost\\SQLEXPRESS;Database=InventarioDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### Pedidos.Api

```json
{
  "ConnectionStrings": {
    "PedidosConnection": "Server=localhost\\SQLEXPRESS;Database=PedidosDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

> Si la instancia de SQL Server utilizada es diferente, por ejemplo `localhost\WINCC`, se debe reemplazar `localhost\SQLEXPRESS` por la instancia correspondiente.

---

# 10. Entity Framework Core

El acceso a SQL Server se realiza mediante:

```text
Entity Framework Core
```

Cada microservicio dispone de su propio `DbContext`.

```text
Catalogo.Api
     |
     └── CatalogoDbContext
             |
             └── CatalogoDB

Inventario.Api
     |
     └── InventarioDbContext
             |
             └── InventarioDB

Pedidos.Api
     |
     └── PedidosDbContext
             |
             └── PedidosDB
```

Esto permite mantener separada la información de cada dominio.

---

# 11. Validaciones

Los microservicios deben validar los datos recibidos antes de almacenarlos.

Entre las validaciones consideradas se encuentran:

- Campos obligatorios.
- Longitud máxima de textos.
- Precios mayores o iguales a cero.
- Cantidades válidas.
- Existencia de categorías.
- Existencia de marcas.
- Existencia de clientes.
- Existencia de pedidos.
- Verificación de IDs.
- Control de productos inexistentes.

Ejemplo:

```text
POST /api/Productos
```

Antes de crear el producto se verifica que:

```text
IdCategoria exista
IdMarca exista
SKU no esté repetido
Precio sea válido
Nombre sea válido
```

---

# 12. Códigos HTTP utilizados

La API utiliza respuestas HTTP apropiadas de acuerdo con el resultado de cada operación.

| Código | Significado |
|---|---|
| 200 | Operación exitosa |
| 201 | Recurso creado |
| 204 | Operación exitosa sin contenido |
| 400 | Datos enviados incorrectamente |
| 404 | Recurso no encontrado |
| 500 | Error interno del servidor |

Ejemplo de creación exitosa:

```text
HTTP 201 Created
```

Ejemplo cuando no existe un recurso:

```text
HTTP 404 Not Found
```

---

# 13. Swagger

Cada microservicio incorpora Swagger/OpenAPI para facilitar la visualización y prueba de los endpoints.

Ejemplo:

```text
http://localhost:5201/swagger
```

Desde Swagger se pueden probar operaciones como:

```text
GET
POST
PUT
DELETE
```

---

# 14. Pruebas con Postman

Las pruebas de los microservicios se realizan mediante Postman.

Se deben probar principalmente:

### GET

Consultar registros:

```http
GET /api/Categorias
GET /api/Marcas
GET /api/Productos
```

### POST

Crear registros:

```http
POST /api/Categorias
POST /api/Marcas
POST /api/Productos
```

### PUT

Actualizar registros:

```http
PUT /api/Categorias/1
PUT /api/Marcas/1
PUT /api/Productos/1
```

### DELETE

Eliminar registros:

```http
DELETE /api/Categorias/1
DELETE /api/Marcas/1
DELETE /api/Productos/1
```

Las pruebas deben comprobar tanto respuestas exitosas como respuestas de error.

---

# 15. Ejemplo de JSON - Categoría

```json
{
  "nombre": "Laptops",
  "descripcion": "Computadores portátiles"
}
```

---

# 16. Ejemplo de JSON - Marca

```json
{
  "nombre": "Lenovo",
  "paisOrigen": "China"
}
```

---

# 17. Ejemplo de JSON - Producto

```json
{
  "idCategoria": 1,
  "idMarca": 1,
  "nombre": "Laptop Lenovo IdeaPad",
  "sku": "LEN-001",
  "precio": 850.00,
  "estado": true
}
```

El `IdCategoria` y el `IdMarca` deben corresponder a registros existentes.

---

# 18. Ejecución del proyecto

Para ejecutar el proyecto:

1. Abrir la solución en **Visual Studio**.
2. Verificar que SQL Server esté iniciado.
3. Comprobar las cadenas de conexión.
4. Comprobar que las tres bases de datos existan.
5. Compilar la solución.
6. Ejecutar los microservicios.
7. Abrir Swagger.
8. Realizar las pruebas con Postman.

Los tres servicios deben funcionar independientemente.

---

# 19. GitHub

El proyecto debe mantenerse bajo control de versiones utilizando Git y GitHub.

No se deben subir archivos generados automáticamente por Visual Studio y .NET.

Entre los archivos que deben excluirse se encuentran:

```text
bin/
obj/
.vs/
```

Para esto se utiliza el archivo:

```text
.gitignore
```

---

# 20. Buenas prácticas utilizadas

El proyecto aplica las siguientes prácticas:

- Separación de responsabilidades.
- Un microservicio por dominio.
- Una base de datos por microservicio.
- Uso de Entity Framework Core.
- Validación de información.
- Uso adecuado de códigos HTTP.
- Control de errores.
- Uso de Swagger.
- Pruebas mediante Postman.
- Control de versiones con Git.
- Separación entre Models, Data y Controllers.

---

# 21. Resultado esperado

Al finalizar la implementación se debe disponer de:

```text
                  SISTEMA DISTRIBUIDO
                         |
        ┌────────────────┼────────────────┐
        │                │                │
        ▼                ▼                ▼
   Catalogo.Api     Inventario.Api    Pedidos.Api
        │                │                │
        ▼                ▼                ▼
   CatalogoDB       InventarioDB       PedidosDB
```

Cada servicio administra exclusivamente la información correspondiente a su dominio.

El sistema permite:

- Administrar categorías.
- Administrar marcas.
- Administrar productos.
- Administrar bodegas.
- Controlar existencias.
- Registrar movimientos de inventario.
- Reservar stock.
- Registrar clientes.
- Registrar pedidos.
- Registrar detalles de pedidos.
- Realizar operaciones CRUD mediante APIs REST.
- Validar información.
- Probar los servicios mediante Swagger y Postman.

---

# 22. Conclusión

La solución implementa una arquitectura distribuida basada en tres microservicios independientes. La separación de responsabilidades permite que el catálogo, el inventario y los pedidos puedan evolucionar de manera independiente.

La utilización de bases de datos independientes permite mantener separados los dominios de negocio y utilizar referencias lógicas cuando un servicio necesita identificar información administrada por otro servicio.

De esta manera se cumple la propuesta principal de la actividad: desarrollar una solución de **microservicios REST con ASP.NET Core Web API y SQL Server**, aplicando operaciones sobre datos, validaciones y pruebas mediante Postman.

Autor Erick Sosapanta