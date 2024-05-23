using Application.Features.Managements.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Managements;

public class ManagementManager : IManagementService
{
    private readonly IManagementRepository _managementRepository;
    private readonly ManagementBusinessRules _managementBusinessRules;

    public ManagementManager(IManagementRepository managementRepository, ManagementBusinessRules managementBusinessRules)
    {
        _managementRepository = managementRepository;
        _managementBusinessRules = managementBusinessRules;
    }

    public async Task<Management?> GetAsync(
        Expression<Func<Management, bool>> predicate,
        Func<IQueryable<Management>, IIncludableQueryable<Management, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Management? management = await _managementRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return management;
    }

    public async Task<IPaginate<Management>?> GetListAsync(
        Expression<Func<Management, bool>>? predicate = null,
        Func<IQueryable<Management>, IOrderedQueryable<Management>>? orderBy = null,
        Func<IQueryable<Management>, IIncludableQueryable<Management, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Management> managementList = await _managementRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return managementList;
    }

    public async Task<Management> AddAsync(Management management)
    {
        Management addedManagement = await _managementRepository.AddAsync(management);

        return addedManagement;
    }

    public async Task<Management> UpdateAsync(Management management)
    {
        Management updatedManagement = await _managementRepository.UpdateAsync(management);

        return updatedManagement;
    }

    public async Task<Management> DeleteAsync(Management management, bool permanent = false)
    {
        Management deletedManagement = await _managementRepository.DeleteAsync(management);

        return deletedManagement;
    }
}
