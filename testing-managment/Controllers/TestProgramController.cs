using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using testing_managment.DataTransferObjects.CaseInBlockDto;
using testing_managment.DataTransferObjects.DutDto;
using testing_managment.DataTransferObjects.TestProgramDto;
using testing_managment.Interfaces;
using testing_managment.Models.Entities;

namespace testing_managment.Controllers
{
    //Контроллер
    [Route("api/[controller]")]
    [ApiController]
    public class TestProgramController : ControllerBase
    {
        private readonly IRequestsRepository _requestsRepository;
        private readonly IMapper _mapper;

        //Конструктор контроллера управления
        public TestProgramController(IRequestsRepository requestsRepository, IMapper mapper)
        {
            _requestsRepository = requestsRepository;
            _mapper = mapper;
        }

        //Создание
        [HttpPost("create")]
        [ProducesResponseType(500)]
        public IActionResult CreateDut([FromForm] TestProgramCreateDto testProgramCreate)
        {
            if (testProgramCreate == null)
            {
                return BadRequest(new { Message = "Объект содержит только пустые значения" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TestProgram testProgramMap = _mapper.Map<TestProgram>(testProgramCreate);


            if (!_requestsRepository.Create(testProgramMap))
            {
                return StatusCode(500, new { Message = "Внутренняя ошибка" });
            }

            return Ok(new { testProgramMap, Message = $"Запись {testProgramMap.Id} успешно создана" });
        }

        //Получение всех
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TestProgramViewDto>))]
        public IActionResult GetAll()
        {
            var testsPrograms = _mapper.Map<List<TestProgramViewDto>>(
                _requestsRepository.GetAll<TestProgram>());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(testsPrograms);
        }

        //Получение по id 
        [HttpGet("{testProgramId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TestProgramViewDto>))]
        public IActionResult GetById(Guid testProgramId)
        {
            var testProgram = _mapper.Map<TestProgramViewDto>(
                _requestsRepository.GetById<TestProgram>(testProgramId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(testProgram);
        }

        //Обновлание
        [HttpPut("update")]
        public IActionResult DeleteDut([FromForm] TestProgram testProgram)
        {
            if (testProgram == null)
            {
                return BadRequest(new { Message = $"Объект содержит только пустые значения" });
            }

            if (!_requestsRepository.Update(testProgram))
            {
                return StatusCode(500, new { Message = $"Что-то пошло не так во время создания записи" });
            }

            return Ok(new { Message = $"Запись обновлена" });
        }

        //Удаление по id
        [HttpDelete("delete")]
        public IActionResult DeleteDut(Guid Id)
        {
            var testProgram = _requestsRepository.GetById<TestProgram>(Id);

            if (!_requestsRepository.Delete(testProgram))
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
