using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Models.Entities
{
    [Table("products")]
    public class CatalogEntryEntity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }
}
