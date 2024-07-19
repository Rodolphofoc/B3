using Domain;
using Domain.Domain;
using MediatR;

namespace Applications.Finance.Commands
{
    public class AddTaskManagerCommand : IRequest<Response>
    {
        public string Description { get; set; }

        public StatusTask Status { get; set; }
    }
}
