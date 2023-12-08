using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Middleware.Interfaces;
using Middleware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseCRUDController<GET, POST, PUT>: ControllerBase
        where GET : BaseEntity
        where POST : BaseEntity
        where PUT : BaseEntity
    {
        protected readonly IBaseEntityRepository<GET> _repository;
        protected readonly IMapper _mapper;

        public BaseCRUDController(IBaseEntityRepository<GET> repository)
        {
            _repository = repository;
        }

        protected virtual IQueryable<GET> _list => _repository.FindAll();

        [HttpGet]
        public virtual IActionResult AllRecords(int limit = 1000, int skip = 0)
        {
            if (limit <= 0 || limit > 1000)
                limit = 1000;
            if (skip < 0)
                skip = 0;

            var result = _list.OrderBy(e => e.CreatedDate).Skip(skip).Take(limit).ToList();

            return Ok(result);
        }

        [HttpPost]
        public virtual IActionResult Create(POST entity)
        {
            var record = _mapper.Map<GET>(entity);
            if (_repository.Create(record) != Guid.Empty)
                return Ok(record);

            return StatusCode(500, "Creating record error");
        }

        [HttpGet("{id:guid}")]
        public virtual IActionResult Get(Guid id)
        {
            var record = _repository.Get(id);

            if (record == null)
                return NotFound("Id not found");

            return Ok(record);
        }
        [HttpPut]
        public virtual IActionResult Update(PUT entity)
        {
            var record = _mapper.Map<GET>(entity);
            var recordFromDb = _repository.Get(entity.Id);

            if (recordFromDb == null)
                return NotFound("Record's id not found");

            var result = _mapper.Map(record, recordFromDb);
            if (_repository.Update(result))
                return Ok(result);

            return StatusCode(500, "Updating record error");
        }

        [HttpDelete("id")]
        public virtual IActionResult Delete(Guid id)
        {
            if (!_list.Any(e => e.Id == id))
                return NotFound("Id not found");

            if (_repository.Delete(id))
                return Ok();

            return StatusCode(500, "Deleting record error");
        }
    }
}
