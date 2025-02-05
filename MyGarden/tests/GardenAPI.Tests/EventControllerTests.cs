using GardenAPI.Controllers;
using GardenAPI.Entities;
using GardenAPI.Entities.Events;
using GardenAPI.Entities.Plants;
using GardenAPI.Tests;
using GardenAPI.Transfer.Event;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace EventTest
{
    [TestFixture]
    internal class EventControllerTests : CommonSetUp
    {
        internal required EventController _eventController;

        [OneTimeSetUp]
        public void SetUp()
        {
            _eventController = new EventController(_eventService);
            Task.Run(() => _plantService.Set(_dataContext.Plants, [
                new Plant{
                    LightNeedId = 1,
                    StageId = 1,
                    Title = "meow",
                    BiologyTitle = "meow",
                    UserId = "meow",
                    WateringNeedId = 1,
                    GroupId = 0,
                    Id = 0
                },
                ])).Wait();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Task.Run(() => _plantService.Remove(_dataContext.Plants, [0])).Wait();
        }

        [Test, Order(1)]
        public async Task Post_ReturnsOkResult_WheneventsSaved()
        {
            // Arrange
            var eventRequest = new List<RequestEventDTO>
        {
            new RequestEventDTO {
                PlantId = 0,
                Title = "meow",
                UserId = "meow",
            },
            new RequestEventDTO {
                PlantId = 0,
                Title = "meow2",
                UserId = "meow2",
            }
        };
            // Act
            var result = await _eventController!.Post(eventRequest);

            // ClassicAssert
            ClassicAssert.IsInstanceOf<OkResult>(result);
        }
        [Test, Order(2)]
        public async Task Get_ReturnsOkResult_Withevents()
        {
            // Arrange
            var userId = "meow";
            var ids = new List<int> { 1, 2 };
            var entities = new List<Event>
            {
                new Event { UserId = "meow", Title="meow", PlantId = 0 },
            };

            var p = new List<EventDTO>();
            // Act
            var result = await _eventController!.Get(userId, ids);
            if (result.Result is OkObjectResult okResult)
            {
                p = okResult.Value as List<EventDTO>;
            }

            // ClassicAssert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.IsAssignableFrom<List<EventDTO>>(p);
            ClassicAssert.AreEqual(entities.Count, p?.Count() ?? 0);
        }

        [Test, Order(3)]
        public async Task Post_ReturnsOkResult_WheneventsChanged()
        {
            // Arrange
            var request = new List<RequestEventDTO>
        {
            new RequestEventDTO {
                Id = 1,
                Title = "meow",
                UserId = "meow",
                PlantId = 0,
            },
            new RequestEventDTO {
                Id = 2,
                Title = "meow3",
                UserId = "meow2",
                PlantId = 0,
            }
        };

            // Act
            var result = await _eventController!.Post(request);

            // ClassicAssert
            var badRequestResult = result as OkObjectResult;
            ClassicAssert.IsInstanceOf<OkResult>(result);

        }

        [Test, Order(4)]
        public async Task Delete_ReturnsOkResult_WheneventsDeleted()
        {
            // Arrange
            var eventIds = new List<int> { 1, 2 };

            // Act
            var result = await _eventController!.Delete(eventIds);

            // ClassicAssert
            ClassicAssert.IsInstanceOf<OkResult>(result);
        }

        [Test, Order(5)]
        public async Task Delete_ReturnsBadRequest_WheneventsNotDeleted()
        {
            // Arrange
            var ids = new List<int> { 1, 2 };

            // Act
            var result = await _eventController!.Delete(ids);

            // ClassicAssert
            var badRequestResult = result as BadRequestObjectResult;
            ClassicAssert.IsNotNull(badRequestResult, "Expected BadRequestObjectResult.");
            ClassicAssert.AreEqual("No events were deleted!", badRequestResult!.Value);
        }
    }
}