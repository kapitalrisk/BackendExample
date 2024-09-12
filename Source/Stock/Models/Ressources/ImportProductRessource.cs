using System.ComponentModel.DataAnnotations;

namespace Stock.Models.Ressources
{
    public class ImportProductRessource
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int StockAvailable { get; set; }
    }
}
