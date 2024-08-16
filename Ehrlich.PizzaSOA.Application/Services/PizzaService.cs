using AutoMapper;
using CsvHelper;
using Ehrlich.PizzaSOA.Application.Interfaces;
using Ehrlich.PizzaSOA.Application.Mappings.CSVFieldMappings;
using Ehrlich.PizzaSOA.Application.Models;
using Ehrlich.PizzaSOA.Application.Services.Abstractions;
using Ehrlich.PizzaSOA.Domain.Constants;
using Ehrlich.PizzaSOA.Domain.Entities;
using Ehrlich.PizzaSOA.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMEAppHouse.Core.CodeKits.Helpers;
using System.Globalization;
using System.Linq.Expressions;

namespace Ehrlich.PizzaSOA.Application.Services;

public class PizzaService(IMapper mapper,
                        ILogger<IPizzaService> logger,
                        IPizzaRepository pizzaRepository,
                        IPizzaTypeRepository pizzaTypeRepository) :
    ServiceBase<IPizzaService>(mapper, logger), IPizzaService
{
    private readonly IPizzaRepository _pizzaRepository = pizzaRepository
                            ?? throw new ArgumentNullException(nameof(pizzaRepository));

    private readonly IPizzaTypeRepository _pizzaTypeRepository = pizzaTypeRepository
                            ?? throw new ArgumentNullException(nameof(pizzaTypeRepository));

    public async Task<PizzaModel?> Get(Guid pizzaId)
    {
        try
        {
            var pizzaType = await _pizzaRepository.GetSingleAsync(p => p.Id == pizzaId, p => p.Include(x => x.OrderDetails));
            return base.Mapper.Map<PizzaModel>(pizzaType);
        }
        catch (Exception ex)
        {
            base.Logger.LogError(ex, ex.GetExceptionMessages());
            throw new ApplicationException("An error occurred while processing your request.");
        }
    }

    public async Task<PizzaModel?> Get(string pizzaCode)
    {
        try
        {
            var pizza = await _pizzaRepository.GetSingleAsync(p => p.PizzaCode == pizzaCode);
            return base.Mapper.Map<PizzaModel>(pizza);
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
            csv.Context.RegisterClassMap<PizzaModelMap>();

            var pizzaModels = csv.GetRecords<PizzaModel>().ToList();
            var pizzas = base.Mapper.Map<List<Pizza>>(pizzaModels); // map to domain models

            // Fetch existing PizzaCodes from the repository
            var existingPizzaCodes = (await _pizzaRepository.GetListAsync())
                                          .Select(pt => pt.PizzaCode)
                                          .ToHashSet();

            // Filter out existing PizzaTypes
            var newPizzas = pizzas
                .Where(p => !existingPizzaCodes.Contains(p.PizzaCode))
                .ToList();

            if (newPizzas == null || newPizzas.Count == 0)
                return 0;

            var pizzaTypeCodes = pizzas.Select(p => p.PizzaTypeCode).Distinct();
            var pizzaTypes = await _pizzaTypeRepository.GetListAsync(filter: pt => pizzaTypeCodes.Any(code => code == pt.PizzaTypeCode));

            foreach (var pizz in newPizzas)
            {
                var pizzaType = pizzaTypes.FirstOrDefault(p => p.PizzaTypeCode == pizz.PizzaTypeCode);
                if (pizzaType != null)
                    pizz.PizzaTypeId = pizzaType.Id;
                else
                {
                }
            }

            await _pizzaRepository.AddAsync(newPizzas);
            await _pizzaRepository.CommitAsync();

            return newPizzas.Count;
        }
        catch (Exception ex)
        {
            base.Logger.LogError(ex, ex.GetExceptionMessages());
            throw new ApplicationException("An error occurred while processing your request.");
        }
    }

    public async Task<IEnumerable<PizzaModel>?> SearchAsync(Rules.PizzaSizesEnum? pizzaSize, decimal? priceLow, decimal? priceHigh)
    {
        try
        {
            if (priceLow != null && priceHigh != null && priceHigh < priceLow)
                throw new ArgumentException("Price range is incorrect.");

            Expression<Func<Pizza, bool>> predicate = p =>
                                        (pizzaSize == null || p.Size == pizzaSize) &&
                                        ((priceLow == null && priceHigh == null) ||
                                        (priceLow != null && priceHigh != null && p.Price >= priceLow && p.Price <= priceHigh) ||
                                        (priceLow != null && priceHigh == null && p.Price >= priceLow) ||
                                        (priceLow == null && priceHigh != null && p.Price <= priceHigh));

            var pizzas = await _pizzaRepository.GetListAsync(predicate);

            return base.Mapper.Map<List<PizzaModel>>(pizzas);
        }
        catch (Exception ex)
        {
            base.Logger.LogError(ex, ex.GetExceptionMessages());
            throw new ApplicationException("An error occurred while processing your request.");
        }
    }
}