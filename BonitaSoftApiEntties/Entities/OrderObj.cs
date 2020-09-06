using System.Collections.Generic;

namespace BonitaSoftApiEntties.Entities
{
    public class OrderObj
    {
        public string TipoCliente { get; set; }

        public decimal MontoOrden { get; set; }

        public string Destino { get; set; }

        public string OrderId { get; set; }

        public string NombreCliente { get; set; }

        public string ApellidoCliente { get; set; }

        public string Pais { get; set; }

        public string Departamento { get; set; }

        public string Ciudad { get; set; }

        public string Barrio { get; set; }

        public string Direccion { get; set; }

        public string CodigoCiudad { get; set; }

        public string EstadoOrden { get; set; }

        public List<Product> Productos { get; set; }

        public string IdentificadorSolicitud { get; set; }

        public string Correo { get; set; }
    }

    public class Product
    {
        public int ItemId { get; set; }

        public string IdProducto { get; set; }

        public string NombreProducto { get; set; }

        public string NumeroParte { get; set; }

        public decimal Precio { get; set; }

        public int Cantidad { get; set; }
    }
}
