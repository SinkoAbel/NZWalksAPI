using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace NZWalksAPI.Requests
{
    public class CreateRegionRequestDto
    {
        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [AllowNull]
        [Url]
        public string? RegionImageUrl { get; set; }
    }
}
