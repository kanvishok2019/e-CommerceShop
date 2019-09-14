using System;
using Xunit;

namespace Infrastructure.Tests.Command
{
    public class CommandTests
    {
        [Fact]
        void Create_New_Command_Should_Have_CommandId_And_CreatedDate()
        {
            var command = new Core.Command.Command();
            Assert.Equal(default(Guid), command.Id);
            Assert.Equal(DateTime.UtcNow.Date, command.CreatedAt.Date);
        }

        [Fact]
        void Command_Initialization_Should_Set_NewGUId()
        {
            var command = new Core.Command.Command();
            var guid = Guid.NewGuid();
            command.Initialize(guid);
            Assert.Equal(guid, command.CorrelationId);
        }
        
    }
}
