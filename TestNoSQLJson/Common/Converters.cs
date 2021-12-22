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
                TypeName = GetRealTypeName(value.DotNetProfileModelType),
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
                DotNetProfileModelType = GetEnumFromText(value.TypeName)
            };

            return dto;
        }

        public ProfilInvestisseurDto ConvertModelToDto(ProfilInvestisseur profilInvestisseur)
        {
            var dto = new ProfilInvestisseurDto() 
            { 
                ProfilInvestisseurId = profilInvestisseur.ProfilInvestisseurId, 
                SubscriberId = profilInvestisseur.Subscriber.SubscriberId, 
                CreationDate = profilInvestisseur.CreationDate 
            };
            dto.Content = new List<ProfilLineDto>();

            foreach (var modelLine in profilInvestisseur.Content)
            {
                dto.Content.Add(new ProfilLineDto() 
                { 
                    FieldName = modelLine.FieldName, 
                    LabelVersion = modelLine.LabelVersion, 
                    LabelText = modelLine.LabelText, 
                    DotNetProfileModelType = GetEnumFromText(modelLine.TypeName), 
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

        private DotNetProfileModelType GetEnumFromText(string typeName)
        {
            if (typeName == typeof(int).FullName)
                return DotNetProfileModelType.IntegerType;

            if (typeName == typeof(decimal).FullName)
                return DotNetProfileModelType.DecimalType;

            if (typeName == typeof(DateTime).FullName)
                return DotNetProfileModelType.DateTimeType;

            if (typeName == typeof(ProfilLineDto).FullName)
                return DotNetProfileModelType.ProlilLine;

            if (typeName == typeof(CompositeValueDto).FullName)
                return DotNetProfileModelType.CompositeValue;

            return DotNetProfileModelType.StringType;
        }

        private static string GetRealTypeName(DotNetProfileModelType type)
        {
            return type switch
            {
                DotNetProfileModelType.IntegerType => typeof(int).FullName,
                DotNetProfileModelType.DecimalType => typeof(decimal).FullName,
                DotNetProfileModelType.DateTimeType => typeof(DateTime).FullName,
                DotNetProfileModelType.ProlilLine => typeof(ProfilLineDto).FullName,
                DotNetProfileModelType.CompositeValue => typeof(CompositeValueDto).FullName,
                _ => typeof(string).FullName
            };
        }
    }
}
