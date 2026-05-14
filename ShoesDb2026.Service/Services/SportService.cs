using FluentValidation;
using ShoesDb2026.Data;
using ShoesDb2026.Entities;
using ShoesDb2026.Service.Common;
using ShoesDb2026.Service.DTOs.Brand;
using ShoesDb2026.Service.DTOs.Sport;
using ShoesDb2026.Service.Interfaces;
using ShoesDb2026.Service.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Service.Services
{
    public class SportService : ISportService
    {
        private readonly IValidator<Sport> _validator;
        private readonly IUnitOfWork _uow;
        public SportService(IValidator<Sport> validator, IUnitOfWork uow)
        {
            _validator = validator;
            _uow = uow;
        }
        public Result Add(SportCreateDto sportDto)
        {
            var sport = SportMapper.ToEntity(sportDto);
            var result = _validator.Validate(sport);
            if (!result.IsValid)
            {
                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
            }

            if (_uow.Sports.ExistSameName(sport.SportName))
            {
                return Result.Failure("SPORT ALREADY EXISTS");
            }
            try
            {
                _uow.Sports.Add(sport);
                _uow.Save();
                return Result.Success();

            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Result Delete(int id)
        {
            var sport = _uow.Sports.GetById(id);
            if (sport == null)
            {
                return Result.Failure("SPORT NOT FOUND");
            }
            if (_uow.Sports.HasShoes(id))
            {
                return Result.Failure("SPORT HAS SHOES, CAN'T BE DELETED");
            }
            try
            {
                _uow.Sports.Delete(id);
                _uow.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Result<List<SportListDto>> GetAll()
        {
            var sports = _uow.Sports.GetAll().Select(SportMapper.ToListDto).ToList();
            return Result<List<SportListDto>>.Success(sports);
        }

        public Result<SportUpdateDto> GetForUpdate(int id)
        {
            var sport = _uow.Sports.GetById(id);
            if (sport == null)
            {
                return Result<SportUpdateDto>.Failure("SPORT NOT FOUND");
            }

            return Result<SportUpdateDto>.Success(SportMapper.ToUpdateDto(sport));
        }

        public Result Update(SportUpdateDto sportDto)
        {
            var sport = _uow.Sports.GetById(sportDto.SportId);
            if (sport == null)
            {
                return Result.Failure("SPORT NOT FOUND");
            }
            sport.SportName = sportDto.SportName;

            if (_uow.Sports.ExistSameName(sport.SportName, sport.SportId))
            {
                return Result.Failure("SPORT ALREADY EXISTS");
            }
            try
            {
                _uow.Sports.Update(sport);
                _uow.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
