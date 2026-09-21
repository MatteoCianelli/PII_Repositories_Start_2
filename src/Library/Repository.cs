using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace Ucu.Poo.Repositories
{
   /// <summary>
   /// Esta clase representa un repositorio de T.
   /// </summary>
   /// <typeparam name="T">IItem.</typeparam>
    public class Repository<T>
    where T : class, IItem
    {
        private List<T> items = new List<T>();

        /// <summary>
        /// Obtiene la lista de autos en la base de datos.
        /// </summary>
        public ReadOnlyCollection<T> Items
        {
            get { return this.items.AsReadOnly(); }
        }

        /// <summary>
        /// Agrega un auto a la base de datos.
        /// </summary>
        /// <param name="item">El auto a agregar.</param>
        public void Add(T item)
        {
            if (item != null)
            {
                this.items.Add(item);
            }
        }

        /// <summary>
        /// Elimina un auto de la base de datos.
        /// </summary>
        /// <param name="item">El auto a remover.</param>
        public void Remove(T item)
        {
            this.items.Remove(item);
        }

        /// <summary>
        /// Busca un auto en la base de datos que cumpla con un criterio
        /// específico.
        /// </summary>
        /// <param name="field">El nombre del atributo por el cual
        /// buscar.</param>
        /// <param name="value">El valor del atributo por el cual
        /// buscar.</param>
        /// <returns>El auto encontrado que cumple el criterio especificado o
        /// null si no se encuentra ninguno.</returns>
        public T Find(string field, string value)
        {
            foreach (T item in this.items)
            {
                if (item.HasValue(field, value))
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// Convierte la base de datos de autos a una representación en formato
        /// JSON.
        /// </summary>
        /// <returns>Una representación de la base de datos en formato
        /// JSON.</returns>
        public string ConvertToJson()
        {
            return JsonSerializer.Serialize(this.items);
        }

        /// <summary>
        /// Carga la base de datos de autos desde una representación en formato
        /// JSON.
        /// </summary>
        /// <param name="content">La representación en formato JSON desde la
        /// cual cargar la base de datos.</param>
        public void LoadFromJson(string content)
        {
                List<T> items = JsonSerializer.Deserialize<List<T>>(content);
                if (items != null)
                {
                    this.items = items;
                }
                else
                {
                    this.items = new List<T>();
                }
        }

        /// <summary>
        /// Guarda la base de datos de autos en un archivo en formato JSON.
        /// </summary>
        /// <param name="filePath">El nombre del archivo, incluyendo
        /// opcionalmente la ruta.</param>
        public void SaveToFile(string filePath)
        {
            string content = this.ConvertToJson();
            File.WriteAllText(filePath, content);
        }

        /// <summary>
        /// Carga la base de datos de autos desde un archivo en formato JSON.
        /// </summary>
        /// <param name="filePath">El nombre del archivo, incluyendo
        /// opcionalmente la ruta.</param>
        /// <returns>Retorna <c>true</c> si se cargó la base de datos y
        /// <c>false</c> en caso contrario.</returns>
        public bool LoadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);
                this.LoadFromJson(content);
                return true;
            }

            return false;
        }
    }
}
