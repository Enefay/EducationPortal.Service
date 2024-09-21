﻿using AutoMapper;
using EducationPortal.BusinessLayer.Abstract;
using EducationPortal.DtoLayer.CategoryDto;
using EducationPortal.EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "RequireAdminRole")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        [HttpGet("CategoryList")]
        public IActionResult CategoryList()
        {
            var value = _mapper.Map<List<ResultCategoryDto>>(_categoryService.TGetListAll());
            if (value.Count == 0)
            {
                return BadRequest(new { message = "Herhangi bir kategori bulunamadı." });
            }
            return Ok(value);
        }

        [HttpPost("CreateCategory")]
        public IActionResult CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var value = _mapper.Map<Category>(createCategoryDto);
            _categoryService.TAdd(value);
            return Ok("Kategori Eklendi");
        }
        [HttpDelete("DeleteCategory")]
        public IActionResult DeleteCategory(int id)
        {
            var value = _categoryService.TGetByID(id);
            _categoryService.TDelete(value);
            return Ok("Kategori Silindi");
        }
        [HttpGet("GetCategoryById")]
        public IActionResult GetCategoryById(int id)
        {
            var value = _mapper.Map<GetCategoryDto>(_categoryService.TGetByID(id));
            return Ok(value);
        }
        [HttpPut("UpdateCategory")]
        public IActionResult UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var value = _mapper.Map<Category>(updateCategoryDto);
            _categoryService.TUpdate(value);
            return Ok("Kategori Güncellendi");
        }
    }
}
