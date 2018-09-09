using System.ComponentModel.DataAnnotations;

namespace Chushka.Web.BindingModels
{
    public class ProductBindingModel
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Price { get; set; }

        public string Description { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
