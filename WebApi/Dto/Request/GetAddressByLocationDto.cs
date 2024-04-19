using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto.Request;

public class GetAddressByLocationDto
{
    [Required(ErrorMessage = "Wrong latitude")]
    public double Lat { get; set; }
    [Required(ErrorMessage = "Wrong longitude")]
    public double Long { get; set; }
    
    public double? Radius { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
}