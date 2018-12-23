using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHouse.Domain.Models;
using TestHouse.Persistence;

namespace TestHouse.Application.Services
{
    public class SuitService
    {
        private readonly TestHouseDbContext _projectDbContext;

        public SuitService(TestHouseDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }
        /// <summary>
        /// Add new suit
        /// </summary>
        /// <param name="name">Name of the suit</param>
        /// <param name="description">Description of the suit</param>
        /// <param name="projectId">Parent project id</param>
        /// <param name="parentId">Parent suit id</param>
        /// <returns>Added suit</returns>
        public async Task<Suit> AddSuit(string name, string description, long projectId, long parentId)
        {
            var project = await _projectDbContext.Projects.FirstOrDefaultAsync(p=> p.Id == projectId)
                        ?? throw new ArgumentException("Project with specified id is not found", "projectId");

            var parent = parentId == 0 
                        ? null 
                        : await _projectDbContext.Suits.Include(s=>s.Childs)
                                                    .FirstOrDefaultAsync(s=>s.Id == parentId);

            var order = parent?.Childs.Max(c => c.Order) ?? 0;

            var suit = new Suit(name, description,order + 1,project);

            parent?.Childs.Add(suit);

            _projectDbContext.Suits.Add(suit);
            await _projectDbContext.SaveChangesAsync();

            return suit;
        }
    }
}
