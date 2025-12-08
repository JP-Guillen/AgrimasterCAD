# **AgrimasterCAD – Sistema de Gestión de Solicitudes de Agrimensura**

### Proyecto Final – Programación Aplicada I (AP1)

## **Descripción del Proyecto (Mini Manual)**

AgrimasterCAD es una plataforma desarrollada en **Blazor Server (.NET 10)** que permite gestionar todo el proceso de solicitudes de agrimensura entre **Clientes**, **Agrimensores** y **Administradores**.

El sistema incluye:

### Cliente

* Crear solicitudes de levantamiento.
* Subir documentos (PDF).
* Consultar el estado de cada solicitud.
* Realizar pagos (tarjeta, efectivo o transferencia).
* Ver comprobantes, documentos y plano final.
* Recibir notificaciones.

### Agrimensor

* Aceptar solicitudes asignadas o pendientes.
* Registrar costos y detalles del proceso.
* Subir el plano final.
* Actualizar estados de la solicitud.
* Crear seguimientos.
* Notificar al cliente automáticamente.

### Administrador

* Gestionar clientes y agrimensores.
* Editar, reasignar y cancelar solicitudes.
* Revisar y aprobar pagos.
* Generar comprobantes de pago.
* Ver un Dashboard con estadísticas generales.

---

## **Estructura General**

* `Models/` – Entidades principales del sistema
* `Services/` – Lógica de datos (CRUD + validaciones)
* `Pages/Cliente/` – Módulo del Cliente
* `Pages/Agrimensor/` – Módulo del Agrimensor
* `Pages/Admin/` – Módulo del Administrador
* `wwwroot/` – Recursos estáticos

---

## **Usuarios de Prueba**

**Administrador**

```
Usuario: admin@agrimaster.com
Clave: Admin123*
```

**Agrimensor**

```
Usuario: fatimaguillen@gmail.com
Clave: Fatima123*
```

**Cliente**

```
Usuario: juanguillen@gmail.com
Clave: Juan123(
```

---

## **Entidades Principales**

* Solicitudes (Master)
* SolicitudDocumentos (Detail)
* SolicitudSeguimientos (Detail)
* Pagos
* ComprobantesPago
* Planos
* Notificaciones
* MetodosPago
* Usuarios de Identity

## **Colaboradores**
* James Jesús De Peña Rodríguez
* Juan Pablo Guillen Zorrilla
