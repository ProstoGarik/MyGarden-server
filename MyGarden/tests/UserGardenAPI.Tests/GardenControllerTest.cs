using Microsoft.AspNetCore.Mvc;
using Mongo2Go;
using MongoDB.Bson;
using MongoDB.Driver;
using UserGardenAPI.Controllers;
using UserGardenAPI.Model;

namespace UserGardenAPI.Tests
{

    [TestFixture]
    public class GardenControllerTests
    {
        private MongoDbRunner _runner;
        private IMongoClient _mockMongoClient;
        private IMongoCollection<Garden>? _mockCollection;
        private GardenRepository _mockRepository;
        private GardenController _controller;
        private const string _userId = "test-user";

        [OneTimeSetUp]
        public void SetUp()
        {
            // Start an in-memory MongoDB instance
            _runner = MongoDbRunner.Start();

            // Create a connection string to the in-memory instance
            var connectionString = _runner.ConnectionString;

            // Create a MongoClient
            _mockMongoClient = new MongoClient(connectionString);

            // Get a database and a collection
            var database = _mockMongoClient.GetDatabase("TestDb");
            var collection = database.GetCollection<BsonDocument>("TestCollection");


            // Теперь создаем GardenRepository с использованием мокированного mongoClient
            _mockRepository = new GardenRepository(_mockMongoClient);

            // Создаем контроллер с использованием мокированного репозитория
            _controller = new GardenController(_mockRepository);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _mockMongoClient.Dispose();
            _runner.Dispose();
        }
        [Test, Order(1)]
        public async Task CreateGarden_ShouldReturnCreatedAtAction_WhenGardenIsCreated()
        {
            // Arrange
            var garden1 = new Garden { Id = "1", UserId = _userId, GardenType = "meow1", Plants = [] };
            var garden2 = new Garden { Id = "2", UserId = _userId, GardenType = "meow1", Plants = [] };

            // Act
            var result1 = await _controller.CreateGarden(garden1);
            var result2 = await _controller.CreateGarden(garden2);
            Assert.IsAssignableFrom<OkResult>(result1);
            Assert.IsAssignableFrom<OkResult>(result2);
        }

        [Test, Order(2)]
        public async Task GetGardens_ShouldReturnOkResult_WhenGardensExist()
        {
            // Arrange
            var gardens = new List<Garden> {
                new Garden { Id = "1", UserId = _userId, GardenType = "meow1", Plants = [] },
                new Garden { Id = "2", UserId = _userId, GardenType = "meow1", Plants = [] }
            };

            // Act
            var result = await _controller.GetGardenByUserId(_userId);
            // Assert
            Assert.IsAssignableFrom<OkObjectResult>(result.Result);

            Assert.That(((result.Result as OkObjectResult)!.Value as List<Garden>)!.Count(), Is.EqualTo(gardens.Count()));

        }
        [Test, Order(3)]
        public async Task GetGardens_ShouldReturnNotFound_WhenGardensNotExist()
        {
            // Arrange
            var gardens = new List<Garden> {
                new Garden { Id = "1", UserId = _userId, GardenType = "meow1", Plants = [] },
                new Garden { Id = "2", UserId = _userId, GardenType = "meow1", Plants = [] }
            };

            // Act
            var result = await _controller.GetGardenByUserId(_userId + "1");
            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

        [Test, Order(4)]
        public async Task GetGarden_ShouldReturnOkResult_WhenGardenExist()
        {
            // Arrange
            var gardenId = "1";

            // Act
            var result = await _controller.GetGardenById(gardenId);

            // Assert
            Assert.IsAssignableFrom<OkObjectResult>(result.Result);
        }
        [Test, Order(5)]
        public async Task GetGarden_ShouldReturnNotFound_WhenGardenDoesNotExist()
        {
            // Arrange
            var gardenId = "non-existent-id";

            // Act
            var result = await _controller.GetGardenById(gardenId);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }


        [Test, Order(6)]
        public async Task UpdateGarden_ShouldReturnOkResult_WhenGardenIsUpdated()
        {
            // Arrange
            var gardenId = "1";
            var garden = new Garden { Id = "1", UserId = "test-user", GardenType = "meow2" };

            // Act
            var result = await _controller.UpdateGarden(gardenId, garden);
            Assert.IsAssignableFrom<OkResult>(result);
        }
        [Test, Order(7)]
        public async Task UpdateGarden_ShouldReturnNotFound_WhenGardenIsUpdated()
        {
            // Arrange
            var gardenId = "not-found";
            var garden = new Garden { Id = "1", UserId = "test-user", GardenType = "meow2" };

            // Act
            var result = await _controller.UpdateGarden(gardenId, garden);
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Test, Order(8)]
        public async Task DeleteGarden_ShouldReturnOkResult_WhenGardenIsDeleted()
        {
            // Arrange
            var gardenId = "1";


            // Act
            var result = await _controller.DeleteGarden(gardenId);

            Assert.IsAssignableFrom<OkResult>(result);
        }

        [Test, Order(9)]
        public async Task DeleteGarden_ShouldReturnNotFound_WhenGardenIsDeleted()
        {
            // Arrange
            var gardenId = "not-found";


            // Act
            var result = await _controller.DeleteGarden(gardenId);

            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

    }

}