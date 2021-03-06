﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHouse.Application.Infastructure.Repositories;
using TestHouse.Application.Extensions;
using TestHouse.Domain.Models;
using TestHouse.DTOs.DTOs;

namespace TestHouse.Application.Services
{
    public class SuitService
    {
        private readonly IProjectRepository _projectRepository;

        public SuitService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        /// <summary>
        /// Add new suit
        /// </summary>
        /// <param name="name">Name of the suit</param>
        /// <param name="description">Description of the suit</param>
        /// <param name="projectId">Parent project id</param>
        /// <param name="parentId">Parent suit id</param>
        /// <returns>Added suit</returns>
        public async Task<SuitDto> AddSuitAsync(string name, string description, long projectId, long? parentId = null)
        {
            var project = await _projectRepository.GetAsync(projectId)
                        ?? throw new ArgumentException("Project with specified id is not found", "projectId");


            var suit = project.AddSuit(name, description, parentId);

            await _projectRepository.SaveAsync();

            return suit.ToSuitDto();
        }
    }
}
