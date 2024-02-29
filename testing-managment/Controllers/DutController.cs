using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using testing_managment.DataTransferObjects.DutDto;
using testing_managment.Interfaces;
using testing_managment.Models.Entities;

namespace testing_managment.Controllers
{
    //Контроллер Dut
    [Route("api/[controller]")]
    [ApiController]
    public class DutController : ControllerBase
    {
        private readonly IRequestsRepository _dutRepository;
        private readonly IMapper _mapper;

        //Конструктор контроллера управления дутами
        public DutController(IRequestsRepository dutRepository, IMapper mapper)
        {
            _dutRepository = dutRepository;
            _mapper = mapper;
        }

        //Создание Дута
        [HttpPost("createDut")]
        [ProducesResponseType(500)]
        public IActionResult CreateDut([FromForm] DutCreateDto dutCreate)
        {
            if (dutCreate == null)
            {
                return BadRequest(new { Message = "Объект содержит только пустые значения" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Dut dutMap = _mapper.Map<Dut>(dutCreate);
            

            if (!_dutRepository.Create(dutMap))
            {
                return StatusCode(500, new { Message = "Внутренняя ошибка" });
            }

            return Ok(new { dutMap, Message = $"Запись {dutCreate.Name} успешно создана" });
        }

        //Получение всех дутов
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DutViewDto>))]
        public IActionResult GetDuts()
        {
            var duts = _mapper.Map<List<DutViewDto>>(
                _dutRepository.GetAll<Dut>());
        
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(duts);
        }
        
        //Получение дута по id 
        [HttpGet("{dutId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DutViewDto>))]
        public IActionResult GetDutById(Guid dutId)
        {
            var dut = _mapper.Map<DutViewDto>(
                _dutRepository.GetById<Dut>(dutId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(dut);
        }

        //Обновлание дута
        [HttpPut("updateDut")]
        public IActionResult DeleteDut([FromForm] Dut dut)
        {
            if (dut == null)
            {
                return BadRequest(new { Message = $"Объект содержит только пустые значения" });
            }

            if (!_dutRepository.Update(dut))
            {
                return StatusCode(500, new { Message = $"Что-то пошло не так во время создания записи" });
            }

            return Ok(new { Message = $"Запись обновлена" });
        }
        
        //Удаление дута по id
        [HttpDelete("deleteDut")]
        public IActionResult DeleteDut(Guid dutId)
        {
            var dut = _dutRepository.GetById<Dut>(dutId);

            if (!_dutRepository.Delete(dut))
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


        
