using System.Collections.Generic;
using TestNoSQLJson.DTOs;
using TestNoSQLJson.Models;

namespace TestNoSQLJson.Common
{
    public interface IConverter
    {
        ProfilLine ConvertDtoLineToModelLine(ProfilLineDto value);
        ProfilLineDto ConvertModelLineToDtoLine(ProfilLine value);
        ProfilInvestisseurDto ConvertModelToDto(ProfilInvestisseur profilInvestisseurs);
        List<ProfilInvestisseurDto> GetDtoList(List<ProfilInvestisseur> modelList);
    }
}