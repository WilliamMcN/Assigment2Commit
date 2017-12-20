using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using WebApplication2.Controllers;
using System.Collections.Generic;
using WebApplication2.Models;
using System.Linq;
using System.Text;
using Moq;

namespace WebApplication2.Tests.Controllers
{
    [TestClass]
    public class PartieControllerTest
    {
        PartiesController controller;
        List<Party> parties;
        Mock<IStoreManagerRepository> mock;

        [TestInitialize]
        public void TestInitialize()
        {
            // Arrange
            mock = new Mock<IStoreManagerRepository>();

            // Mock the Album Data
            parties = new List<Party>
            {
                new Party { PartyId = 1, Party_Type_ = "Concert", Message_ = "Party time", Address_ = "Barrie",PLocation = new PLocation {LocationID = 3, City = "Barrie" } },
                new Party { PartyId = 2, Party_Type_ = "Kegger", Message_ = "Party time", Address_ = "Barrie",PLocation = new PLocation {LocationID = 3, City = "Barrie"  } },
                new Party { PartyId = 3, Party_Type_ = "Party", Message_ = "Party time", Address_ = "Barrie",PLocation = new PLocation {LocationID = 3, City = "Barrie" } }
            };

            // populate the mock object with our sample data
            mock.Setup(m => m.Parties).Returns(parties.AsQueryable());

            // Pass the mock to 2nd constructor
            controller = new PartiesController(mock.Object);
        }

        [TestMethod]
        public void IndexViewLoads()
        {
            // Arrange

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexReturnsParties()
        {
            // Act
            // call Index, set result to an Album List as specified in Index's Model
            var actual = (List<Party>)controller.Index().Model;

            // Assert
            // check if the list returned in the view matches the list we passed in to the mock
            CollectionAssert.AreEqual(parties, actual);
        }

        [TestMethod]
        public void DetailsValidParty()
        {
            // Act
            var actual = (Party)controller.Details(1).Model;

            // Assert
            Assert.AreEqual(parties.ToList()[0], actual);
        }

        [TestMethod]
        public void DetailsInvalidParty()
        {
            // Act
            var actual = (Party)controller.Details(11111).Model;

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void DetailsInvalidNoId()
        {
            // Arrange
            int? id = null;

            // Act
            var actual = controller.Details(id);

            // Assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        // GET: Edit
        [TestMethod]
        public void EditPartyValid()
        {
            // ACT
            var actual = (Party)controller.Edit(1).Model;

            // ASSERT
            Assert.AreEqual(parties.ToList()[0], actual);
        }

        [TestMethod]
        public void EditInvalidNoId()
        {
            //arrange
            int? id = null;

            //act
            var actual = (ViewResult)controller.Edit(id);

            //assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void EditInvalidPartyId()
        {
            // Act
            ViewResult result = controller.Edit(-314) as ViewResult;

            // Assert
            Assert.AreEqual("Error", result.ViewName);
        }

        // GET: Delete
        [TestMethod]
        public void DeleteValidParty()
        {
            // Act            
            var actual = (Party)controller.Delete(1).Model;

            // Assert            
            Assert.AreEqual(parties.ToList()[0], actual);
        }

        // Delete invalid ID test
        [TestMethod]
        public void DeleteInvalidPartyId()
        {
            // Arrange
            int id = 87656765;

            // Act
            ViewResult actual = (ViewResult)controller.Delete(id);

            // Assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteInvalidNoId()
        {
            // arrange           
            int? id = null;

            // act           
            ViewResult actual = controller.Delete(id);

            // assert           
            Assert.AreEqual("Error", actual.ViewName);
        }

        // GET: Create
        [TestMethod]
        public void CreateViewLoads()
        {
            // act - cast the return type as ViewResult
            ViewResult actual = (ViewResult)controller.Create();

            // assert
            Assert.AreEqual("Create", actual.ViewName);
        }

        // POST: Create
        [TestMethod]
        public void CreateValidParty()
        {
            // arrange
            Party parties = new Party
            {
                PartyId = 4,
                Party_Type_ = "Concert",
                Message_ = "Party time",
                Address_ = "Barrie",
                LocationID = 1
            };

            // act
            RedirectToRouteResult actual = (RedirectToRouteResult)controller.Create(parties);

            // assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateInvalidParty()
        {
            // arrange
            controller.ModelState.AddModelError("key", "error message");

            Party parties = new Party
            {
                PartyId = 4,
                Party_Type_ = "Concert",
                Message_ = "Party time",
                Address_ = "Barrie",
                LocationID = 1
                
            };

            // act - cast the return type as ViewResult
            ViewResult actual = (ViewResult)controller.Create(parties);

            // assert
            Assert.AreEqual("Create", actual.ViewName);
        }

        // POST: Edit
        [TestMethod]
        public void EditValidParty()
        {
            // arrange
            Party party = parties.ToList()[0];

            // act
            RedirectToRouteResult actual = (RedirectToRouteResult)controller.Edit(party);

            // assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }

        [TestMethod]
        public void EditInvalidParty()
        {
            // arrange
            controller.ModelState.AddModelError("key", "error message");

            Party parties = new Party
            {
                PartyId = 4,
                Party_Type_ = "Concert",
                Message_ = "Party time",
                Address_ = "Barrie",
                LocationID = 1
            };

            // act - cast the return type as ViewResult
            ViewResult actual = (ViewResult)controller.Edit(parties);

            // assert
            Assert.AreEqual("Edit", actual.ViewName);
        }

        // POST: DeleteConfirmed
        [TestMethod]
        public void DeleteConfirmedValidParty()
        {
            // Act            
            RedirectToRouteResult actual = (RedirectToRouteResult)controller.DeleteConfirmed(1);

            // Assert            
            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }

        // Delete invalid ID test
        [TestMethod]
        public void DeleteConfirmedInvalidPartyId()
        {
            // Arrange
            int id = 87656765;

            // Act
            ViewResult actual = (ViewResult)controller.DeleteConfirmed(id);

            // Assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedInvalidNoId()
        {
            // arrange           
            int? id = null;

            // act           
            ViewResult actual = (ViewResult)controller.Delete(id);

            // assert           
            Assert.AreEqual("Error", actual.ViewName);
        }
    }
}
