using Stock.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Stock.Models.Ressources
{
    public class ProductRessource
    {
        public ProductRessource(ProductEntity fromEntity)
        {
            this.Id = fromEntity.Id;
            this.Name = fromEntity.Name;
            this.Description = fromEntity.Description;
            this.StockAvailable = fromEntity.StockAvailable;
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int StockAvailable { get; set; }
    }
}
