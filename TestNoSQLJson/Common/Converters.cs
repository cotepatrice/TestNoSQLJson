using System;
using System.Collections.Generic;
using TestNoSQLJson.DTOs;
using TestNoSQLJson.Models;

namespace TestNoSQLJson.Common
{
    public class Converters : IConverter
    {
        public ProfilLine ConvertDtoLineToModelLine(ProfilLineDto value)
        {
            var model = new ProfilLine
            {
                LabelText = value.LabelText,
                TypeName = GetRealTypeName(value.DotNetType),
                FieldName = value.FieldName,
                LabelVersion = value.LabelVersion,
                Value = value.Value
            };

            return model;
        }

        public ProfilLineDto ConvertModelLineToDtoLine(ProfilLine value)
        {
            var dto = new ProfilLineDto()
            {
                FieldName = value.FieldName,
                LabelVersion = value.LabelVersion,
                LabelText = value.LabelText,
                DotNetType = GetEnumFromText(value.TypeName)
            };

            return dto;
        }

        public ProfilInvestisseurDto ConvertModelToDto(ProfilInvestisseur profilInvestisseur)
        {
            var dto = new ProfilInvestisseurDto() { ProfilInvestisseurId = profilInvestisseur.ProfilInvestisseurId, SubscriberId = profilInvestisseur.Subscriber.SubscriberId };
            dto.Content = new List<ProfilLineDto>();

            foreach (var modelLine in profilInvestisseur.Content)
            {
                dto.Content.Add(new ProfilLineDto() 
                { 
                    FieldName = modelLine.FieldName, 
                    LabelVersion = modelLine.LabelVersion, 
                    LabelText = modelLine.LabelText, 
                    DotNetType = GetEnumFromText(modelLine.TypeName), 
                    Value = modelLine.Value 
                });
            }

            return dto;
        }

        public List<ProfilInvestisseurDto> GetDtoList(List<ProfilInvestisseur> modelList)
        {
            var dtoList = new List<ProfilInvestisseurDto>();
            foreach (var profil in modelList)
            {
                var newDto = ConvertModelToDto(profil);
                dtoList.Add(newDto);
            }

            return dtoList;
        }

        private DotNetType GetEnumFromText(string typeName)
        {
            if (typeName == typeof(int).FullName)
                return DotNetType.IntegerType;

            if (typeName == typeof(decimal).FullName)
                return DotNetType.DecimalType;

            if (typeName == typeof(DateTime).FullName)
                return DotNetType.DateTimeType;

            return DotNetType.StringType;
        }

        private static string GetRealTypeName(DotNetType type)
        {
            return type switch
            {
                DotNetType.IntegerType => typeof(int).FullName,
                DotNetType.DecimalType => typeof(decimal).FullName,
                DotNetType.DateTimeType => typeof(DateTime).FullName,
                _ => typeof(string).FullName
            };
        }
    }
}
