using FluentValidation;
using ShoesDb2026.Data;
using ShoesDb2026.Entities;
using ShoesDb2026.Service.Common;
using ShoesDb2026.Service.DTOs.Shoe;
using ShoesDb2026.Service.DTOs.Sport;
using ShoesDb2026.Service.Interfaces;
using ShoesDb2026.Service.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Service.Services
{
    public class ShoeService : IShoeService
    {
        private readonly IValidator<Shoe> _validator;
        private readonly IUnitOfWork _uow;

        public ShoeService(IValidator<Shoe> validator, IUnitOfWork uow)
        {
            _validator = validator;
            _uow = uow;
        }
        public Result Add(ShoeCreateDto dto)
        {
            var shoe = new Shoe
            {
                Model = dto.Model,
                Description = dto.Description,
                Price = dto.Price,
                BrandId = dto.BrandId,
                SportId = dto.SportId,
                GenreId = dto.GenreId,
                SizeId = dto.SizeId,
                Active = true

            };

            var validationResult = _validator.Validate(shoe);
            if(!validationResult.IsValid)
            {
                return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            if(_uow.Shoes.ExistSameName(shoe.Model, shoe.BrandId, shoe.SportId))
            {
                return Result.Failure(new List<string> { "A SHOE ALREADY EXISTS." });
            }

            try
            {
                _uow.Shoes.Add(shoe);
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
            var shoe = _uow.Shoes.GetById(id);
            if(shoe == null)
            {
                return Result.Failure("SHOE NOT FOUND.");
            }
            try
            {
                _uow.Shoes.Delete(id);
                _uow.Save();
                return Result.Success();
            }
            catch (Exception ex)
            {

                return Result.Failure(ex.Message);
            }
        }

        public Result<List<ShoeListDto>> GetAll()
        {
            //var shoes = _uow.Shoes.GetAll().Select(ShoeMapper.ToListDto).ToList();

            var shoes = _uow.Shoes.GetAll().Select(s => new ShoeListDto
            {
                ShoeId = s.ShoeId,
                Model = s.Model,
                Price = s.Price,
                BrandName = s.Brand.BrandName,
                SportName = s.Sport.SportName,
                GenreName = s.Genre.GenreName,
                SizeNumber = s.Size.SizeNumber
            }).ToList();
            return Result<List<ShoeListDto>>.Success(shoes);
        }

        public Result<List<ShoeListDto>> GetByBrand(int brandId)
        {
            //var shoes=_uow.Shoes.GetByBrand(brandId)
            //    .Select(ShoeMapper.ToListDto)
            //    .ToList();
            //return Result<List<ShoeListDto>>.Success(shoes);
            var shoes=_uow.Shoes.GetByBrand(brandId).Select(ShoeMapper.ToListDto).ToList();
            if (shoes == null || !shoes.Any())
            {
                return Result<List<ShoeListDto>>.Failure("NO SHOES FOUND FOR THE SPECIFIED BRAND.");
            }
            return Result<List<ShoeListDto>>.Success(shoes);
        }

        public Result<List<ShoeListDto>> GetBySize(int sizeId)
        {
            //var shoes = _uow.Shoes.GetBySize(sizeId).Select(ShoeMapper.ToListDto).ToList();
            //return Result<List<ShoeListDto>>.Success(shoes);
            var shoes = _uow.Shoes.GetBySize(sizeId).Select(ShoeMapper.ToListDto).ToList();
            if (shoes == null || !shoes.Any())
            {
                return Result<List<ShoeListDto>>.Failure("NO SHOES FOUND FOR THE SPECIFIED SIZE.");
            }
            return Result<List<ShoeListDto>>.Success(shoes);
        }

        public Result<List<ShoeListDto>> GetBySport(int sportId)
        {
            //var shoes = _uow.Shoes.GetBySport(sportId).Select(ShoeMapper.ToListDto).ToList();
            //return Result<List<ShoeListDto>>.Success(shoes);
            var shoes = _uow.Shoes.GetBySport(sportId).Select(ShoeMapper.ToListDto).ToList();
            if (shoes == null || !shoes.Any())
            {
                return Result<List<ShoeListDto>>.Failure("NO SHOES FOUND FOR THE SPECIFIED SPORT.");
            }
            return Result<List<ShoeListDto>>.Success(shoes);
        }

        public Result<ShoeDetailsDto> GetDetails(int id)
        {
            //INICIO PRIMERA PRUEBA
            //var shoe = _uow.Shoes.GetById(id);
            //if (shoe is null)
            //{
            //    return Result<ShoeDetailsDto>.Failure("SHOE NOT FOUND.");
            //}
            //return Result<ShoeDetailsDto>.Success(ShoeMapper.ToDetailsDto(shoe));
            //FIN PRIMERA PRUEBA

            var shoe = _uow.Shoes.Query()
                .Where(s => s.ShoeId == id)
                .Select(s => new ShoeDetailsDto
                {
                    ShoeId = s.ShoeId,
                    Model = s.Model,
                    Description = s.Description,
                    Price = s.Price,
                    BrandName = s.Brand.BrandName,
                    SportName = s.Sport.SportName,
                    GenreName = s.Genre.GenreName,
                    SizeNumber = s.Size.SizeNumber
                })
                .FirstOrDefault();
            if (shoe == null)
            {
                return Result<ShoeDetailsDto>.Failure("SHOE NOT FOUND.");
            }
            return Result<ShoeDetailsDto>.Success(shoe);

        }

        public Result<ShoeUpdateDto> GetForUpdate(int id)
        {
            var shoe=_uow.Shoes.GetById(id);
            if(shoe == null)
            {
                return Result<ShoeUpdateDto>.Failure("SHOE NOT FOUND.");
            }

            var shoeUpdateDto = new ShoeUpdateDto
            {
                ShoeId = shoe.ShoeId,
                Model = shoe.Model,
                Description = shoe.Description,
                Price = shoe.Price,
                BrandId = shoe.BrandId,
                SportId = shoe.SportId,
                GenreId = shoe.GenreId,
                SizeId = shoe.SizeId
            };
            return Result<ShoeUpdateDto>.Success(shoeUpdateDto);
        }

        public Result<List<Genre>> GetGenres()
        {
            var genres = _uow.Genres.GetAll().ToList();
            return Result<List<Genre>>.Success(genres);
        }

        public Result<List<ShoeListDto>> OrderByBrand()
        {
            var shoes = _uow.Shoes.OrderByBrand().Select(ShoeMapper.ToListDto).ToList();
            return Result<List<ShoeListDto>>.Success(shoes);
        }

        public Result<List<ShoeListDto>> OrderByModel()
        {
            var shoes = _uow.Shoes.OrderByModel().Select(ShoeMapper.ToListDto).ToList();
            return Result<List<ShoeListDto>>.Success(shoes);
        }

        public Result<List<ShoeListDto>> OrderByPrice()
        {
            var shoes = _uow.Shoes.OrderByPrice().Select(ShoeMapper.ToListDto).ToList();
            return Result<List<ShoeListDto>>.Success(shoes);
        }

        public Result Update(ShoeUpdateDto dto)
        {
            var shoe = _uow.Shoes.GetById(dto.ShoeId);

            if(shoe == null)
            {
                return Result.Failure("SHOE NOT FOUND.");
            }

            shoe.Model = dto.Model;
            shoe.Description = dto.Description;
            shoe.Price = dto.Price;
            shoe.BrandId = dto.BrandId;
            shoe.SportId = dto.SportId;
            shoe.GenreId = dto.GenreId;
            shoe.SizeId = dto.SizeId;

            var validationResult = _validator.Validate(shoe);
            if (!validationResult.IsValid)
            {
                return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            if (_uow.Shoes.ExistSameName(shoe.Model, shoe.BrandId, shoe.SportId, shoe.ShoeId))
            {
                return Result.Failure(new List<string> { "A SHOE ALREADY EXISTS." });
            }

            try
            {
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
