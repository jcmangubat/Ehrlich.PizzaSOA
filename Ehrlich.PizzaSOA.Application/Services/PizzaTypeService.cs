using AutoMapper;
using CsvHelper;
using Ehrlich.PizzaSOA.Application.Interfaces;
using Ehrlich.PizzaSOA.Application.Mappings.CSVFieldMappings;
using Ehrlich.PizzaSOA.Application.Models;
using Ehrlich.PizzaSOA.Application.Services.Abstractions;
using Ehrlich.PizzaSOA.Domain.Entities;
using Ehrlich.PizzaSOA.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SMEAppHouse.Core.CodeKits.Helpers;
using System.Globalization;
using System.Linq.Expressions;

namespace Ehrlich.PizzaSOA.Application.Services;

public class PizzaTypeService(IMapper mapper,
                        ILogger<IPizzaTypeService> logger,
                        IPizzaTypeRepository pizzaTypeRepository) :
    ServiceBase<IPizzaTypeService>(mapper, logger), IPizzaTypeService
{
    private readonly IPizzaTypeRepository _pizzaTypeRepository = pizzaTypeRepository
                            ?? throw new ArgumentNullException(nameof(pizzaTypeRepository));

    public async Task<PizzaTypeModel?> Get(Guid pizzaTypeId)
    {
        try
        {
            var pizzaType = await _pizzaTypeRepository.GetSingleAsync(p => p.Id == pizzaTypeId);
            return base.Mapper.Map<PizzaTypeModel>(pizzaType);
        }
        catch (Exception ex)
        {
            base.Logger.LogError(ex, ex.GetExceptionMessages());
            throw new ApplicationException("An error occurred while processing your request.");
        }
    }

    public async Task<PizzaTypeModel?> Get(string pizzaTypeCode)
    {
        try
        {
            var pizzaType = await _pizzaTypeRepository.GetSingleAsync(p => p.PizzaTypeCode == pizzaTypeCode);
            return base.Mapper.Map<PizzaTypeModel>(pizzaType);
        }
        catch (Exception ex)
        {
            base.Logger.LogError(ex, ex.GetExceptionMessages());
            throw new ApplicationException("An error occurred while processing your request.");
        }
    }

    public async Task<IEnumerable<PizzaTypeModel>?> SearchAsync(string? typeCodePartial, string? namePartial)
    {
        try
        {
            typeCodePartial = (typeCodePartial ?? string.Empty).Trim();
            namePartial = (namePartial ?? string.Empty).Trim();

            Expression<Func<PizzaType, bool>> predicate = p =>
                                        (typeCodePartial == string.Empty ||
                                         p.PizzaTypeCode.Contains(typeCodePartial) ) &&
                                        (namePartial == string.Empty ||
                                         p.Name.Contains(namePartial) );

            var pizzaTypes = await _pizzaTypeRepository.GetListAsync(predicate);

            return base.Mapper.Map<List<PizzaTypeModel>>(pizzaTypes);
        }
        catch (Exception ex)
        {
            base.Logger.LogError(ex, ex.GetExceptionMessages());
            throw new ApplicationException("An error occurred while processing your request.");
        }
    }    

    public async Task<int> ImportAsync(IFormFile file)
    {
        try
        {
            using var reader = new StreamReader(file.OpenReadStream());

            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<PizzaTypeModelMap>();

            var pizzaTypeModels = csv.GetRecords<PizzaTypeModel>().ToList();
            var pizzaTypes = base.Mapper.Map<List<PizzaType>>(pizzaTypeModels); // map to domain models

            // Fetch existing PizzaTypeCodes from the repository
            var existingPizzaTypeCodes = (await _pizzaTypeRepository.GetListAsync())
                                          .Select(pt => pt.PizzaTypeCode)
                                          .ToHashSet();

            // Filter out existing PizzaTypes
            var newPizzaTypes = pizzaTypes
                .Where(pt => !existingPizzaTypeCodes.Contains(pt.PizzaTypeCode))
                .ToList();

            if (newPizzaTypes == null || newPizzaTypes.Count == 0)
                return 0;

            await _pizzaTypeRepository.AddAsync(newPizzaTypes);
            await _pizzaTypeRepository.CommitAsync();

            return newPizzaTypes.Count;
        }
        catch (Exception ex)
        {
            base.Logger.LogError(ex, ex.GetExceptionMessages());
            throw new ApplicationException("An error occurred while processing your request.");
        }
    }
}

/*public async Task<IEnumerable<PizzaTypeModel>?> GetActivitiesByFilterAsync(Expression<Func<PizzaTypeModel, bool>> filterExpression)
    {
        try
        {
            Expression<Func<PizzaType, bool>> efFilterExpression = ExpressionConverter.Convert<PizzaType, PizzaTypeModel>(filterExpression);

            var pizzaTypes = await _pizzaTypeRepository.GetListAsync(efFilterExpression);
            return base.Mapper.Map<List<PizzaTypeModel>>(pizzaTypes);
        }
        catch (Exception ex)
        {
            base.Logger.LogDebug(ex.GetExceptionMessages());
            throw;
        }
    }*/