using System;
using Xunit;

namespace Infrastructure.Tests.Event
{
    public class EventTests
    {
        [Fact]
        void Create_New_Event_Should_Have_EventId_And_CreatedDate()
        {
            var eventObj = new Core.Event.Event();
            Assert.Equal(default(Guid), eventObj.Id);
            Assert.Equal(DateTime.UtcNow.Date, eventObj.CreatedAt.Date);
        }

        [Fact]
        void Command_Initialization_Should_Set_NewGUId()
        {
            var eventObj = new Core.Event.Event();
            var guid = Guid.NewGuid();
            eventObj.Initialize(guid);
            Assert.Equal(guid, eventObj.CorrelationId);
        }
    }
}
