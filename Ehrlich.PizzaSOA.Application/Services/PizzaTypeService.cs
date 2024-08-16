using AutoMapper;
using CsvHelper;
using Ehrlich.PizzaSOA.Application.Interfaces;
using Ehrlich.PizzaSOA.Application.Models;
using Ehrlich.PizzaSOA.Application.Services.Abstractions;
using Ehrlich.PizzaSOA.Domain.Entities;
using Ehrlich.PizzaSOA.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SMEAppHouse.Core.CodeKits.Helpers;
using System.Globalization;

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
            base.Logger.LogDebug(ex.GetExceptionMessages());
            throw;
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
            base.Logger.LogDebug(ex.GetExceptionMessages());
            throw;
        }
    }

    public async Task<IEnumerable<PizzaTypeModel>?> GetListAsync(string pizzaTypeCodePartial)
    {
        try
        {
            var pizzaTypes = await _pizzaTypeRepository.GetListAsync(p => p.PizzaTypeCode.StartsWith(pizzaTypeCodePartial) ||
                                                                        p.PizzaTypeCode.EndsWith(pizzaTypeCodePartial));
            return base.Mapper.Map<List<PizzaTypeModel>>(pizzaTypes);
        }
        catch (Exception ex)
        {
            base.Logger.LogDebug(ex.GetExceptionMessages());
            throw;
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

    public async Task ImportAsync(IFormFile file)
    {
        try
        {
            using var reader = new StreamReader(file.OpenReadStream());
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var pizzaTypeModels = csv.GetRecords<PizzaTypeModel>().ToList();
            var pizzaTypes = base.Mapper.Map<List<PizzaType>>(pizzaTypeModels);

            await _pizzaTypeRepository.AddAsync(pizzaTypes);
            await _pizzaTypeRepository.CommitAsync();
        }
        catch (Exception ex)
        {
            base.Logger.LogDebug(ex.GetExceptionMessages());
            throw;
        }
    }
}