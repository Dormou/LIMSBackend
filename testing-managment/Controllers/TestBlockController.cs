using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using testing_managment.DataTransferObjects.CaseInBlockDto;
using testing_managment.DataTransferObjects.DutDto;
using testing_managment.DataTransferObjects.TestBlockDto;
using testing_managment.Interfaces;
using testing_managment.Models.Entities;

namespace testing_managment.Controllers
{
    //Контроллер
    [Route("api/[controller]")]
    [ApiController]
    public class TestBlockController : ControllerBase
    {
        private readonly IRequestsRepository _requestsRepository;
        private readonly IMapper _mapper;

        //Конструктор контроллера управления
        public TestBlockController(IRequestsRepository requestsRepository, IMapper mapper)
        {
            _requestsRepository = requestsRepository;
            _mapper = mapper;
        }

        //Создание
        [HttpPost("create")]
        [ProducesResponseType(500)]
        public IActionResult CreateDut([FromForm] TestBlockCreateDto testBlockCreate)
        {
            if (testBlockCreate == null)
            {
                return BadRequest(new { Message = "Объект содержит только пустые значения" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TestBlock testBlockMap = _mapper.Map<TestBlock>(testBlockCreate);

            if (!_requestsRepository.Create(testBlockMap))
            {
                return StatusCode(500, new { Message = "Внутренняя ошибка" });
            }

            return Ok(new { testBlockMap, Message = $"Запись {testBlockMap.Id} успешно создана" });
        }

        //Получение всех
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TestBlockViewDto>))]
        public IActionResult GetAll()
        {
            var testsBlocks = _mapper.Map<List<TestBlockViewDto>>(
                _requestsRepository.GetAll<TestBlock>());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(testsBlocks);
        }

        //Получение по id 
        [HttpGet("{testBlockId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TestBlockViewDto>))]
        public IActionResult GetById(Guid testBlockId)
        {
            var testBlock = _mapper.Map<TestBlockViewDto>(
                _requestsRepository.GetById<TestBlock>(testBlockId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(testBlock);
        }

        //Обновлание
        [HttpPut("update")]
        public IActionResult Update([FromForm] TestBlock testBlock)
        {
            if (testBlock == null)
            {
                return BadRequest(new { Message = $"Объект содержит только пустые значения" });
            }

            if (!_requestsRepository.Update(testBlock))
            {
                return StatusCode(500, new { Message = $"Что-то пошло не так во время создания записи" });
            }

            return Ok(new { Message = $"Запись обновлена" });
        }

        //Удаление по id
        [HttpDelete("delete")]
        public IActionResult Delete(Guid Id)
        {
            var testBlock = _requestsRepository.GetById<TestBlock>(Id);

            if (!_requestsRepository.Delete(testBlock))
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
