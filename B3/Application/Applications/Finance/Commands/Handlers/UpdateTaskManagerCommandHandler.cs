﻿using Applications.Interfaces.Repository;
using Applications.Interfaces.Service;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Applications.Finance.Commands.Handlers
{
    public class UpdateTaskManagerCommandHandler : IRequestHandler<UpdateTaskManagerCommand, Response>  
    {
        private readonly IResponse _response;
        private readonly ITaskManagerService _taskManagerService;
        private readonly ITaskManagerRepository _taskManagerRepository;
        private readonly ILogger<UpdateTaskManagerCommandHandler> _logger;


        public UpdateTaskManagerCommandHandler(IResponse response, ITaskManagerService financeService, ITaskManagerRepository taskRepository, ILogger<UpdateTaskManagerCommandHandler> logger)
        {
            _response = response;
            _taskManagerService = financeService;
            _taskManagerRepository = taskRepository;
            _logger = logger;
        }
            
        public async Task<Response> Handle(UpdateTaskManagerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Start process to update new task in rabbitMq");

                var entity = await _taskManagerRepository.FindByIdAsync(request.GetId());

                if (entity is null)
                    return await _response.CreateErrorResponseAsync(HttpStatusCode.NotFound);

                entity.LastModified = DateTime.Now;
                entity.Description = request.Description;
                entity.Status = request.Status;

                _logger.LogInformation("Start process to send task in rabbitMq");

                await _taskManagerService.SendMessage(entity);

                return await _response.CreateSuccessResponseAsync(null, string.Empty);

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"something went wrong message : {ex.Message}");

                return await _response.CreateErrorResponseAsync(null, HttpStatusCode.InternalServerError);
            }
        }
    }
}
