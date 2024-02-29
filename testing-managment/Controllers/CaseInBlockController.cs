using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using testing_managment.DataTransferObjects.CaseInBlockDto;
using testing_managment.DataTransferObjects.DutDto;
using testing_managment.Interfaces;
using testing_managment.Models.Entities;

namespace testing_managment.Controllers
{
    //Контроллер 
    [Route("api/[controller]")]
    [ApiController]
    public class CaseInBlockController : ControllerBase
    {
        private readonly IRequestsRepository _requestsRepository;
        private readonly IMapper _mapper;

        //Конструктор контроллера управления
        public CaseInBlockController(IRequestsRepository requestsRepository, IMapper mapper)
        {
            _requestsRepository = requestsRepository;
            _mapper = mapper;
        }

        //Создание
        [HttpPost("create")]
        [ProducesResponseType(500)]
        public IActionResult CreateDut([FromForm] CaseInBlockCreateDto caseInBlockCreate)
        {
            if (caseInBlockCreate == null)
            {
                return BadRequest(new { Message = "Объект содержит только пустые значения" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CaseInBlock caseInBlockMap = _mapper.Map<CaseInBlock>(caseInBlockCreate);


            if (!_requestsRepository.Create(caseInBlockMap))
            {
                return StatusCode(500, new { Message = "Внутренняя ошибка" });
            }

            return Ok(new { caseInBlockMap, Message = $"Запись {caseInBlockMap.Case} успешно создана" });
        }

        //Получение всех
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CaseInBlockViewDto>))]
        public IActionResult GetAll()
        {
            var casesInBlocks = _mapper.Map<List<CaseInBlockViewDto>>(
                _requestsRepository.GetAll<CaseInBlock>());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(casesInBlocks);
        }

        //Получение по id 
        [HttpGet("{caseInBlockId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CaseInBlockViewDto>))]
        public IActionResult GetById(Guid caseInBlockId)
        {
            var caseInBlock = _mapper.Map<CaseInBlockViewDto>(
                _requestsRepository.GetById<CaseInBlock>(caseInBlockId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(caseInBlock);
        }

        //Обновлание
        [HttpPut("update")]
        public IActionResult Update([FromForm] CaseInBlock caseInBlock)
        {
            if (caseInBlock == null)
            {
                return BadRequest(new { Message = $"Объект содержит только пустые значения" });
            }

            if (!_requestsRepository.Update(caseInBlock))
            {
                return StatusCode(500, new { Message = $"Что-то пошло не так во время создания записи" });
            }

            return Ok(new { Message = $"Запись обновлена" });
        }

        //Удаление по id
        [HttpDelete("delete")]
        public IActionResult Delete(Guid Id)
        {
            var caseInBlock = _requestsRepository.GetById<CaseInBlock>(Id);

            if (!_requestsRepository.Delete(caseInBlock))
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
