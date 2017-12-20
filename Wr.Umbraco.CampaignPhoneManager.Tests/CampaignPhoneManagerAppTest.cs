﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers;
using Wr.Umbraco.CampaignPhoneManager.Tests.Providers;
using static Wr.Umbraco.CampaignPhoneManager.XmlHelper;

namespace Wr.Umbraco.CampaignPhoneManager.Tests
{
    [TestClass]
    public class CampaignPhoneManagerAppTest
    {
        [TestMethod]
        public void CampaignPhoneManagerApp_ProcessAllPotentialCandidatePhoneNumbers_WithNoCookie_WithNoFoundPhoneNumber_WithNoDefaultPhonenumber_ReturnsLastResortPhoneNumber()
        {
            // Arrange
            // generate test data
            var dataModel = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.CampaignDetail = new List<CampaignDetail>() { new CampaignDetail() { Id = "1201", PhoneNumber = "0800 123 4567", CampaignCode = "testcode" } };
            var testPhoneManagerData = SerializeXml.ToString(dataModel);
            var _dataProvider = new XPathDataProvider(new XPathDataProviderSource_GetXPathNavigatorTest(testPhoneManagerData));

            // generate the required result
            var correctResult = new FinalResultModel()
            {
                OutputCookieHolder = null,
                OutputModelResult = new OutputModel() { PhoneNumber = "XXX XXX XXXX" },
                OutputResultSource = OutputSource.LastResortPlaceholder
            };

            CampaignPhoneManagerApp target = new CampaignPhoneManagerApp(null, _dataProvider, null, null, null, null);
            PrivateObject obj = new PrivateObject(target);

            //Act
            FinalResultModel retVal = (FinalResultModel)obj.Invoke("ProcessAllPotentialCandidatePhoneNumbers", new object[] { new CookieHolder(), new CampaignDetail() });

            //Assert
            Assert.AreEqual(retVal.OutputResultSource, correctResult.OutputResultSource);
            Assert.AreEqual(retVal.OutputModelResult.PhoneNumber, correctResult.OutputModelResult.PhoneNumber);
        }

        [TestMethod]
        public void CampaignPhoneManagerApp_ProcessAllPotentialCandidatePhoneNumbers_WithNoCookie_WithNoFoundPhoneNumber_WithDefaultPhonenumber_ReturnsDefaultPhoneNumber()
        {
            // Arrange
            // generate test data
            var dataModel = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "0800 000 0001", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.CampaignDetail = new List<CampaignDetail>() { new CampaignDetail() { Id = "1201", PhoneNumber = "0800 123 4567", CampaignCode = "testcode" } };
            var testPhoneManagerData = SerializeXml.ToString(dataModel);
            var _dataProvider = new XPathDataProvider(new XPathDataProviderSource_GetXPathNavigatorTest(testPhoneManagerData));

            // generate the required result
            var correctResult = new FinalResultModel()
            {
                OutputCookieHolder = null,
                OutputModelResult = new OutputModel() { PhoneNumber = "0800 000 0001" },
                OutputResultSource = OutputSource.DefaultNumberFromAdmin
            };

            CampaignPhoneManagerApp target = new CampaignPhoneManagerApp(null, _dataProvider, null, null, null, null);
            PrivateObject obj = new PrivateObject(target);

            //Act
            FinalResultModel retVal = (FinalResultModel)obj.Invoke("ProcessAllPotentialCandidatePhoneNumbers", new object[] { new CookieHolder(), new CampaignDetail() });

            //Assert
            Assert.AreEqual(retVal.OutputResultSource, correctResult.OutputResultSource);
        }

        [TestMethod]
        public void CampaignPhoneManagerApp_ProcessAllPotentialCandidatePhoneNumbers_WithNoCookie_WithFoundPhoneNumber_ReturnsFoundPhoneNumber()
        {
            // Arrange
            // generate test data
            var dataModel = new CampaignPhoneManagerModel() { DefaultPhoneNumber = "0800 000 0001", DefaultCampaignQueryStringKey = "fsource", DefaultPersistDurationInDays = 32 };
            dataModel.CampaignDetail = new List<CampaignDetail>() { new CampaignDetail() { Id = "1201", PhoneNumber = "0800 123 4567", CampaignCode = "testcode" } };
            var testPhoneManagerData = SerializeXml.ToString(dataModel);
            var _dataProvider = new XPathDataProvider(new XPathDataProviderSource_GetXPathNavigatorTest(testPhoneManagerData));
            var foundRecord = new CampaignDetail() { Id = "1201", PhoneNumber = "FOUND PHONENUMBER" };
            // generate the required result
            var correctResult = new FinalResultModel()
            {
                OutputCookieHolder = null,
                OutputModelResult = new OutputModel() { PhoneNumber = "FOUND PHONENUMBER" },
                OutputResultSource = OutputSource.FoundRecordFromCriteria
            };

            CampaignPhoneManagerApp target = new CampaignPhoneManagerApp(null, _dataProvider, null, null, null, null);
            PrivateObject obj = new PrivateObject(target);

            //Act
            FinalResultModel retVal = (FinalResultModel)obj.Invoke("ProcessAllPotentialCandidatePhoneNumbers", new object[] { new CookieHolder(), foundRecord });

            //Assert
            Assert.AreEqual(retVal.OutputResultSource, correctResult.OutputResultSource);
            Assert.AreEqual(retVal.OutputModelResult.PhoneNumber, correctResult.OutputModelResult.PhoneNumber);
        }
    }
}