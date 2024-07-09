using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace NZWalksAPI.Requests
{
    public class UpdateRegionRequestDto
    {
        [AllowNull]
        [StringLength(10)]
        public string? Code { get; set; }

        [AllowNull]
        [StringLength(100)]
        public string? Name { get; set; }

        [AllowNull]
        [Url]
        public string? RegionImageUrl { get; set; }
    }
}
