using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TestNoSQLJson.Common;
using TestNoSQLJson.DTOs;
using TestNoSQLJson.Models;
using Xunit;

namespace TestJsonObjects
{
    public class UnitTest1
    {
        [Fact]
        public void Dto_With_ComplexObjects_Should_ConvertToModel()
        {
            ProfilInvestisseurDto profilDto = GetProfilDtoObject();

            var testValue = JsonConvert.SerializeObject(profilDto);

            var converters = new Converters();

            var contentList = new List<ProfilLine>();
            foreach (var line in profilDto.Content)
            {
                var profilLine = converters.ConvertDtoLineToModelLine(line);
                contentList.Add(profilLine);
            }

            Assert.NotNull(testValue);
            Assert.NotEmpty(testValue);
        }

        private static ProfilInvestisseurDto GetProfilDtoObject()
        {
            var compositeValue1 = new CompositeValueDto() { NumberOfUnits = 2, UnitName = "Mois" };
            var compositeValue2 = new CompositeValueDto() { NumberOfUnits = 1, UnitName = "Année" };

            var subline = new List<ProfilLineDto>()
            {
                new ProfilLineDto()
                {
                    FieldName = "ChildBirth",
                    LabelText = "Naissance d’un enfant",
                    LabelVersion = 1.0M,
                    DotNetProfileModelType = DotNetProfileModelType.CompositeValue,
                    Value = JsonConvert.SerializeObject(compositeValue1)
                },
                new ProfilLineDto()
                {
                    FieldName = "HouseBuy",
                    LabelText = "Achat d’une résidence",
                    LabelVersion = 1.0M,
                    DotNetProfileModelType = DotNetProfileModelType.CompositeValue,
                    Value = JsonConvert.SerializeObject(compositeValue2)
                }

            };

            var profilDto = new ProfilInvestisseurDto()
            {
                ProfilInvestisseurId = 28,
                SubscriberId = 1,
                CreationDate = DateTime.UtcNow,
                Content = new List<ProfilLineDto>()
                {
                    new ProfilLineDto()
                    {
                        FieldName = "DisruptingEvents",
                        LabelVersion = 1.0M,
                        LabelText = @"Dans le futur, est-ce que l’un ou plusieurs des événements suivants pourraient 
affecter votre situation actuelle en tant qu’investisseur ou pourraient faire en sorte que vous deviez 
retirer votre investissement plus tôt que prévu?",
                        DotNetProfileModelType = DotNetProfileModelType.ProlilLine,
                        Value = JsonConvert.SerializeObject(subline)
                    }
                }
            };
            return profilDto;
        }
    }
}
