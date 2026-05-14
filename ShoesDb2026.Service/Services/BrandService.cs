using FluentValidation;
using ShoesDb2026.Data;
using ShoesDb2026.Entities;
using ShoesDb2026.Service.Common;
using ShoesDb2026.Service.DTOs.Brand;
using ShoesDb2026.Service.Interfaces;
using ShoesDb2026.Service.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Service.Services
{
    public class BrandService : IBrandService
    {
        private readonly IValidator<Brand> _validator;
        private readonly IUnitOfWork _uow;

        public BrandService(IValidator<Brand> validator, IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _uow = unitOfWork;
        }
        public Result Add(BrandCreateDto brandDto)
        {
            var brand = BrandMapper.ToEntity(brandDto);
            var result = _validator.Validate(brand);
            if (!result.IsValid)
            {
                return Result.Failure(result.Errors.Select(e => e.ErrorMessage).ToList());
            }
            if (_uow.Brands.ExistSameName(brand.BrandName))
            {
                return Result.Failure("BRAND ALREADY EXISTS");
            }
            try
            {
                _uow.Brands.Add(brand);
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
            var brand = _uow.Brands.GetById(id);
            if (brand == null)
            {
                return Result.Failure("BRAND NOT FOUND");
            }
            if(_uow.Brands.HasShoes(id))
            {
                return Result.Failure("BRAND HAS SHOES, CAN'T BE DELETED");
            }
            try
            {
                _uow.Brands.Delete(id);
                _uow.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Result<List<BrandListDto>> GetAll()
        {
            var brands = _uow.Brands.GetAll().Select(BrandMapper.ToListDto).ToList();
            return Result<List<BrandListDto>>.Success(brands);
        }

        public Result<BrandUpdateDto> GetForUpdate(int id)
        {
            var brand = _uow.Brands.GetById(id);
            if (brand == null) 
            {
                return Result<BrandUpdateDto>.Failure("BRAND NOT FOUND");
            }

            return Result<BrandUpdateDto>.Success(BrandMapper.ToUpdateDto(brand));
        }

        public Result Update(BrandUpdateDto brandDto)
        {
            var brand=_uow.Brands.GetById(brandDto.BrandId);
            if (brand==null)
            {
                return Result.Failure("BRAND NOT FOUND");
            }
            brand.BrandName = brandDto.BrandName;
            brand.ImageUrl = brandDto.ImageUrl;

            if(_uow.Brands.ExistSameName(brand.BrandName, brand.BrandId))
            {
                return Result.Failure("BRAND ALREADY EXISTS");
            }
            try
            {
                _uow.Brands.Update(brand);
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
