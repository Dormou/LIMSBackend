using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using testing_managment.DataTransferObjects.CaseInBlockDto;
using testing_managment.DataTransferObjects.CompanyDto;
using testing_managment.DataTransferObjects.DutDto;
using testing_managment.Interfaces;
using testing_managment.Models.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace testing_managment.Controllers
{
    //Контроллер
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IRequestsRepository _requestsRepository;
        private readonly IMapper _mapper;

        //Конструктор контроллера управления
        public CompanyController(IRequestsRepository requestsRepository, IMapper mapper)
        {
            _requestsRepository = requestsRepository;
            _mapper = mapper;
        }

        //Создание
        [HttpPost("create")]
        [ProducesResponseType(500)]
        public IActionResult CreateDut([FromForm] CompanyCreateDto companyCreate)
        {
            if (companyCreate == null)
            {
                return BadRequest(new { Message = "Объект содержит только пустые значения" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Company companyMap = _mapper.Map<Company>(companyCreate);


            if (!_requestsRepository.Create(companyMap))
            {
                return StatusCode(500, new { Message = "Внутренняя ошибка" });
            }

            return Ok(new { companyMap, Message = $"Запись {companyCreate.Name} успешно создана" });
        }

        //Получение всех
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CompanyViewDto>))]
        public IActionResult GetAll()
        {
            var companies = _mapper.Map<List<CompanyViewDto>>(
                _requestsRepository.GetAll<Company>());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(companies);
        }

        //Получение по id 
        [HttpGet("{companyId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CompanyViewDto>))]
        public IActionResult GetById(Guid companyId)
        {
            var company = _mapper.Map<CompanyViewDto>(
                _requestsRepository.GetById<Company>(companyId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(company);
        }

        //Обновлание
        [HttpPut("update")]
        public IActionResult Update([FromForm] Company company)
        {
            if (company == null)
            {
                return BadRequest(new { Message = $"Объект содержит только пустые значения" });
            }

            if (!_requestsRepository.Update(company))
            {
                return StatusCode(500, new { Message = $"Что-то пошло не так во время создания записи" });
            }

            return Ok(new { Message = $"Запись обновлена" });
        }

        //Удаление по id
        [HttpDelete("delete")]
        public IActionResult Delete(Guid Id)
        {
            var company = _requestsRepository.GetById<Company>(Id);

            if (!_requestsRepository.Delete(company))
            {
                return StatusCode(500, new { Message = $"Что-то пошло не так во время удаления записи" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new { Message = $"Запись удалена" });
        }
    }
}
