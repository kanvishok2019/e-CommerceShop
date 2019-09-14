using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Core.Command;
using Xunit;

namespace Infrastructure.Tests.Command
{
    public class CommandBusTests
    {
        [Fact]
        public async Task When_The_Input_Command_Is_Null_Throw_ArgumentNullException()
        {
            Dictionary<Type, ICommandHandler> commandHandlers = null;
            CommandHandler commandHandler = null;
            var commandBus = new CommandBus(commandHandlers);
            await Assert.ThrowsAsync<ArgumentNullException>(() => commandBus.SubscribeAsync(commandHandler));
        }

        [Fact]
        public async Task When_The_Input_Same_CommandHandler_Should_Throw_InvalidOperationException()
        {
            Dictionary<Type, ICommandHandler> commandHandlers = new Dictionary<Type, ICommandHandler>();
            var commandBus = new CommandBus(commandHandlers);
            CommandHandler commandHandler = new CommandHandler();
            await commandBus.SubscribeAsync(commandHandler);
            await Assert.ThrowsAsync<InvalidOperationException>(() => commandBus.SubscribeAsync(commandHandler));
        }

        [Fact]
        public async Task SendAsync_Should_Handle_The_HandleAsync()
        {
            Dictionary<Type, ICommandHandler> commandHandlers = new Dictionary<Type, ICommandHandler>();
            var commandBus = new CommandBus(commandHandlers);
            CommandHandler commandHandler = new CommandHandler();
            await commandBus.SubscribeAsync(commandHandler);
            var testCommand = new TestCommand();
            await commandBus.SendAsync(testCommand);
            Assert.True(testCommand.TestCommandValue);
        }
    }

    public class CommandHandler : ICommandHandler<TestCommand>
    {
        public Task HandleAsync(TestCommand command)
        {
            command.TestCommandValue = true;
            return Task.FromResult(true);
        }
    }
    public class TestCommand : ICommand
    {
        public bool TestCommandValue = false;
        public void Initialize(Guid correlationId = default(Guid))
        {

        }
    }
}
