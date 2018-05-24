using System;
namespace EventBus
{
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
