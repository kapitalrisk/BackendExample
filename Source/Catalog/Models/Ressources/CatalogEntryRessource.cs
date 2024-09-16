using Catalog.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Models.Ressources
{
    public class CatalogEntryRessource
    {
        public CatalogEntryRessource(CatalogEntryEntity fromEntity)
        {
            this.Name = fromEntity.Name;
            this.Description = fromEntity.Description;
            this.Rating = fromEntity.Rating;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int Rating { get; set; }
    }
}
